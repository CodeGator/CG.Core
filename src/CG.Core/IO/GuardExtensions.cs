using CG.Core.Properties;
using CG.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CG.IO
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
        /// argument contains a value that is not a readable stream.
        /// </summary>
        /// <param name="guard">The guard instance to use for the operation.</param>
        /// <param name="argValue">The argument to test.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="memberName">Not used. Supplied by the compiler.</param>
        /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
        /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
        /// <returns>The <paramref name="guard"/> value.</returns>
        /// <exception cref="ArgumentException">This exception is thrown when
        /// the <paramref name="argValue"/> argument contains a stream that 
        /// is not readable.
        /// </exception>
        /// <example>
        /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotReadable(IGuard, Stream, string, string, string, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         // make an invalid argument.
        ///         var arg = new FileStream("test.doc", FileMode.Open, FileAccess.Write);
        /// 
        ///         // throws an exception, since the stream is not readable.
        ///         Guard.Instance().ThrowIfNotReadable(arg, nameof(arg));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IGuard ThrowIfNotReadable(
            this IGuard guard,
            Stream argValue,
            string argName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            // Make the test.
            if (!argValue.CanRead)
            {
                // Panic!!!
                throw new ArgumentException(
                    message: string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Guard_ArgNotReadable,
                        memberName,
                        sourceFilePath,
                        sourceLineNumber
                        ),
                    paramName: argName
                    );
            }

            // Return the guard instance.
            return guard;
        }

        // ******************************************************************

        /// <summary>
        /// This method throws an exception if the <paramref name="argValue"/>
        /// argument contains an invalid file path.
        /// </summary>
        /// <param name="guard">The guard instance to use for the operation.</param>
        /// <param name="argValue">The argument to test.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="memberName">Not used. Supplied by the compiler.</param>
        /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
        /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
        /// <returns>The <paramref name="guard"/> value.</returns>
        /// <exception cref="ArgumentException">This exception is thrown when
        /// the <paramref name="argValue"/> argument contains an invalid file
        /// path.
        /// </exception>
        /// <example>
        /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidFilePath(IGuard, string, string, string, string, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         // make an invalid argument.
        ///         var arg = "*";
        /// 
        ///         // throws an exception, since the file path is invalid.
        ///         Guard.Instance().ThrowIfInvalidFilePath(arg, nameof(arg));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IGuard ThrowIfInvalidFilePath(
            this IGuard guard,
            string argValue,
            string argName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            // Make the check.
            if (Path.GetFileName(argValue).IndexOfAny(Path.GetInvalidFileNameChars(), 0) != -1 ||
                Path.GetDirectoryName(argValue).IndexOfAny(Path.GetInvalidPathChars(), 0) != -1)
            {
                // Panic!!!
                throw new ArgumentException(
                    message: string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Guard_InvalidFilePath,
                        memberName,
                        sourceFilePath,
                        sourceLineNumber
                        ),
                    paramName: argName
                    );
            }

            // Return the guard instance.
            return guard;
        }

        // ******************************************************************

        /// <summary>
        /// This method throws an exception if the <paramref name="argValue"/> 
        /// argument contains an invalid folder path.
        /// </summary>
        /// <param name="guard">The guard instance to use for the operation.</param>
        /// <param name="argValue">The argument to test.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="memberName">Not used. Supplied by the compiler.</param>
        /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
        /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
        /// <returns>The <paramref name="guard"/> value.</returns>
        /// <exception cref="ArgumentException">This exception is thrown when
        /// the <paramref name="argValue"/> argument contains an invalid folder
        /// path.
        /// </exception>
        /// <example>
        /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidFolderPath(IGuard, string, string, string, string, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         // make an invalid argument.
        ///         var arg = "*";
        /// 
        ///         // throws an exception, since the folder path is invalid.
        ///         Guard.Instance().ThrowIfInvalidFolderPath(arg, nameof(arg));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IGuard ThrowIfInvalidFolderPath(
            this IGuard guard,
            string argValue,
            string argName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            // Make the check.
            if (argValue.IndexOfAny(Path.GetInvalidPathChars(), 0) != -1)
            {
                // Panic!!!
                throw new ArgumentException(
                    message: string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Guard_InvalidFolderPath,
                        memberName,
                        sourceFilePath,
                        sourceLineNumber
                        ),
                    paramName: argName
                    );
            }

            // Return the guard instance.
            return guard;
        }

        // ******************************************************************

        /// <summary>
        /// This method throws an exception if the <paramref name="argValue"/>
        /// argument contains an invalid file extension.
        /// </summary>
        /// <param name="guard">The guard instance to use for the operation.</param>
        /// <param name="argValue">The argument to test.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="memberName">Not used. Supplied by the compiler.</param>
        /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
        /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
        /// <returns>The <paramref name="guard"/> value.</returns>
        /// <exception cref="ArgumentException">This exception is thrown when
        /// the <paramref name="argValue"/> argument contains an invalid file
        /// extension.
        /// </exception>
        /// <example>
        /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidFileExtension(IGuard, string, string, string, string, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         // make an invalid argument.
        ///         var arg = "*";
        /// 
        ///         // throws an exception, since the file extension is invalid.
        ///         Guard.Instance().ThrowIfInvalidFolderPath(arg, nameof(arg));
        ///    }
        /// }
        /// </code>
        /// </example>
        public static IGuard ThrowIfInvalidFileExtension(
            this IGuard guard,
            string argValue,
            string argName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            // Make the check.
            if (!argValue.Contains(".") ||
                argValue.Trim().Length <= 1 ||
                Path.GetExtension(argValue).IndexOfAny(Path.GetInvalidFileNameChars(), 0) != -1)
            {
                // Panic!!!
                throw new ArgumentException(
                    message: string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Guard_InvalidExtension,
                        memberName,
                        sourceFilePath,
                        sourceLineNumber
                        ),
                    paramName: argName
                    );
            }

            // Return the guard instance.
            return guard;
        }

        // *******************************************************************

        /// <summary>
        /// This method throws an exception if the <paramref name="argValue"/>
        /// argument contains a value that is not a writable stream.
        /// </summary>
        /// <param name="guard">The guard instance to use for the operation.</param>
        /// <param name="argValue">The argument to test.</param>
        /// <param name="argName">The name of the argument.</param>
        /// <param name="memberName">Not used. Supplied by the compiler.</param>
        /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
        /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
        /// <returns>The <paramref name="guard"/> value.</returns>
        /// <exception cref="ArgumentException">This exception is thrown when
        /// the <paramref name="argValue"/> argument contains a stream that 
        /// is not writable.
        /// </exception>
        /// <example>
        /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotWritable(IGuard, Stream, string, string, string, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         // make an invalid argument.
        ///         var arg = new FileStream("test.doc", FileMode.Open, FileAccess.Read);
        /// 
        ///         // throws an exception, since the stream is not writeable.
        ///         Guard.Instance().ThrowIfNotWritable(arg, nameof(arg));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IGuard ThrowIfNotWritable(
            this IGuard guard,
            Stream argValue,
            string argName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            // Make the test.
            if (!argValue.CanWrite)
            {
                // Panic!!!
                throw new ArgumentException(
                    message: string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Guard_ArgNotWritable,
                        memberName,
                        sourceFilePath,
                        sourceLineNumber
                        ),
                    paramName: argName
                    );
            }

            // Return the guard instance.
            return guard;
        }

        #endregion
    }
}
