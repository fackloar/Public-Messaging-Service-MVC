using MessagingService.Models;
using Microsoft.AspNetCore.Mvc;
using RazorLight;

namespace MessagingService.Models
{
    static public class TemplateCreator
    {
        public static string GetEmailSubject()
        {
            return "Testing Service";
        }
        public static async Task<string> GetEmailMessage(string name)
        {
            string templatePath = $@"{Directory.GetCurrentDirectory()}\EmailTemplates";
            var engine = new RazorLightEngineBuilder()
                // required to have a default RazorLightProject type,
                // but not required to create a template from string.
                .UseEmbeddedResourcesProject(typeof(Email))
                .SetOperatingAssembly(typeof(Email).Assembly)
                .UseMemoryCachingProvider()
                .Build();

            string template = "Hello, @Model.Name. This email is sent from a MVC Applicaton.";

            var model = new Email()
            {
                Name = name
            };

            string result = await engine.CompileRenderStringAsync("templateKey",template, model);

            return result;
        }
    }
}
