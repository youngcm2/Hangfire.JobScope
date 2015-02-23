using System;

namespace Sample {
	public class SampleClass : ISampleClass {
		public void Write (string format, params object[] args) {
			Console.WriteLine(format, args);
		}
	}
}