using System;
using Hangfire;
using Hangfire.JobScope;
using Hangfire.Server;
using Hangfire.SqlServer;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace Sample {
	public class SampleModule : NinjectModule {
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load () {
			JobActivator.Current = new NinjectJobActivator (Kernel);

			Bind<JobStorage> ()
				.ToMethod (CreateStorage)
				.InSingletonScope ();

			Bind<BackgroundJobServerOptions> ()
				.ToMethod (context => {
					var name = String.Format ("{0}:{1}", Environment.MachineName, "Sample");

					return new BackgroundJobServerOptions {
						ServerName = name
					};
				});


			Bind<IServerSupervisor> ()
				.ToMethod (CreateBackgroundJobServer)
				.InSingletonScope ();

			Bind<ISampleClass> ()
				.To<SampleClass> ()
				.InUnitOfWorkScope ();

		}

		private JobStorage CreateStorage (IContext arg) {
			var storage = new SqlServerStorage ("Data Source=.;Initial Catalog=HangfireTest;Integrated Security=True;MultipleActiveResultSets=True;Asynchronous Processing=true;");
			return storage;
		}


		private IServerSupervisor CreateBackgroundJobServer (IContext context) {
			var kernel = context.Kernel;
			var backgroundJobServerOptions = kernel.Get<BackgroundJobServerOptions> ();
			var jobStorage = kernel.Get<JobStorage> ();
			var backgroundJobServer = new BackgroundJobServer (backgroundJobServerOptions, jobStorage);

			return backgroundJobServer;
		}


	}
}