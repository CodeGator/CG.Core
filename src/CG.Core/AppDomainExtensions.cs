using CG.Validations;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace CG
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="AppDomain"/>
    /// type.
    /// </summary>
    public static partial class AppDomainExtensions
    {
        /// <summary>
        /// This method gets the much friendlier name of the app-domain.
        /// </summary>
        /// <param name="appDomain">The app-domain to use for the operation.</param>
        /// <param name="stripTrailingExtension">True to strip any trailing file
        /// extension in the friendly name; false otherwise.</param>
        /// <returns>The friendly name of this application domain.</returns>
        /// <exception cref="AppDomainUnloadedException">The operation was 
        /// attempted on an unloaded app-domain.</exception>
        public static string FriendlyNameEx(
            this AppDomain appDomain,
            bool stripTrailingExtension = false
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(appDomain, nameof(appDomain));

            // Get the .NET friendly name.
            var friendlyName = AppDomain.CurrentDomain.FriendlyName;

            // Are we running in a test environment?
            if (friendlyName.Contains("Enumerating source"))
            {
                // If we get here then it most likely means we are actually running
                //   inside a unit test environment. For some reason, the standard
                //   unit test environment changes the friendly name to something 
                //   that's not so very friendly. So, let's fix that now.

                // We'll use a REGEX to try to pull the embedded file path out.
                var match = Regex.Match(friendlyName, @": Enumerating source \((?<path>.*)\)");

                // Did we find the path?
                if (match.Groups.Count >= 2)
                {
                    // Turn the friendly name into the file name.
                    friendlyName = Path.GetFileName(match.Groups[1].Value);
                }
                else
                {
                    // If we get here then we've got no friendly name because something 
                    //   we didn't anticipate has happened. So, we'll need to invent a 
                    //   reasonable friendly name now.
                    friendlyName = Path.GetFileName(
                        Process.GetCurrentProcess().MainModule.FileName
                        );
                }
            }

            // Should we automtically remove any trailing file extension?
            if (stripTrailingExtension)
            {
                // Is there a trailing file extension?
                if (friendlyName.ToLower().EndsWith(".dll") ||
                    friendlyName.ToLower().EndsWith(".exe"))
                {
                    // Strip the trailing extension.
                    friendlyName = Path.GetFileNameWithoutExtension(friendlyName);
                }
            }

            // Return the results.
            return friendlyName;
        }
    }
}
