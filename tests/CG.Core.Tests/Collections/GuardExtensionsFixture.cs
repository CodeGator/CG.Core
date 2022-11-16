using CG.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace System.Collections
{
    /// <summary>
    /// This class is a test fixture for the <see cref="GuardExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class GuardExtensionsFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfEmpty{T}(Validations.IGuard, System.Collections.Generic.IEnumerable{T}, string, string, string, int)"/> 
        /// method throws an exception if the argument contains an empty collection.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GuardExtensions_ThrowIfEmpty_IsEmpty()
        {
            Guard.Instance().ThrowIfEmpty(new string[0], null);
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfEmpty{T}(Validations.IGuard, System.Collections.Generic.IEnumerable{T}, string, string, string, int)"/> 
        /// method does not throw an exception if the argument contains a non-empty
        /// collection.
        /// </summary>
        [TestMethod]
        public void GuardExtensions_ThrowIfEmpty_NotEmpty()
        {
            Guard.Instance().ThrowIfEmpty(new string[] { "A" }, null);
        }

        #endregion
    }
}
