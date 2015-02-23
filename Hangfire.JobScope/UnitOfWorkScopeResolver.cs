using System.Collections.Concurrent;
using System.Linq;
using Ninject.Activation;

namespace Hangfire.JobScope {
    /// <summary>
    ///     Provides methods for resolving a scope based on any callback that is registered
    /// </summary>
    class UnitOfWorkScopeResolver : IUnitOfWorkScopeResolver {
        private readonly IProducerConsumerCollection<ResolverListEntry> resolvers = new ConcurrentBag<ResolverListEntry> ();

        /// <summary>
        ///     Adds a resolver.  The order is the order in which the resolvers are tried.  The lowest order number
        ///     is tried first
        /// </summary>
        public void RegisterResolver (IScopeResolver resolver, int order) {
            resolvers.TryAdd (new ResolverListEntry {
                Resolver = resolver,
                Order = order
            });
        }

        /// <summary>
        ///     Resolves a scope for the provided context.  If none of the registered resolvers can resolve the scope, null
        ///     is returned
        /// </summary>
        public object Resolve (IContext context) {
            return resolvers
                .OrderBy (entry => entry.Order)
                .Select (entry => entry.Resolver.Resolve (context))
                .FirstOrDefault (scope => scope != null);
        }
    }
}