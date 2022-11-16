
namespace System.Security.Claims;

/// <summary>
/// This class contains extension methods related to the <see cref="ClaimsPrincipal"/>
/// type.
/// </summary>
public static partial class ClaimsPrincipalExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method returns the value of the email claim, if it exists, in
    /// the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="principal">The claims to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetEmail(
        this ClaimsPrincipal principal
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(principal, nameof(principal));

        // Look for the claim on the principal.
        var claim = principal.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.Email
            );
        
        // Did we find it?
        if (null != claim)
        {
            // Return the claim value.
            return claim.Value;
        }

        // Claim not found.
        return string.Empty;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the name identifier claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetNameIdentifier(
        this ClaimsPrincipal claimsPrincipal
        )
    {
        // Look for the specified claim.
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier
            );

        // Did we find it?
        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; // Didn't find it.
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the nickname claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetNickName(
        this ClaimsPrincipal claimsPrincipal
        )
    {
        // Look for the specified claim.
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == "nickname"
            );

        // Did we find it?
        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; // Didn't find it.
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the name claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetName(
        this ClaimsPrincipal claimsPrincipal
        )
    {
        // Look for the specified claim.
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.Name
            );

        // Did we find it?
        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; // Didn't find it.
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the picture claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetPicture(
        this ClaimsPrincipal claimsPrincipal
        )
    {
        // Look for the specified claim.
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == "picture"
            );

        // Did we find it?
        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; // Didn't find it.
        }
    }

    #endregion
}
