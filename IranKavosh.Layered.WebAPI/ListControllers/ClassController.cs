using System.Reflection;
using Microsoft.AspNetCore.Mvc;
namespace IranKavosh.Layered.WebAPI.ListControllers
{
    public class ClassController
    {
       public class ModelController
        {
            public string Controller { get; set; }
            public string Action { get; set; }

        }
        public static List<ModelController> GetController()
        {
            var controllerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

            List<ModelController> controllers = new List<ModelController>();
            foreach (var controller in controllerTypes)
            {
                Console.WriteLine($"Controller: {controller.Name}");
             
                var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(m => m.IsPublic && !m.IsSpecialName);

                foreach (var method in methods)
                {
                    controllers.Add(new ModelController
                    {
                        Controller = controller.Name,
                        Action = method.Name
                    });
                    Console.WriteLine($"  Action: {method.Name}");
                }
            }

            return controllers;
        }


    }
}
