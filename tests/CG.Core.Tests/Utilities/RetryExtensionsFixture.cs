using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.Utilities
{
    /// <summary>
    /// This class is a test fixture for the <see cref="RetryExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class RetryExtensionsFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.Execute(IRetry, Action, Func{Exception, bool}, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public void RetryExtensions_Execute()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            Retry.Instance().Execute(
                () => { if (times++ < 1) throw new Exception(); },
                (ex) => { return true; }, 
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.Execute(IRetry, Action, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public void RetryExtensions_Execute2()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            Retry.Instance().Execute(
                () => { if (times++ < 1) throw new Exception(); },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.Execute{T}(IRetry, Func{T}, Func{Exception, bool}, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public void RetryExtensions_Execute3()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            var result = Retry.Instance().Execute<string>(
                () => { if (times++ < 1) throw new Exception(); return $"{times}"; },
                (ex) => { return true; },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
            Assert.IsTrue(result == "2", "The return was invalid.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.Execute{T}(IRetry, Func{T}, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public void RetryExtensions_Execute4()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            var result = Retry.Instance().Execute<string>(
                () => { if (times++ < 1) throw new Exception(); return $"{times}"; },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
            Assert.IsTrue(result == "2", "The return was invalid.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.ExecuteAsync(IRetry, Action, Func{Exception, bool}, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public async Task RetryExtensions_ExecutAsync1()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            await Retry.Instance().ExecuteAsync(
                () => { if (times++ < 1) throw new Exception(); },
                (ex) => { return true; },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.ExecuteAsync(IRetry, Action, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public async Task RetryExtensions_ExecutAsync2()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            await Retry.Instance().ExecuteAsync(
                () => { if (times++ < 1) throw new Exception(); },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.ExecuteAsync{T}(IRetry, Func{Task{T}}, Func{Exception, bool}, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public async Task RetryExtensions_ExecutAsync3()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            var result = await Retry.Instance().ExecuteAsync<string>(
                () => { if (times++ < 1) throw new Exception(); return Task.FromResult($"{times}"); },
                (ex) => { return true; },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
            Assert.IsTrue(result == "2", "The return was invalid.");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="RetryExtensions.ExecuteAsync{T}(IRetry, Func{Task{T}}, int, int)"/> 
        /// method. Here we verify that the method retries a work item.
        /// </summary>
        [TestMethod]
        public async Task RetryExtensions_ExecutAsync4()
        {
            // Arrange ...
            var times = 0;

            // Act ...
            var result = await Retry.Instance().ExecuteAsync<string>(
                () => { if (times++ < 1) throw new Exception(); return Task.FromResult($"{times}"); },
                3,
                0
                );

            // Assert ...
            Assert.IsTrue(times == 2, "The method didn't retry.");
            Assert.IsTrue(result == "2", "The return was invalid.");
        }

        #endregion
    }
}
