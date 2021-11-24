using CG.Validations;
using System.Text;

// I found this idea here: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings

namespace System.Security.Cryptography
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="RandomNumberGenerator"/>
    /// type.
    /// </summary>
    public static partial class RandomNumberGeneratorExtensions
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains mixed case alphanumeric characters for generating strings.
        /// </summary>
        private static readonly char[] _allChars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        /// <summary>
        /// This field contains lower case alpha characters for generating strings.
        /// </summary>
        private static readonly char[] _lowerChars =
            "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <summary>
        /// This field contains upper case alpha characters for generating strings.
        /// </summary>
        private static readonly char[] _upperChars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        /// <summary>
        /// This field contains numeric characters for generating strings.
        /// </summary>
        private static readonly char[] _numChars = "1234567890".ToCharArray();

        /// <summary>
        /// This field contains symbol characters for generating strings.
        /// </summary>
        private static readonly char[] _symbChars = 
            "~!@#$%^&*()[];:<>,.-=_+".ToCharArray();

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method generates a string of random alphanumeric characters.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="size">The number of characters to include in the 
        /// string.</param>
        /// <returns>A random alphanumeric string.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextString(RandomNumberGenerator, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var str = random.NextString(10);
        /// 
        ///         // str contains a 10 character string.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string NextString(
            this RandomNumberGenerator random,
            int size
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random))
                .ThrowIfLessThanOrEqualZero(size, nameof(size));

            // Get some random bytes.
            byte[] data = new byte[sizeof(int) * size];
            random.GetBytes(data);

            // Loop and convert the bytes to characters.
            var sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * sizeof(int));

                // Convert the byte to an index.
                var idx = rnd % _allChars.Length;

                // Add the corresponding character to the string.
                sb.Append(_allChars[idx]);
            }

            // Get the resulting string.
            var result = sb.ToString();

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a random integer who's value has been constrained
        /// to be within the range specified by the <paramref name="min"/> and
        /// <paramref name="max"/> parameters.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="min">The lower value for the range.</param>
        /// <param name="max">The upper value for the range.</param>
        /// <returns>A randon integer who's value has been constrained
        /// to be within the range specified by the <paramref name="min"/> and
        /// <paramref name="max"/> parameters.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.Next(RandomNumberGenerator, int, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var num = random.Next(1, 10);
        /// 
        ///         // num contains a number between 1 and 10.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static int Next(
            this RandomNumberGenerator random,
            int min,
            int max
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random));

            // Fixup max.
            max--;

            // Get some random bytes.
            var bytes = new byte[sizeof(int)];
            random.GetNonZeroBytes(bytes);

            // Convert to a value.
            var val = BitConverter.ToInt32(bytes);

            // Constrain the value.
            var result = ((val - min) % (max - min + 1) + (max - min + 1)) %
                (max - min + 1) + min;

            // Return the results.
            return result;

        }

        // *******************************************************************

        /// <summary>
        /// This method returns a random integer value.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <returns>A random integer value</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.Next(RandomNumberGenerator)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var num = random.Next();
        /// 
        ///         // num contains a number.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static int Next(
            this RandomNumberGenerator random
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random));

            // Get some random bytes.
            var bytes = new byte[sizeof(int)];
            random.GetNonZeroBytes(bytes);

            // Convert to a value.
            var result = BitConverter.ToInt32(bytes);

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a random numeric value.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="size">The number of characters to include in the 
        /// string.</param>
        /// <returns>A random numeric string.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextDigits(RandomNumberGenerator, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var num = random.NextDigits(3);
        /// 
        ///         // num contains random numbers.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string NextDigits(
            this RandomNumberGenerator random,
            int size
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random));

            // Get some random bytes.
            byte[] data = new byte[sizeof(int) * size];
            random.GetBytes(data);

            // Loop and convert the bytes to characters.
            var sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * sizeof(int));

                // Convert the byte to an index.
                var idx = rnd % _numChars.Length;

                // Add the corresponding character to the string.
                sb.Append(_numChars[idx]);
            }

            // Get the resulting string.
            var result = sb.ToString();

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string with random symbols.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="size">The number of symbols to include in the string.</param>
        /// <returns>A random symbolic string.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextSymbols(RandomNumberGenerator, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var num = random.NextSymbols(3);
        /// 
        ///         // num contains random symbols.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string NextSymbols(
            this RandomNumberGenerator random,
            int size
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random));

            // Get some random bytes.
            byte[] data = new byte[sizeof(int) * size];
            random.GetBytes(data);

            // Loop and convert the bytes to characters.
            var sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * sizeof(int));

                // Convert the byte to an index.
                var idx = rnd % _symbChars.Length;

                // Add the corresponding character to the string.
                sb.Append(_symbChars[idx]);
            }

            // Get the resulting string.
            var result = sb.ToString();

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string with upper case alpha characters.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="size">The number of characters to include in the string.</param>
        /// <returns>A random alpha string.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextUpper(RandomNumberGenerator, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var num = random.NextUpper(3);
        /// 
        ///         // num contains random upper case alpha characters.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string NextUpper(
            this RandomNumberGenerator random,
            int size
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random));

            // Get some random bytes.
            byte[] data = new byte[sizeof(int) * size];
            random.GetBytes(data);

            // Loop and convert the bytes to characters.
            var sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * sizeof(int));

                // Convert the byte to an index.
                var idx = rnd % _upperChars.Length;

                // Add the corresponding character to the string.
                sb.Append(_upperChars[idx]);
            }

            // Get the resulting string.
            var result = sb.ToString();

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string with lower case alpha characters.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="size">The number of characters to include in the string.</param>
        /// <returns>A random alpha string.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextLower(RandomNumberGenerator, int)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var num = random.NextLower(3);
        /// 
        ///         // num contains random lower case alpha characters.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string NextLower(
            this RandomNumberGenerator random,
            int size
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random));

            // Get some random bytes.
            byte[] data = new byte[sizeof(int) * size];
            random.GetBytes(data);

            // Loop and convert the bytes to characters.
            var sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * sizeof(int));

                // Convert the byte to an index.
                var idx = rnd % _lowerChars.Length;

                // Add the corresponding character to the string.
                sb.Append(_lowerChars[idx]);
            }

            // Get the resulting string.
            var result = sb.ToString();

            // Return the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string with randomly shuffled characters.
        /// </summary>
        /// <param name="random">The random number generator to use for the 
        /// operation.</param>
        /// <param name="source">The string to be shuffled.</param>
        /// <returns>A shuffled string.</returns>
        /// <example>
        /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.Shuffle(RandomNumberGenerator, string)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var random = RandomNumberGenerator.Create();
        ///         var str = random.Shuffle("this is a test");
        /// 
        ///         // str contains randomly shuffled characters.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static string Shuffle(
            this RandomNumberGenerator random,
            string source
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(random, nameof(random))
                .ThrowIfNullOrEmpty(source, nameof(source));

            // Get some random bytes.
            byte[] data = new byte[sizeof(int) * source.Length];
            random.GetBytes(data);

            // Loop through the bytes.
            var indexes = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                // Get the next byte.
                var rnd = BitConverter.ToUInt32(data, i * sizeof(int));

                // Convert the byte to an index.
                var idx = Math.Abs(rnd % indexes.Length);

                // Save the index.
                indexes[i] = (int)idx;
            }

            // Create a place to tweak the characters.
            var sb = new StringBuilder(source);

            // Loop in from the start.
            for (var x = 0; x < indexes.Length / 2; x++)
            {
                // Loop in from the end.
                for (var y = indexes.Length / 2; y > 0; y--)
                {
                    // Get the chars.
                    var oldX = sb[x];
                    var oldY = sb[y];

                    // Swap the chars.
                    sb[x] = oldY;
                    sb[y] = oldX;
                }
            }

            // Return the results.
            return sb.ToString();
        }

        #endregion
    }
}
