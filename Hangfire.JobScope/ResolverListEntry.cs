namespace Hangfire.JobScope {
    class ResolverListEntry {
        public int Order { get; set; }
        public IScopeResolver Resolver { get; set; }
    }
}