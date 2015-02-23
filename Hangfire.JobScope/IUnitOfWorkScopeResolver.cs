using Ninject.Activation;

namespace Hangfire.JobScope {
    public interface IUnitOfWorkScopeResolver {
        /// <summary>
        ///     Adds a resolver callback.  The order is the order in which the callbacks are tried.  The lowest order number
        ///     is tried first
        /// </summary>
        void RegisterResolver (IScopeResolver resolver, int order);

        /// <summary>
        ///     Resolves a scope for the provided context.  If none of the registered resolvers can resolve the scope, null
        ///     is returned
        /// </summary>
        object Resolve (IContext context);
    }
}