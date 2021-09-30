using CG.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CG
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="Random"/>
    /// type.
    /// </summary>
    public static partial class RandomExtensions
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains alphanumeric characters for generating strings.
        /// </summary>
        private static readonly char[] _chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method generates a string of pseudo random alphanumeric 
        /// characters, or the specified length.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="size">The number of characters to include in the 
        /// string.</param>
        /// <returns>A pseudo random alphanumeric string.</returns>
        /// <remarks>
        /// <para>
        /// Do NOT use this method for cryptographic purposes.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example shows how to call the <see cref="RandomExtensions.GetNextString(Random, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = new Random();
        ///         var str = random.GetNextString(10);
        /// 
        ///         // str contains a pseudo random string.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string GetNextString(
            this Random random,
            int size
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random))
                .ThrowIfLessThanOrEqualZero(size, nameof(size));

            // Get some random bytes.
            var data = new byte[4 * size];
            random.NextBytes(data);
            
            // Loop and convert the bytes to characters.
            StringBuilder sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * 4);

                // Convert the byte to a character.
                var idx = rnd % _chars.Length;

                // Add the character to the string.
                sb.Append(_chars[idx]);
            }

            // Get the resulting string.
            var result = sb.ToString();

            // Return the results.
            return result;
        }

        #endregion
    }
}
