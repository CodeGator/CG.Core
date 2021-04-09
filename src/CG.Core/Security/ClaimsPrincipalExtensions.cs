using CG.Validations;
using System.Linq;

namespace System.Security.Claims
{
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
        /// wasn't found on the pricipal.</returns>
        public static string GetEmail(
            this ClaimsPrincipal principal
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(principal, nameof(principal));

            // Look for the claim on the principal.
            var claim = principal.Claims.FirstOrDefault(
                x => x.Type == "email"
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

        #endregion
    }
}
