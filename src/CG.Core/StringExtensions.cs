using CG.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CG
{
    /// <summary>
    /// This class contains extensiom methods related to the <see cref="String"/>
    /// type.
    /// </summary>
    public static partial class StringExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method compares two strings and returns true if they match; 
        /// false if they don't. The <paramref name="rhs"/> parameter may have
        /// embedded wildcard symbols in it. The acceptable wild card symbols 
        /// are: * to match zero or more characters, or, ? to match a single 
        /// character.
        /// </summary>
        /// <param name="lhs">The left hand side of the operation.</param>
        /// <param name="rhs">The right hand side of the operation.</param>
        /// <returns>The results of the comparison.</returns>
        /// <example>
        /// This example shows how to call the <see cref="StringExtensions.IsMatch(string, string)"/>
        /// method.
        /// <code>
        /// class TestClass
        /// {
        ///     static void Main()
        ///     {
        ///         var toBeMatched = "This is a string to be matched.";
        ///         var isMatch = toBeMatched.IsMatch("*a string*");
        /// 
        ///         // IsMatch is true.
        ///     }
        /// }
        /// </code>
        /// </example>
        public static bool IsMatch(
            this string lhs,
            string rhs
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(lhs, nameof(lhs))
                .ThrowIfNull(rhs, nameof(rhs));

            var result = false;

            // Does RHS contain wildcards?
            if (rhs.Contains("*") || rhs.Contains("?"))
            {
                // Convert the rhs to an equivalent regular expression.
                var regex = "^" + Regex.Escape(rhs).Replace("\\?", ".").Replace("\\*", ".*") + "$";

                // Make the comparison.
                result = Regex.IsMatch(lhs, regex);
            }

            // Otherwise, does the LHS contain wildcards?
            else if (lhs.Contains("*") || lhs.Contains("?"))
            {
                // Convert the lhs to an equivalent regular expression.
                var regex = "^" + Regex.Escape(lhs).Replace("\\?", ".").Replace("\\*", ".*") + "$";

                // Make the comparison.
                result = Regex.IsMatch(rhs, regex);
            }

            // Otherwise, just do a simple equality comparison.
            else
            {
                // Make the comparison.
                result = lhs.Equals(rhs);
            }

            // Returning the results.
            return result;
        }

        // *******************************************************************

        /// <summary>
        /// This method randomly shuffles the characters in a string.
        /// </summary>
        /// <param name="incoming">The string to be shuffled.</param>
        /// <returns>A randomly shuffled version of the specified string.</returns>
        public static string Shuffle(
            this string incoming
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(incoming, nameof(incoming));

            int index;

            var random = new Random(Guid.NewGuid().GetHashCode());
            var chars = new List<char>(incoming);
            var sb = new StringBuilder();

            // Loop through all the characters.
            while (chars.Count > 0)
            {
                // Calculate a random nidex.
                index = random.Next(chars.Count);

                // Append the character to our new string.
                sb.Append(chars[index]);

                // Remove the character from our old string.
                chars.RemoveAt(index);
            }

            // Return the results.
            return sb.ToString();
        }

        #endregion
    }
}
