using Hangfire;
using Hangfire.JobScope;
using Hangfire.Server;

namespace Sample {
	public class TestServer {
		private readonly IServerSupervisor hangfireJobServer;
		private readonly HangfireHostContextFilter hangfireHostContextFilter;

		public TestServer (IServerSupervisor hangfireJobServer, HangfireHostContextFilter hangfireHostContextFilter) {
			this.hangfireJobServer = hangfireJobServer;
			this.hangfireHostContextFilter = hangfireHostContextFilter;
		}

		public void Start () {
			GlobalJobFilters.Filters.Add (hangfireHostContextFilter);
			hangfireJobServer.Start ();
		}
	}
}