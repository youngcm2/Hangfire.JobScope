using Ninject.Modules;

namespace Hangfire.JobScope {
    public class CommonInfrastructureModule : NinjectModule {
        /// <summary>
        ///     Loads the module into the kernel.
        /// </summary>
        public override void Load () {
            Bind<IUnitOfWorkScopeResolver> ()
                .To<UnitOfWorkScopeResolver> ()
                .InSingletonScope ();

            Bind<IHostScopeLifetimeManager> ()
                .To<HostScopeLifetimeManager> ()
                .InSingletonScope ();
        }
    }
}