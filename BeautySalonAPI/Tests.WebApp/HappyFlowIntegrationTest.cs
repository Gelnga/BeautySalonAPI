using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using AppUser = App.Domain.Identity.AppUser;

namespace Tests.WebApp;

public class HappyFlowIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public HappyFlowIntegrationTest(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
    }

    [Fact]
    public async Task TestUserHappyFlow()
    {
        // Arrange
        var rnd = new Random();
        
        var registerDto = new Register()
        {
            Email = "test@test.test",
            Password = "Test1.test",
            FirstName = "Test First",
            LastName = "Test Last"
        };
        var jsonStr = System.Text.Json.JsonSerializer.Serialize(registerDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        var registerResponse = await _client.PostAsync("/api/v1/identity/Account/Register/", data);
       
        var requestContent = await registerResponse.Content.ReadAsStringAsync();

        var resultJwt = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(
            requestContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );
        
        // Act
        // Get random salon
        var apiRequestSalons = new HttpRequestMessage();
        apiRequestSalons.Method = HttpMethod.Get;
        apiRequestSalons.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequestSalons.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Jwt);
        apiRequestSalons.RequestUri = new Uri("https://localhost:7202/api/v1/Salons");

        var requestSalonsResponse = await _client.SendAsync(apiRequestSalons);
        var requestSalonsResponseContent = await requestSalonsResponse.Content.ReadAsStringAsync();

        var resultSalons = System.Text.Json.JsonSerializer.Deserialize<ICollection<Salon>>(
            requestSalonsResponseContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );
        
        // Get random salon service
        var salon = resultSalons!.ToList()[rnd.Next(resultSalons!.Count - 1)];
        
        var apiRequestSalonServices = new HttpRequestMessage();
        apiRequestSalonServices.Method = HttpMethod.Get;
        apiRequestSalonServices.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequestSalonServices.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Jwt);
        apiRequestSalonServices.RequestUri = new Uri($"https://localhost:7202/api/v1/salons/{salon.Id}/salonServices");

        var requestSalonServicesResponse = await _client.SendAsync(apiRequestSalonServices);
        var requestSalonServicesResponseContent = await requestSalonServicesResponse.Content.ReadAsStringAsync();

        var resultSalonServices = System.Text.Json.JsonSerializer.Deserialize<ICollection<ServiceWithSalonServiceData>>(
            requestSalonServicesResponseContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );

        var service = resultSalonServices!.ToList()[rnd.Next(resultSalonServices!.Count - 1)];
        
        // Get random worker
        var apiRequestSalonWorkers = new HttpRequestMessage();
        apiRequestSalonWorkers.Method = HttpMethod.Get;
        apiRequestSalonWorkers.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequestSalonWorkers.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Jwt);
        apiRequestSalonWorkers.RequestUri = new Uri($"https://localhost:7202/api/v1/salons/{salon.Id}/workers?serviceId={service.Id}");

        var requestSalonWorkersResponse = await _client.SendAsync(apiRequestSalonWorkers);
        var requestSalonWorkersResponseContent = await requestSalonWorkersResponse.Content.ReadAsStringAsync();

        var resultSalonWorkers = System.Text.Json.JsonSerializer.Deserialize<ICollection<WorkerWithSalonServiceData>>(
            requestSalonWorkersResponseContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );

        var worker = resultSalonWorkers!.ToList()[rnd.Next(resultSalonWorkers!.Count - 1)];
        
        // Get random appointment time
        var date = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
        var apiRequestWorkersTime = new HttpRequestMessage();
        apiRequestWorkersTime.Method = HttpMethod.Get;
        apiRequestWorkersTime.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequestWorkersTime.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Jwt);
        apiRequestWorkersTime.RequestUri = new Uri($"https://localhost:7202/api/v1/workers/{worker.Id}/GetWorkerAvailableTimes?" +
                                                   $"date={date.ToString()}&serviceDuration={worker.ServiceDuration}");

        var requestWorkersTimeResponse = await _client.SendAsync(apiRequestWorkersTime);
        var requestWorkersTimeResponseContent = await requestWorkersTimeResponse.Content.ReadAsStringAsync();
        
        var resultWorkersTime = System.Text.Json.JsonSerializer.Deserialize<ICollection<AvailableTimeSpan>>(
            requestWorkersTimeResponseContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );

        var time = resultWorkersTime!.ToList()[rnd.Next(resultWorkersTime!.Count - 1)];
        
        // Create appointment
        
        var appointment = new Appointment
        {
            AppointmentDate = date.ToString(),
            AppointmentStart = time.availableStartTime,
            AppointmentEnd = time.availableEndTime,
            WorkerId = worker.Id,
            ServiceId = service.Id,
            SalonId = salon.Id,
            Price = service.Price + " " + service.UnitName,
            Commentary = null
        };
        var jsonAppointment = JsonSerializer.Serialize(appointment);
        _testOutputHelper.WriteLine(resultJwt!.Jwt);
        
        var apiRequestAppointment = new HttpRequestMessage();
        apiRequestAppointment.Method = HttpMethod.Post;
        apiRequestAppointment.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequestAppointment.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Jwt);
        apiRequestAppointment.Content = new System.Net.Http.StringContent(jsonAppointment, Encoding.UTF8, "application/json");
        apiRequestAppointment.RequestUri = new Uri($"https://localhost:7202/api/v1/appointments");

        var requestAppointmentResponse = await _client.SendAsync(apiRequestAppointment);
        
        // Assert
        registerResponse.EnsureSuccessStatusCode();
        requestSalonsResponse.EnsureSuccessStatusCode();
        requestSalonServicesResponse.EnsureSuccessStatusCode();
        requestSalonWorkersResponse.EnsureSuccessStatusCode();
        requestWorkersTimeResponse.EnsureSuccessStatusCode();
        requestAppointmentResponse.EnsureSuccessStatusCode();
    }
}