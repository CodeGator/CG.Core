#region File header
/*****************************************************************************
  Copyright © by CODEGATOR. All rights reserved.

  Permission is hereby granted, free of charge, to any person obtaining a copy 
  of this software and associated documentation files (the "Software"), to 
  deal in the Software without restriction, including without limitation the 
  rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
  sell copies of the Software, and to permit persons to whom the Software is 
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in 
  all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
  THE SOFTWARE.
   
  Have Fun! :o)
*****************************************************************************/
#endregion

#region Using directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CG.Reflection
{
    /// <summary>
    /// This class is used for internal testing purposes.
    /// </summary>
    public static class _TestHelper
    {
        /// <summary>
        /// This method is used for internal testing purposes.
        /// </summary>
        /// <param name="value">This parameter is used for internal testing purposes.</param>
        public static void TestMethod1(this int value) { }

        /// <summary>
        /// This method is used for internal testing purposes.
        /// </summary>
        /// <param name="value">This parameter is used for internal testing purposes.</param>
        /// <param name="a">This parameter is used for internal testing purposes.</param>
        public static void TestMethod2(this int value, string a = "test") { }
    }

    /// <summary>
    /// This class is a test fixture for the <see cref="AppDomainExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class AppDomainExtensionsFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method verifies that the <see cref="AppDomainExtensions.ExtensionMethods(AppDomain, Type, string, Type[], string, string)"/>
        /// method can locate a test extension method with no parameters.
        /// </summary>
        [TestMethod]
        public void AppDomainExtensions_ExtensionMethods1()
        {
            // Arrange ...

            // Act ...
            var result = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(int),
                "TestMethod1"
                );

            // Assert ...
            Assert.IsTrue(result != null, "method returned an invalid value.");
        }

        // *******************************************************************

        /// <summary>
        /// This method verifies that the <see cref="AppDomainExtensions.ExtensionMethods(AppDomain, Type, string, Type[], string, string)"/>
        /// method can locate a test extension method with parameters.
        /// </summary>
        [TestMethod]
        public void AppDomainExtensions_ExtensionMethods2()
        {
            // Arrange ...

            // Act ...
            var result = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(int),
                "TestMethod2",
                new Type[] { typeof(string) }
                );

            // Assert ...
            Assert.IsTrue(result != null, "method returned an invalid value.");
        }

        #endregion
    }
}
