using App.Contracts.BLL;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using Base.Extensions;
using AppUser = App.Domain.Identity.AppUser;

namespace WebApp.ApiControllers.Identity;

using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _rnd = new();
    private readonly IAppBLL _bll;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
        IConfiguration configuration, ILogger<AccountController> logger, IAppBLL bll)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
        _bll = bll;
    }

    /// <summary>
    /// Login endpoint, requires user credentials
    /// </summary>
    /// <param name="loginData"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Identity.JwtResponse), 200)]
    [ProducesResponseType(typeof(App.Public.DTO.v1.RestApiErrorResponse), 400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<JwtResponse>> LogIn([FromBody] Login loginData)
    {
        // verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        // verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password problem for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        await _bll.AppUsers.LoadAllUserRefreshTokens(appUser);
        foreach (var userRefreshToken in appUser.RefreshTokens!)
        {
            if (userRefreshToken.TokenExpirationDateTime < DateTime.UtcNow &&
                userRefreshToken.PreviousTokenExpirationDateTime < DateTime.UtcNow)
            {
                await _bll.RefreshTokens.RemoveAsync(userRefreshToken.Id);
            }
        }

        await _bll.SaveChangesAsync();

        var refreshToken = new App.BLL.DTO.Identity.RefreshToken();
        refreshToken.OwnerId = appUser.Id;

        _bll.RefreshTokens.Add(refreshToken, appUser.Id);
        await _bll.RefreshTokens.AddRefreshTokenToUser(appUser.Id, refreshToken);
        await _bll.SaveChangesAsync();

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        var res = new JwtResponse()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.Token
        };

        return Ok(res);
    }


    /// <summary>
    /// Register endpoint, requires new user credentials
    /// </summary>
    /// <param name="registrationData"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Identity.JwtResponse), 200)]
    [ProducesResponseType(typeof(App.Public.DTO.v1.RestApiErrorResponse), 400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<JwtResponse>> Register(Register registrationData)
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };
            errorResponse.Errors["email"] = new List<string>()
            {
                "Email already registered"
            };
            return BadRequest(errorResponse);
        }

        var refreshToken = new App.BLL.DTO.Identity.RefreshToken();

        appUser = new AppUser()
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName
        };

        // create user (system will do it)
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        await _userManager.AddToRoleAsync(appUser, "user");

        await _bll.SaveChangesAsync();

        refreshToken.OwnerId = appUser.Id;
        await _bll.RefreshTokens.AddRefreshTokenToUser(appUser.Id, refreshToken);
        _bll.RefreshTokens.Add(refreshToken);
        await _bll.SaveChangesAsync();

        // get full user from system with fixed data
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after registration", registrationData.Email);
            return BadRequest($"User with email {registrationData.Email} is not found after registration");
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            return NotFound("User/Password problem");
        }

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        var res = new JwtResponse()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.Token
        };

        return Ok(res);
    }
    
    /// <summary>
    /// Refresh token endpoit. Returns new jwt and refreshtoken. JWT is obsoleted minute after generation
    /// </summary>
    /// <param name="jwtResponse"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Identity.JwtResponse), 200)]
    [ProducesResponseType(typeof(App.Public.DTO.v1.RestApiErrorResponse), 400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> RefreshToken([FromBody] JwtResponse jwtResponse)
    {
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.Jwt);
            if (jwtToken == null)
            {
                return BadRequest("No token");
            }
        }
        catch (Exception e)
        {
            return BadRequest($"Cant parse the token, {e.Message}");
        }

        // TODO: validate token signature
        // https://stackoverflow.com/questions/49407749/jwt-token-validation-in-asp-net

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest("No email in jwt");
        }

        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound($"User with email {userEmail} not found");
        }

        // load and compare refresh tokens
        await _bll.AppUsers.LoadValidUserRefreshTokens(appUser, jwtResponse.RefreshToken);

        if (appUser.RefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }

        if (appUser.RefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }

        if (appUser.RefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found.");
        }

        // generate new jwt

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            return NotFound("User/Password problem");
        }

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        // make new refresh token, obsolete old ones
        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.Token == jwtResponse.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);

            await _bll.SaveChangesAsync();
        }

        var res = new JwtResponse()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.Token
        };

        return Ok(res);
    }
}