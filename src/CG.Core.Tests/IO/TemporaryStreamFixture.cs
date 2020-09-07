using System;
using System.IO;
using CG.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.IO
{
    /// <summary>
    /// This class is a test fixture for the <see cref="TemporaryStream"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class TemporaryStreamFixture
    {
        // *******************************************************************
        // Test methods.
        // *******************************************************************

        #region Test methods

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.TemporaryStream(string)"/> 
        /// method. Here we verify that the constructor properly initializes 
        /// object instances.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_ctor1()
        {
            // Arrange ...

            // Act ...
            using (var result = new TemporaryStream())
            {
                // Assert ...
                Assert.IsTrue(result.Name.Length > 0, "The stream name was invalid.");
                Assert.IsTrue(result.CanRead, "The stream is write only.");
                Assert.IsTrue(result.CanWrite, "The stream is read only.");
                Assert.IsTrue(result.CanSeek, "The stream is unable to seek.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Length"/> 
        /// property returns the number of bytes in the stream.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Length()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                stream.Write(new byte[] { 0, 1, 2, 3 });

                // Act ...
                var result = stream.Length;

                // Assert ...
                Assert.IsTrue(result == 4, "The stream length was invalid.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Position"/> 
        /// property returns the current position of the stream.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Position1()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                stream.Write(new byte[] { 0, 1, 2, 3 });

                // Act ...
                var result = stream.Position;

                // Assert ...
                Assert.IsTrue(result == 4, "The stream position was invalid.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Position"/> 
        /// property sets the current position of the stream.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Position2()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                stream.Write(new byte[] { 0, 1, 2, 3 });

                // Act ...
                stream.Position = 2;

                // Assert ...
                var result = stream.Position;
                Assert.IsTrue(result == 2, "The stream position was invalid.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Read"/> 
        /// property reads data from the stream into the specified buffer.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Read()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                stream.Write(new byte[] { 0, 1, 2, 3 });
                stream.Position = 0;

                var buffer = new byte[10];

                // Act ...
                var result = stream.Read(buffer, 0, buffer.Length);

                // Assert ...
                Assert.IsTrue(result == 4, "The stream failed to read the entire buffer.");
                Assert.IsTrue(buffer[0] == 0, "The byte 0 was invalid.");
                Assert.IsTrue(buffer[1] == 1, "The byte 0 was invalid.");
                Assert.IsTrue(buffer[2] == 2, "The byte 0 was invalid.");
                Assert.IsTrue(buffer[3] == 3, "The byte 0 was invalid.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Seek"/> 
        /// property positions the stream.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Seek()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                stream.Write(new byte[] { 0, 1, 2, 3 });

                // Act ...
                stream.Seek(1, SeekOrigin.Begin);

                // Assert ...
                Assert.IsTrue(stream.Position == 1, "The stream failed to seek properly.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.SetLength"/> 
        /// property sets the size of the stream.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_SetLength()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                // Act ...
                stream.SetLength(100);

                // Assert ...
                Assert.IsTrue(stream.Length == 100, "The stream failed to resize properly.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Write"/> 
        /// method writes data to the stream.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Write()
        {
            // Arrange ...
            using (var stream = new TemporaryStream())
            {
                // Act ...
                stream.Write(new byte[] { 0, 1, 2, 3 });

                // Assert ...
                Assert.IsTrue(stream.Length == 4, "The stream failed to write data properly.");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Name"/> 
        /// property contains the path to the underlying temp file.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Name()
        {
            // Arrange ...

            // Act ...
            using (var stream = new TemporaryStream())
            {
                // Assert ...
                Assert.IsTrue(stream.Name != "", "The stream name was invalid.");
            }            
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="TemporaryStream.Dispose"/> 
        /// method cleans up the underlying temp file.
        /// </summary>
        [TestMethod]
        public void TemporaryStream_Dispose()
        {
            // Arrange ...
            var file = "";

            // Act ...
            using (var stream = new TemporaryStream())
            {
                file = stream.Name;
            }

            // Assert ...
            Assert.IsFalse(System.IO.File.Exists(file), "The stream failed to cleanup the file.");
        }

        #endregion
    }
}
