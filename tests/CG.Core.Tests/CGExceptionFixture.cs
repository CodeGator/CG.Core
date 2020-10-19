using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG
{
    /// <summary>
    /// This class is a QA fixture for the <see cref="CGException"/>
    /// class.
    /// </summary>
    [TestCategory("Unit")]
    [TestClass]
    public class CGExceptionFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="CGException.CGException"/>
        /// constructor creates an initialized object.
        /// </summary>
        [TestMethod]
        public void CGException_ctor1()
        {
            // Arrange ...

            // Act ...
            var result = new CGException();
            
            // Assert ...
            Assert.IsTrue(result.Message == "Exception of type 'CG.CGException' was thrown.", "the message was invalid");
            Assert.IsTrue(result.InnerException == null, "the inner exception was invalid");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="CGException.CGException(string)"/>
        /// constructor creates an initialized object.
        /// </summary>
        [TestMethod]
        public void CGException_ctor2()
        {
            // Arrange ...

            // Act ...
            var result = new CGException("test");

            // Assert ...
            Assert.IsTrue(result.Message == "test", "the message was invalid");
            Assert.IsTrue(result.InnerException == null, "the inner exception was invalid");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="CGException.CGException(string, Exception)"/>
        /// constructor creates an initialized object.
        /// </summary>
        [TestMethod]
        public void CGException_ctor3()
        {
            // Arrange ...

            // Act ...
            var result = new CGException("test", new Exception());

            // Assert ...
            Assert.IsTrue(result.Message == "test", "the message was invalid");
            Assert.IsTrue(result.InnerException != null, "the inner exception was invalid");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="CGException.CGException(System.Runtime.Serialization.SerializationInfo, System.Runtime.Serialization.StreamingContext)"/>
        /// constructor creates an initialized object.
        /// </summary>
        [TestMethod]
        public void CGException_ctor4()
        {
            // Arrange ...
            var original = new CGException("test", new Exception());

            using (var memStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memStream, original);

                memStream.Seek(0, SeekOrigin.Begin);

                // Act ...
                var result = formatter.Deserialize(memStream) as CGException;

                // Assert ...
                Assert.IsTrue(result.Message == "test", "the message was invalid");
                Assert.IsTrue(result.InnerException != null, "the inner exception was invalid");
            }
        }

        #endregion
    }
}
