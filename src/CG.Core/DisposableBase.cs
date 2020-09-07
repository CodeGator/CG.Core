using System;

namespace CG
{
    /// <summary>
    /// This class is a base implementation of the standard .NET "disposable
    /// pattern". 
    /// </summary>
    /// <remarks>
    /// <para>
    /// The idea, with this class, is to reduce the amount of clutter created 
    /// by implementing the <see cref="IDisposable"/> interface on a class. 
    /// Of course, this approach only works where inheritance is a possibility.
    /// Still, it's better than repetitively following the .NET "disposable 
    /// pattern" in every class that implements <see cref="IDisposable"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// This example demonstrates how to derive from <see cref="DisposableBase"/>
    /// and write your own cleanup code.
    /// <code>
    /// class MyClass : DisposableBase
    /// {
    ///    protected void override Dispose(bool disposing)
    ///    {
    ///        // TODO : write your cleanup code here.
    ///        base.Dispose(disposing);
    ///    }
    /// }
    /// </code>
    /// </example>
    public abstract class DisposableBase : IDisposable
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a flag that indicates whether the object
        /// has been disposed of.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        #endregion

        // *******************************************************************
        // IDisposable implementation.
        // *******************************************************************

        #region IDisposable implementation

        /// <summary>
        /// This method performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            if (!IsDisposed)
            {
                // Dispose the object.
                Dispose(true);

                // No need for GC on this object.
                GC.SuppressFinalize(this);

                // Mark the object as disposed.
                IsDisposed = true;
            }
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method is called when the object is disposed.
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources; false
        /// otherwise.</param>
        protected virtual void Dispose(
            bool disposing
            )
        {
            // TODO : override in your base class to dispose of your resources.
        }

        #endregion
    }
}
