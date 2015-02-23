using System.Runtime.Remoting.Messaging;
using Hangfire.Server;

namespace Hangfire.JobScope {
    /// <summary>
    ///     Sets up the host context for Hangfire jobs so that they
    /// </summary>
    public class HangfireHostContextFilter : IServerFilter {
        private readonly IHostScopeLifetimeManager hostScopeLifetimeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public HangfireHostContextFilter (IHostScopeLifetimeManager hostScopeLifetimeManager) {
            this.hostScopeLifetimeManager = hostScopeLifetimeManager;
        }

        /// <summary>
        ///     Called after the performance of the job.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnPerformed (PerformedContext filterContext) {
            hostScopeLifetimeManager.DestroyScope ();
            CallContext.HostContext = null;
        }

        /// <summary>
        ///     Called before the performance of the job.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnPerforming (PerformingContext filterContext) {
            CallContext.HostContext = filterContext;
            hostScopeLifetimeManager.CreateScope ();
        }
    }
}