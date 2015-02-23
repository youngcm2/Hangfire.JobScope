using System;
using Hangfire.JobScope;
using Ninject;

namespace Sample {
	class Program : IDisposable {
		private IKernel kernel;

		static void Main (string [] args) {

			using (var program = new Program ()) {
				program.Run ();
			}
		}

		public void Dispose () {
			if (kernel != null) {
				kernel.Dispose ();
			}
		}

		private void Run () {
			ConfigureNinject ();
			var service = CreateService ();
			service.Start ();
			Console.ReadLine ();
		}

		private TestServer CreateService () {
			return kernel.Get<TestServer> ();
		}

		private void ConfigureNinject () {
			kernel = new StandardKernel ();
			kernel.Load (new SampleModule ());

			var unitOfWorkScopeResolver = kernel.Get<IUnitOfWorkScopeResolver> ();
			var hostContextScopeResolver = kernel.Get<HostScopeResolver> ();
			unitOfWorkScopeResolver.RegisterResolver (hostContextScopeResolver, 100);
		}
	}
}
