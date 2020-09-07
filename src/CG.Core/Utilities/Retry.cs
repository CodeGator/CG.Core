using System;
using System.Diagnostics;

namespace CG.Utilities
{
    /// <summary>
    /// This class is a singleton implementation of the <see cref="IRetry"/>
    /// interface.
    /// </summary>
    public sealed class Retry : SingletonBase<Retry>, IRetry
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constuctor creates a new instance of the <see cref="Retry"/>
        /// class.
        /// </summary>
        [DebuggerStepThrough]
        private Retry() { }

        #endregion
    }
}
