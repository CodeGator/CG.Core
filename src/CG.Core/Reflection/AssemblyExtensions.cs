
namespace System.Reflection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="Assembly"/>
    /// type.
    /// </summary>
    public static partial class AssemblyExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// Reads the value of the <see cref="AssemblyFileVersionAttribute"/>
        /// attribute for the given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to read from.</param>
        /// <returns>The value of the given assembly's file version attribute.</returns>
        public static string ReadFileVersion(this Assembly assembly)
        {
            // Attempt to read the assembly's file version attribute.
            object[] attributes = assembly.GetCustomAttributes(
                typeof(AssemblyFileVersionAttribute),
                true
                );

            // Did we fail?
            if (attributes.Length == 0)
                return string.Empty;

            // Attempt to recover a reference to the attribute.
            AssemblyFileVersionAttribute attr =
                attributes[0] as AssemblyFileVersionAttribute;

            // Did we fail?
            if (attr == null || attr.Version.Length == 0)
                return string.Empty;

            // Return the text for the attribute.
            return attr.Version;
        }

        #endregion
    }
}
