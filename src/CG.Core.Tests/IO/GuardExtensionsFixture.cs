using System;
using System.IO;
using CG.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CG.IO
{
    /// <summary>
    /// This class is a test fixture for the <see cref="GuardExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class GuardExtensionsFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing.
        /// </summary>
        class WriteOnlyStream : MemoryStream
        {
            /// <summary>
            /// This property is used for internal testing.
            /// </summary>
            public override bool CanRead { get { return false; } }
        }

        #endregion

        // *******************************************************************
        // Test methods.
        // *******************************************************************

        #region Test methods

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfInvalidFilePath(Guard, string, string)"/> 
        /// method. Here we verify that the method throws an exception if the input 
        /// contains an invalid file path.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GuardExtensions_ThrowIfInvalidFilePath_ExpectThrow()
        {
            string arg = "<";
            Guard.Instance().ThrowIfInvalidFilePath(arg, "value");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfInvalidFilePath(Guard, string, string)"/> 
        /// method. Here we verify that the method does not throw an exception 
        /// if the input contains a valid file path.
        /// </summary>
        [TestMethod]
        public void GuardExtensions_ThrowIfInvalidFilePath_ExpectNoThrow()
        {
            string arg = "some.txt";
            Guard.Instance().ThrowIfInvalidFilePath(arg, "value");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfNotWritable(Guard, Stream, string)"/> 
        /// method. Here we verify that the method does not throw an exception 
        /// if the input contains a writeable stream.
        /// </summary>
        [TestMethod]
        public void GuardExtensions_ThrowIfNotWritable_ExpectNoThrow()
        {
            using (var stream = new MemoryStream())
            {
                Guard.Instance().ThrowIfNotWritable(stream, "value");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfNotWritable(Guard, Stream, string)"/> 
        /// method. Here we verify that the method throws an exception if the 
        /// input contains a read-only stream.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GuardExtensions_ThrowIfNotWritable_ExpectThrow()
        {
            using (var stream = new MemoryStream(new byte[0], false))
            {
                Guard.Instance().ThrowIfNotWritable(stream, "value");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfNotReadable(Guard, Stream, string)"/> 
        /// method. Here we verify that the method does not throw an exception 
        /// if the input contains a readable stream.
        /// </summary>
        [TestMethod]
        public void GuardExtensions_ThrowIfNotReadable_ExpectNoThrow()
        {
            using (var stream = new MemoryStream())
            {
                Guard.Instance().ThrowIfNotReadable(stream, "value");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfNotReadable(Guard, Stream, string)"/> 
        /// method. Here we verify that the method throws an exception if the 
        /// input contains a write-only stream.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GuardExtensions_ThrowIfNotReadable_ExpectThrow()
        {
            using (var stream = new WriteOnlyStream())
            {
                Guard.Instance().ThrowIfNotReadable(stream, "value");
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfInvalidFolderPath(Guard, string, string)"/> 
        /// method. Here we verify that the method throws an exception if the 
        /// input contains an invalid folder path.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GuardExtensions_ThrowIfInvalidFolderPath_ExpectThrow()
        {
            var arg = "\a\b\n";
            Guard.Instance().ThrowIfInvalidFolderPath(arg, "value");
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="GuardExtensions.ThrowIfInvalidFolderPath(Guard, string, string)"/> 
        /// method. Here we verify that the method does not throw an exception 
        /// if the input contains a valid folder path.
        /// </summary>
        [TestMethod]
        public void GuardExtensions_ThrowIfInvalidFolderPath_ExpectNoThrow()
        {
            var arg = "c:\\folder1";
            Guard.Instance().ThrowIfInvalidFolderPath(arg, "value");
        }

        #endregion
    }
}
