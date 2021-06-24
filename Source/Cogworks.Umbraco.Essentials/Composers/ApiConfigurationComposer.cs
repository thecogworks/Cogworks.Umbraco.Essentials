using System.Linq;
using System.Web.Http.Controllers;
using Cogworks.Umbraco.Essentials.Components;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Cogworks.Umbraco.Essentials.Composers
{
#pragma warning disable CA1812 // Class never use

    internal class ApiConfigurationComposer : IUserComposer
#pragma warning restore CA1812 // Class never use
    {
        public void Compose(Composition composition)
        {
            RegisterApiControllers(composition);

            composition.Components().Append<ApiConfigurationComponent>();
        }

        private void RegisterApiControllers(IRegister container)
        {
            var controllerTypes = GetType().Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => typeof(IHttpController).IsAssignableFrom(t))
                .ToList();

            foreach (var controllerType in controllerTypes)
            {
                container.Register(controllerType, Lifetime.Request);
            }
        }
    }
}