using Ninject.Activation;

namespace Hangfire.JobScope {
    /// <summary>
    ///     Scope resolver for Owin hosted web application requests
    /// </summary>
    public class HostScopeResolver : IScopeResolver {
        private readonly IHostScopeLifetimeManager lifetimeManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public HostScopeResolver (IHostScopeLifetimeManager lifetimeManager) {
            this.lifetimeManager = lifetimeManager;
        }

        /// <summary>
        ///     Returns the current request scope.  The scope should be started in the RequestScopeMiddleware
        /// </summary>
        public object Resolve (IContext context) {
            return lifetimeManager.GetScope ();
        }
    }
}