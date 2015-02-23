using Ninject;
using Ninject.Syntax;

namespace Hangfire.JobScope {
    public static class NinjectExtensions {
        /// <summary>
        ///     Uses the IUnitOfWorkScopeResolver to resolve the scope.  A unit of work is typically a web request
        ///     or a job execution.
        /// </summary>
        public static IBindingNamedWithOrOnSyntax<T> InUnitOfWorkScope<T> (this IBindingInSyntax<T> syntax) {
            return syntax.InScope (context => context.Kernel.Get<IUnitOfWorkScopeResolver> ()
                .Resolve (context));
        }
    }
}