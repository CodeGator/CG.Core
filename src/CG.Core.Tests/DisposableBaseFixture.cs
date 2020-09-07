using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CG
{
    /// <summary>
    /// This class is a test fixture for the <see cref="DisposableBase"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class DisposableBaseFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing purposes.
        /// </summary>
        class TestClass : DisposableBase
        {
            /// <summary>
            /// This property is used for internal testing purposes.
            /// </summary>
            public bool DisposeCalled { get; private set; }

            /// <summary>
            /// This method is used for internal testing purposes.
            /// </summary>
            protected override void Dispose(
                bool disposing
                )
            {
                base.Dispose(disposing);
                DisposeCalled = true;
            }
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that OnDispose is called whenever a <see cref="DisposableBase"/>
        /// is disposed by the .net framework.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit")]
        public void DisposeCallsOnDispose()
        {
            // Arrange ...

            // Act ...
            var obj = new TestClass();
            using (obj)
            {
                // Assert ...
                Assert.IsFalse(obj.DisposeCalled, "The Dispose was called before it's time!");
            }

            // Assert ...
            Assert.IsTrue(obj.DisposeCalled, "The Dispose method wasn't called after GC cleanup!");
        }

        #endregion
    }
}
