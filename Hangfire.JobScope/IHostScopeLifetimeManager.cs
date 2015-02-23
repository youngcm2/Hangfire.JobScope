namespace Hangfire.JobScope {
    public interface IHostScopeLifetimeManager {
        /// <summary>
        ///     Creates a new scope for the current web request or host context. If one has already been created, the existing
        ///     scope will be returned.
        /// </summary>
        UnitOfWorkScope CreateScope ();

        /// <summary>
        ///     Gets the scope for the current web request. If one has not been set up this method returns null
        /// </summary>
        UnitOfWorkScope GetScope ();

        /// <summary>
        ///     Destroys and dereferences the current logical unit of work scope
        /// </summary>
        void DestroyScope ();
    }
}