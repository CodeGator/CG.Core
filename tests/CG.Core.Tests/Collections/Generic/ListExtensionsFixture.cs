using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.Collections.Generic
{
    /// <summary>
    /// This class is a test fixture for the <see cref="ListExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class ListExtensionsFixture
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
        /// This method verifies that the <see cref="ListExtensions.AddRange{T}(IList{T}, IEnumerable{T})"/>
        /// method adds a range of values to a list.
        /// </summary>
        [TestMethod]
        public void ListExtensions_AddRange()
        {
            // Arrange ...
            IList<int> list = new List<int>();
            IEnumerable<int> e = new int[] { 0, 1, 2 }.AsEnumerable();

            // Act ...
            list.AddRange(e);

            // Assert ...
            Assert.IsTrue(list.Count == 3, "failed to add items to a list.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ListExtensions.RemoveRange{T}(IList{T}, int, int)"/>
        /// method removes a range of values from a list.
        /// </summary>
        [TestMethod]
        public void ListExtensions_RemoveRange()
        {
            // Arrange ...
            IList<int> list = new List<int>()
            {
                0, 1, 2, 3, 4
            };
            
            // Act ...
            list.RemoveRange(0, 2);

            // Assert ...
            Assert.IsTrue(list.Count == 3, "failed to remove a range of items.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ListExtensions.ForEach{T}(IList{T}, Action{T})"/>
        /// method iterates through the items and calls the delegate for each one.
        /// </summary>
        [TestMethod]
        public void ListExtensions_ForEach()
        {
            // Arrange ...
            var counter = 0;
            var list = new List<int>()
            {
                0, 1, 2, 3, 4
            };

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
        /// This method verifies that the <see cref="ListExtensions.ForEach{T}(IList{T}, Func{T, IEnumerable{T}}, Action{T})"/>
        /// method recursively iterates through the items and calls the delegate
        /// for each one.
        /// </summary>
        [TestMethod]
        public void ListExtensions_ForEach2()
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
            };

            // Act ...
            list.ForEach(x => x.C, y => { counter++; });

            // Assert ...
            Assert.IsTrue(counter == 4, "failed to call the delegate for one or more items.");
        }

        #endregion
    }
}
