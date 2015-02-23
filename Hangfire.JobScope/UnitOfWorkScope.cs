using System;
using Ninject.Infrastructure.Disposal;

namespace Hangfire.JobScope {
    public class UnitOfWorkScope : INotifyWhenDisposed {
        public event EventHandler Disposed;

        /// <summary>
        ///     Gets a value indicating whether this instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Notifies all listeners that this object has been disposed
        /// </summary>
        public void Dispose () {
            if (Disposed != null) {
                Disposed (this, new EventArgs ());
                Disposed = null;
            }
            IsDisposed = true;
        }
    }
}