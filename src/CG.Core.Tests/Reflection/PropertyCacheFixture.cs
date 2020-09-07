using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.Reflection
{
    /// <summary>
    /// This class is a QA fixture for the <see cref="PropertyCache"/>
    /// class.
    /// </summary>
    [TestCategory("Unit")]
    [TestClass]
    public class PropertyCacheFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="PropertyCache.PropertyCache"/>
        /// constructor creates a property intialized object.
        /// </summary>
        [TestMethod]
        public void PropertyCache_ctor()
        {
            // Arrange ...

            // Act ...
            var result = new PropertyCache();

            // Assert ...
            Assert.IsTrue(result._cache != null, "The cache wasn't properly initialized.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="P:CG.Reflection.PropertyCache.Item(System.Type, System.String)"/>
        /// method populates the cache for the specified type and member.
        /// </summary>
        [TestMethod]
        public void PropertyCache_indexer()
        {
            // Arrange ...
            var cache = new PropertyCache();

            // Act ...
            var result = cache[typeof(string), "Length"];

            // Assert ...
            Assert.IsTrue(result != null, "The property info was invalid.");
            Assert.IsTrue(result.Name == "Length", "The property name was invalid.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="P:CG.Reflection.PropertyCache.Item(System.Type, System.String)"/>
        /// method throws an exception if it can't find the specified member.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void PropertyCache_indexer_throw()
        {
            // Arrange ...
            var cache = new PropertyCache();

            // Act ...
            var result = cache[typeof(string), "NotReal"];

            // Assert ...
        }

        #endregion
    }
}
