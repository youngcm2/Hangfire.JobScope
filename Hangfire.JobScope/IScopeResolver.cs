using Ninject.Activation;

namespace Hangfire.JobScope {
    public interface IScopeResolver {
        object Resolve (IContext context);
    }
}