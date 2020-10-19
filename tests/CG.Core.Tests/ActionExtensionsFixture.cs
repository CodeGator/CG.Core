using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG
{
    /// <summary>
    /// This class is a QA fixture for the <see cref="ActionExtensions"/>
    /// class.
    /// </summary>
    [TestCategory("Unit")]
    [TestClass]
    public class ActionExtensionsFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="ActionExtensions.WaitAll(System.Collections.Generic.IEnumerable{Action}, int, System.Threading.CancellationToken)"/>
        /// method waits for all the actions to finish.
        /// </summary>
        [TestMethod]
        public void ActionExtensions_WaitAll()
        {
            // Arrange ...
            var finishedA = false;
            var finishedB = false;
            var actions = new Action[]
            {
                () => { Task.Delay(100).Wait(); finishedA = true; },
                () => { Task.Delay(150).Wait(); finishedB = true; },
            };

            // Act ...
            actions.WaitAll(2);

            // Assert ...
            Assert.IsTrue(finishedA, "the first action didn't finish");
            Assert.IsTrue(finishedB, "the second action didn't finish");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ActionExtensions.WhenAll(System.Collections.Generic.IEnumerable{Action}, int, System.Threading.CancellationToken)"/>
        /// method return when all the waits for all the actions to finish.
        /// </summary>
        [TestMethod]
        public void ActionExtensions_WhenAll()
        {
            // Arrange ...
            var finishedA = false;
            var finishedB = false;
            var actions = new Action[]
            {
                () => { Task.Delay(100).Wait(); finishedA = true; },
                () => { Task.Delay(150).Wait(); finishedB = true; },
            };

            // Act ...
            actions.WhenAll(2).Wait();

            // Assert ...
            Assert.IsTrue(finishedA, "the first action didn't finish");
            Assert.IsTrue(finishedB, "the second action didn't finish");
        }

        #endregion
    }
}
