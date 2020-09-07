using CG.Core.Properties;
using CG.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CG.Collections
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IGuard"/>
    /// type.
    /// </summary>
    public static partial class GuardExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method throws an exception if the <paramref name="argValue"/> 
        /// argument contains an empty collection.
        /// </summary>
        /// <param name="guard">The guard to use for the operation.</param>
        /// <param name="argValue">The argument to test.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="memberName">Not used. Supplied by the compiler.</param>
        /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
        /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
        /// <returns>The <paramref name="guard"/> value.</returns>
        /// <exception cref="ArgumentException">This exception is thrown when
        /// the <paramref name="argValue"/> argument contains a value that is
        /// less than zero.
        /// </exception>
        /// <example>
        /// This example shows how to call the <see cref="GuardExtensions.ThrowIfEmpty{T}(IGuard, IEnumerable{T}, string, string, string, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         // make an invalid argument.
        ///         var arg = new string[0];
        /// 
        ///         // throws an exception, since the argument is invalid.
        ///         Guard.Instance().ThrowIfEmpty(arg, nameof(arg));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IGuard ThrowIfEmpty<T>(
            this IGuard guard,
            IEnumerable<T> argValue,
            string argName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            // Make the test.
            if (!argValue.Any())
            {
                // Panic!!!
                throw new ArgumentException(
                    message: string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Guard_ArgIsEmpty,
                        memberName,
                        sourceFilePath,
                        sourceLineNumber
                        ),
                    paramName: argName
                    );
            }

            // Return the guard.
            return guard;
        }

        #endregion
    }
}
