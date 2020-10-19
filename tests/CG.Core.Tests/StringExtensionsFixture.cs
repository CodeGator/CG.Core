using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG
{
    /// <summary>
    /// This class is a QA fixture for the <see cref="StringExtensions"/>
    /// class.
    /// </summary>
    [TestCategory("Unit")]
    [TestClass]
    public class StringExtensionsFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="StringExtensions.IsMatch(string, string)"/>
        /// method matches strings with single char wildcard symbols.
        /// </summary>
        [TestMethod]
        public void StringExtensions_IsMatch_Single()
        {
            // Arrange ...

            // Act ...
            var result = "ABXD".IsMatch("AB?D");

            // Assert ...
            Assert.IsTrue(result, "method failed to match a single char wildcard");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="StringExtensions.IsMatch(string, string)"/>
        /// method matches strings with multi char wildcard symbols.
        /// </summary>
        [TestMethod]
        public void StringExtensions_IsMatch_Multi()
        {
            // Arrange ...

            // Act ...
            var result = "ABCD".IsMatch("AB*");

            // Assert ...
            Assert.IsTrue(result, "method failed to match a multi char wildcard");
        }

        #endregion
    }
}
