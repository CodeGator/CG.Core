
namespace System;

/// <summary>
/// This class contains extension methods related to the <see cref="string"/>
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
    /// false if they don't. The acceptable wild card symbols are: * to 
    /// match zero or more characters, or, ? to match a single character.
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

        bool result;

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
            // Make the comparison the old fashioned way.
            result = lhs.Equals(rhs);
        }

        // Returning the results.
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method checks for embedded HTML in a string.
    /// </summary>
    /// <param name="value">The string to use for the operation.</param>
    /// <returns>True if the string contains HTML; false otherwise.</returns>
    public static bool IsHTML(
        this string value
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(value, nameof(value));

        // Create the regex's for the check.
        var regex1 = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
        var regex2 = new Regex(@"<[^>]+>");

        // Perform the check.
        var hasHtml = regex1.IsMatch(value) || regex2.IsMatch(value);

        // Return the results.
        return hasHtml;
    }

    // *******************************************************************

    /// <summary>
    /// This method randomly shuffles the characters in a string.
    /// </summary>
    /// <param name="value">The string to be shuffled.</param>
    /// <returns>A randomly shuffled version of the specified string.</returns>
    public static string Shuffle(
        this string value
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(value, nameof(value));

        int index;

        var random = new RandomEx();
        var chars = new List<char>(value);
        var sb = new StringBuilder();

        // Loop through all the characters.
        while (chars.Count > 0)
        {
            // Calculate a random index.
            index = random.Next(chars.Count);

            // Append the character to our new string.
            sb.Append(chars[index]);

            // Remove the character from our old string.
            chars.RemoveAt(index);
        }

        // Return the results.
        return sb.ToString();
    }

    // *******************************************************************

    /// <summary>
    /// This method obfuscates characters in the specified string.
    /// </summary>
    /// <param name="value">The string to obfuscate.</param>
    /// <param name="maxCharsToShow">The number of chars to leave exposed.</param>
    /// <returns>The obfuscated string.</returns>
    public static string Obfuscate(
        this string value,
        int maxCharsToShow = 4
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(value, nameof(value))
            .ThrowIfLessThanZero(maxCharsToShow, nameof(maxCharsToShow))
            .ThrowIfGreaterThan(maxCharsToShow, value.Length, nameof(maxCharsToShow));

        var sb = new StringBuilder(value);

        // Calculate the maximum number of characters we can expose.
        var maxChars = Math.Max(sb.Length, maxCharsToShow);

        // Loop and obfuscate chars
        for (var x = maxChars; x < value.Length; x++)
        {
            sb[x] = '*';
        }

        // Return the results.
        return sb.ToString();
    }

    // *******************************************************************

    /// <summary>
    /// This method calculates a hamming distance between two strings.
    /// </summary>
    /// <param name="lhs">The left hand side of the operation.</param>
    /// <param name="rhs">The right hand side of the operation.</param>
    /// <returns>A hamming distance.</returns>
    public static int HammingDistance(
        this string lhs,
        string rhs
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(lhs, nameof(lhs))
            .ThrowIfNull(rhs, nameof(rhs));

        // Pad one of the strings to the length of the other so we
        //   can calculate the hamming distance.

        if (lhs.Length > rhs.Length)
        {
            rhs += rhs.PadRight(lhs.Length - rhs.Length);
        }
        else if (rhs.Length > lhs.Length)
        {
            lhs += lhs.PadRight(rhs.Length - lhs.Length);
        }

        // Make the calculation.
        int distance =
            lhs.ToCharArray().Zip(
                rhs.ToCharArray(), (x1, x2) => new { x1, x2 }
                ).Count(x => x.x1 != x.x2);

        // Return the results.
        return distance;
    }

    // *******************************************************************

    /// <summary>
    /// This method encodes the given string with base-64 encoding.
    /// </summary>
    /// <param name="value">The value to be encoded.</param>
    /// <returns>The SHA512 hash for the <paramref name="value"/> parameter.</returns>
    public static string ToBase64(
        this string value
        )
    {

        // Encode the string.
        var base64 = Convert.ToBase64String(
            Convert.FromBase64String(value)
            );

        // Return the result.
        return base64;
    }

    // *******************************************************************

    /// <summary>
    /// This method reverses the characters in a string.
    /// </summary>
    /// <param name="value">The string to be reversed.</param>
    /// <returns>A reversed version of the specified string.</returns>
    public static string Reverse(
        this string value
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(value, nameof(value));

        // Reverse the characters.
        var reversedChars = value.ToCharArray()
            .Reverse()
            .ToArray();

        // Make a new string.
        var result = new string(
            reversedChars,
            0,
            reversedChars.Length
            );

        // Return the results.
        return result;
    }

    #endregion
}
