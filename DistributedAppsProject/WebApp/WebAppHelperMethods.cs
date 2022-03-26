using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace WebApp;

public class WebAppHelperMethods
{
    public static List<string> GetAdminAreaControllersNames()
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        var controllersInfos = asm.GetTypes()
            .Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
            .ToList();

        var controllersInfoStrings = new List<string>();

        foreach (var controllersInfo in controllersInfos)
        {
            var controllerAsString = controllersInfo.ToString();
            if (controllerAsString.Contains("WebApp.Areas.Admin.Controllers."))
            {
                controllerAsString = controllerAsString.Split("WebApp.Areas.Admin.Controllers.")[1];
                controllerAsString = controllerAsString.Split("Controller")[0];
                controllersInfoStrings.Add(controllerAsString);
            }
        }

        return controllersInfoStrings;
    }
}