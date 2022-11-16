
namespace System.Security.Cryptography;

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
    /// This method calculates an SHA256 hash for the given string.
    /// </summary>
    /// <param name="value">The value to be hashed.</param>
    /// <returns>The SHA256 hash for the <paramref name="value"/> parameter.</returns>
    public static string ToSha256(
        this string value
        )
    {
        // Can't hash an empty string.
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        // Create the hash algorithm.
        using var sha = SHA256.Create();

        // Hash the string.
        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = sha.ComputeHash(bytes);

        // Encode the hash.
        var base64 = Convert.ToBase64String(hash);

        // Return the result.
        return base64;
    }

    // *******************************************************************

    /// <summary>
    /// This method calculates an SHA512 hash for the given string.
    /// </summary>
    /// <param name="value">The value to be hashed.</param>
    /// <returns>The SHA512 hash for the <paramref name="value"/> parameter.</returns>
    public static string ToSha512(
        this string value
        )
    {
        // Can't hash an empty string.
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        // Create the hash algorithm.
        using var sha = SHA512.Create();

        // Hash the string.
        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = sha.ComputeHash(bytes);

        // Encode the hash.
        var base64 = Convert.ToBase64String(hash);

        // Return the result.
        return base64;

    }
            
    #endregion
}
