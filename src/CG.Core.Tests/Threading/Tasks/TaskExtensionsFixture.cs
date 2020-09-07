using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.Threading.Tasks
{
    /// <summary>
    /// This class is a test fixture for the <see cref="TaskExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class TaskExtensionsFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TaskExtensions.WaitAll(System.Collections.Generic.IEnumerable{System.Threading.Tasks.Task}, int, int, System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method waits for all tasks to finish
        /// before returning.
        /// </summary>
        [TestMethod]
        public void TaskExtensions_WaitAll()
        {
            // Arrange ...
            var finishedA = false;
            var finishedB = false;
            var tasks = new Task[]
            {
                new Task(() => { Task.Delay(100).Wait(); finishedA = true; }),
                new Task(() => { Task.Delay(150).Wait(); finishedB = true; }),
            };

            // Act ...
            tasks.WaitAll(2);

            // Assert ...
            Assert.IsTrue(finishedA, "the first task didn't finish");
            Assert.IsTrue(finishedB, "the second task didn't finish");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TaskExtensions.WhenAll(System.Collections.Generic.IEnumerable{Task}, int, int, System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method waits for all tasks to finish
        /// before returning.
        /// </summary>
        [TestMethod]
        public async Task TaskExtensions_WhenAll()
        {
            // Arrange ...
            var finishedA = false;
            var finishedB = false;
            var tasks = new Task[]
            {
                new Task(() => { Task.Delay(100).Wait(); finishedA = true; }),
                new Task(() => { Task.Delay(150).Wait(); finishedB = true; }),
            };

            // Act ...
            await tasks.WhenAll(2);

            // Assert ...
            Assert.IsTrue(finishedA, "the first task didn't finish");
            Assert.IsTrue(finishedB, "the second task didn't finish");
        }

        #endregion
    }
}
