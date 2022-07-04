using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Text.Json;

/// <summary>
/// This class containts extension methods related to the <see cref="JsonSerializer"/>
/// type.
/// </summary>
public static partial class JsonSerializerExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method deserializes the given JSON string into a c# anonymous type.
    /// </summary>
    /// <typeparam name="T">The anonymous type.</typeparam>
    /// <param name="json">The JSON to use for the operation.</param>
    /// <param name="anonymousTypeObject">The anonymous type.</param>
    /// <param name="options">The options to use for the operation.</param>
    /// <returns>An instance of type <typeparamref name="T"/></returns>
    public static T DeserializeAnonymousType<T>(
        this string json,
        T anonymousTypeObject,
        JsonSerializerOptions options = default
        ) => JsonSerializer.Deserialize<T>(
            json,
            options
            );

    // *******************************************************************

    /// <summary>
    /// This method deserializes the given <see cref="Stream"/> into a c# anonymous type.
    /// </summary>
    /// <typeparam name="T">The anonymous type.</typeparam>
    /// <param name="stream">The stream to use for the operation.</param>
    /// <param name="anonymousTypeObject">The anonymous type.</param>
    /// <param name="options">The options to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An instance of type <typeparamref name="T"/></returns>
    public static ValueTask<T> DeserializeAnonymousTypeAsync<T>(
        this Stream stream,
        T anonymousTypeObject,
        JsonSerializerOptions options = default,
        CancellationToken cancellationToken = default
        ) => JsonSerializer.DeserializeAsync<T>(
            stream,
            options,
            cancellationToken
            );

    #endregion
}
