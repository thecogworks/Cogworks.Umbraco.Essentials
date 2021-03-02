using Cogworks.Umbraco.Essentials.Infrastructure;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Cogworks.Umbraco.Essentials.Composers
{
    public class InfrastructureComposer : IUserComposer
    {
        public void Compose(Composition composition)
            => composition.SetServerRegistrar(new ServerRegistrar());
    }
}