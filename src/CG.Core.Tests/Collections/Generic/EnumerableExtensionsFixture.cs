using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.Collections.Generic
{
    /// <summary>
    /// This class is a test fixture for the <see cref="EnumerableExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class EnumerableExtensionsFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        class TestType
        {
            public int Id { get; set; }
            public TestType[] C { get; set; }
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="EnumerableExtensions.ApplyWhiteList{T}(IEnumerable{T}, Func{T, string}, string)"/>
        /// method properly applies a white list.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ApplyWhiteList()
        {
            // Arrange ...
            var list = new List<string>()
            {
                "0", "1", "2", "3", "4"
            };

            // Act ...
            var result = list.ApplyWhiteList(x => x, "1,3,5");

            // Assert ...
            Assert.IsTrue(result.Count() == 2, "failed to apply a white list.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="EnumerableExtensions.ApplyBlackList{T}(IEnumerable{T}, Func{T, string}, string)"/>
        /// method properly applies a black list.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ApplyBlackList()
        {
            // Arrange ...
            var list = new List<string>()
            {
                "0", "1", "2", "3", "4", "5"
            };

            // Act ...
            var result = list.ApplyBlackList(x => x, "1,3,5");

            // Assert ...
            Assert.IsTrue(result.Count() == 3, "failed to apply a black list.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{T})"/>
        /// method iterates through the items and calls the delegate for each one.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ForEach()
        {
            // Arrange ...
            var counter = 0;
            var list = new List<int>()
            {
                0, 1, 2, 3, 4
            }.AsEnumerable();

            // Act ...
            list.ForEach(x =>
            {
                counter++;
            });

            // Assert ...
            Assert.IsTrue(counter == 5, "failed to call the delegate for one or more items.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Func{T, IEnumerable{T}}, Action{T})"/>
        /// method recursively iterates through the items and calls the delegate
        /// for each one.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_ForEach2()
        {
            // Arrange ...
            var counter = 0;
            var list = new TestType[]
            {
                new TestType()
                {
                    Id = 0,
                    C = new TestType[]
                    {
                        new TestType
                        {
                            Id = 1,
                            C = new TestType[0]
                        }
                    }
                },
                new TestType()
                {
                    Id = 2,
                    C = new TestType[]
                    {
                        new TestType
                        {
                            Id = 3,
                            C = new TestType[0]
                        }
                    }
                }
            }.AsEnumerable();

            // Act ...
            list.ForEach(x => x.C, y => { counter++; });

            // Assert ...
            Assert.IsTrue(counter == 4, "failed to call the delegate for one or more items.");
        }

        #endregion
    }
}
