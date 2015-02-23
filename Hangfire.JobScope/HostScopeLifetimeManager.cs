using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Hangfire.JobScope {
    class HostScopeLifetimeManager : IHostScopeLifetimeManager {
        private readonly IDictionary<object, UnitOfWorkScope> scopes = new Dictionary<object, UnitOfWorkScope> ();
        private readonly object scopesSyncRoot = new object ();

        /// <summary>
        ///     Creates a new scope for the current web request or host context. If one has already been created, the existing
        ///     scope will be returned.
        /// </summary>
        public UnitOfWorkScope CreateScope () {
            var hostContext = CallContext.HostContext;
            lock (scopesSyncRoot) {
                return GetScope () ?? CreateAndStoreNewScope (hostContext);
            }
        }

        /// <summary>
        ///     Destroys and dereferences the current logical unit of work scope
        /// </summary>
        public void DestroyScope () {
            var scope = GetScope ();
            if (scope != null) {
                scope.Dispose ();
            }
        }

        /// <summary>
        ///     Gets the scope for the current web request. If one has not been set up this method returns null
        /// </summary>
        public UnitOfWorkScope GetScope () {
            var hostContext = CallContext.HostContext;
            if (hostContext == null) {
                return null;
            }
            UnitOfWorkScope value;
            scopes.TryGetValue (hostContext, out value);
            return value;
        }

        /// <summary>
        ///     Creates a new scope and begins tracking the lifetime of the scope
        /// </summary>
        private UnitOfWorkScope CreateAndStoreNewScope (object hostContext) {
            var scope = new UnitOfWorkScope ();
            scope.Disposed += (sender, e) => RemoveScope (hostContext);
            scopes.Add (hostContext, scope);
            return scope;
        }

        /// <summary>
        ///     Dereferences the scope and stopps all tracking of it's lifecycle
        /// </summary>
        private void RemoveScope (object hostContext) {
            lock (scopesSyncRoot) {
                if (scopes.ContainsKey (hostContext)) {
                    scopes.Remove (hostContext);
                }
            }
        }
    }
}