using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0414

namespace CG.Diagnostics
{
    /// <summary>
    /// This class is a test fixture for the <see cref="ObjectExtensions"/> class.
    /// </summary>   
    [TestCategory("Unit")]
    [TestClass]
    public class ObjectExtensionsFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        class TestType
        {
            public string _a1 = "a1";
            private string _a2 = "a2";
            public string B1 { get; set; } = "B1";
            private string B2 { get; set; } = "B2";

            public WeakReference _weak = new WeakReference("weak");
            public WeakReference Weak { get; set; } = new WeakReference("weak");
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetFieldValue(object, string, bool)"/>
        /// method returns returns the value when wrapped as a weak reference.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetFieldValue_WeakReference()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetFieldValue("_weak");

            // Assert ...
            Assert.IsTrue(results != null);
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetFieldValue(object, string, bool)"/>
        /// method returns null when the field is missing.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetFieldValue_MissingField()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetFieldValue("notreal");

            // Assert ...
            Assert.IsTrue(results == null);
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetFieldValue(object, string, bool)"/>
        /// method returns the value of a public field on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetFieldValue_Public()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetFieldValue("_a1");

            // Assert ...
            Assert.IsTrue(results == (object)"a1");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetFieldValue(object, string, bool)"/>
        /// method returns the value of a private field on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetFieldValue_Private()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetFieldValue("_a2", true);

            // Assert ...
            Assert.IsTrue(results == (object)"a2");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetPropertyValue(object, string, bool)"/>
        /// method returns a weak reference property value.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetPropertyValue_WeakReference()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetPropertyValue("Weak");

            // Assert ...
            Assert.IsTrue(results != null);
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetPropertyValue(object, string, bool)"/>
        /// method returns null for a missing property.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetPropertyValue_MissingProperty()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetPropertyValue("notthere");

            // Assert ...
            Assert.IsTrue(results == null);
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetPropertyValue(object, string, bool)"/>
        /// method returns the value of a public property on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetPropertyValue_Public1()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetPropertyValue("B1");

            // Assert ...
            Assert.IsTrue(results == (object)"B1");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetPropertyValue(object, string, bool)"/>
        /// method returns the value of a private property on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetPropertyValue_Private1()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetPropertyValue("B2", true);

            // Assert ...
            Assert.IsTrue(results == (object)"B2");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetPropertyValue{T}(object, string, bool)"/>
        /// method returns the value of a public property on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetPropertyValue_Public2()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetPropertyValue<string>("B1");

            // Assert ...
            Assert.IsTrue(results == "B1");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.GetPropertyValue{T}(object, string, bool)"/>
        /// method returns the value of a private property on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_GetPropertyValue_Private2()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            var results = tt.GetPropertyValue<string>("B2", true);

            // Assert ...
            Assert.IsTrue(results == "B2");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.SetFieldValue(object, string, object, bool)"/>
        /// method sets the value of a public field on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_SetFieldValue_Public()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            tt.SetFieldValue("_a1", "changed");

            // Assert ...
            Assert.IsTrue(tt._a1 == "changed");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.SetFieldValue(object, string, object, bool)"/>
        /// method sets the value of a private field on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_SetFieldValue_Private()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            tt.SetFieldValue("_a2", "changed", true);

            // Assert ...
            Assert.IsTrue(tt.GetFieldValue("_a2", true) == (object)"changed");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.SetPropertyValue(object, string, object, bool)"/>
        /// method sets the value of a public property on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_SetPropertyValue_Public()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            tt.SetPropertyValue("B1", "changed");

            // Assert ...
            Assert.IsTrue(tt.B1 == "changed");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="ObjectExtensions.SetPropertyValue(object, string, object, bool)"/>
        /// method sets the value of a private property on an object.
        /// </summary>
        [TestMethod]
        public void ObjectExtensions_SetPropertyValue_Private()
        {
            // Arrange ...
            var tt = new TestType();

            // Act ...
            tt.SetPropertyValue("B2", "changed", true);

            // Assert ...
            Assert.IsTrue(tt.GetPropertyValue("B2", true) == (object)"changed");
        }

        #endregion
    }
}
