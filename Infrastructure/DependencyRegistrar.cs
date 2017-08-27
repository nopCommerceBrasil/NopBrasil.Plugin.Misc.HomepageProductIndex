using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using NopBrasil.Plugin.Misc.HomepageProductIndex.Controllers;
using NopBrasil.Plugin.Misc.HomepageProductIndex.Service;
using NopBrasil.Plugin.Misc.HomepageProductIndex.Task;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig nopConfig)
        {
            builder.RegisterType<MiscHomepageProductIndexController>().AsSelf();
            builder.RegisterType<HomepageProductIndexService>().As<IHomepageProductIndexService>().InstancePerDependency();
            builder.RegisterType<HomepageProductIndexTask>();
        }

        public int Order => 2;
    }
}
