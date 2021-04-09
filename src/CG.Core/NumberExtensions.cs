using System;

namespace CG
{
    /// <summary>
    /// This class contains extensiom methods related to the <see cref="String"/>
    /// type.
    /// </summary>
    public static partial class NumberExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method formats the specified value as bytes.
        /// </summary>
        /// <param name="value">The value to use for the oepration.</param>
        /// <param name="decimalPlaces">The number of decimal places to use for 
        /// the operation.</param>
        /// <returns>A formatted string.</returns>
        public static string FormattedAsBytes(
            this double value,
            int decimalPlaces = 1
            )
        {
            // Call the int64 flavor.
            return FormattedAsBytes((long)value, decimalPlaces);
        }

        // *******************************************************************

        /// <summary>
        /// This method formats the specified value as bytes.
        /// </summary>
        /// <param name="value">The value to use for the oepration.</param>
        /// <param name="decimalPlaces">The number of decimal places to use for 
        /// the operation.</param>
        /// <returns>A formatted string.</returns>
        public static string FormattedAsBytes(
            this float value,
            int decimalPlaces = 1
            )
        {
            // Call the int64 flavor.
            return FormattedAsBytes((long)value, decimalPlaces);
        }

        // *******************************************************************

        /// <summary>
        /// This method formats the specified value as bytes.
        /// </summary>
        /// <param name="value">The value to use for the oepration.</param>
        /// <param name="decimalPlaces">The number of decimal places to use for 
        /// the operation.</param>
        /// <returns>A formatted string.</returns>
        public static string FormattedAsBytes(
            this short value,
            int decimalPlaces = 1
            )
        {
            // Call the int64 flavor.
            return FormattedAsBytes((long)value, decimalPlaces);
        }

        // *******************************************************************

        /// <summary>
        /// This method formats the specified value as bytes.
        /// </summary>
        /// <param name="value">The value to use for the oepration.</param>
        /// <param name="decimalPlaces">The number of decimal places to use for 
        /// the operation.</param>
        /// <returns>A formatted string.</returns>
        public static string FormattedAsBytes(
            this int value,
            int decimalPlaces = 1
            )
        {
            // Call the int64 flavor.
            return FormattedAsBytes((long)value, decimalPlaces);
        }

        // *******************************************************************

        /// <summary>
        /// This method formats the specified value as bytes.
        /// </summary>
        /// <param name="value">The value to use for the oepration.</param>
        /// <param name="decimalPlaces">The number of decimal places to use for 
        /// the operation.</param>
        /// <returns>A formatted string.</returns>
        public static string FormattedAsBytes(
            this long value, 
            int decimalPlaces = 1
            )
        {
            var SizeSuffixes = new string[] { 
                "bytes", 
                "KB", 
                "MB", 
                "GB", 
                "TB", 
                "PB", 
                "EB", 
                "ZB", 
                "YB" 
            };

            // Can we take the shorcut?
            if (value < 0) 
            { 
                return "-" + FormattedAsBytes(-value, decimalPlaces); 
            }

            // Determine the magnitude of the value.
            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            // Ensure bytes don't have decimals places.
            if (0 == i)
            {
                decimalPlaces = 0;
            }

            // Format the string.
            var formatted = string.Format(
                "{0:n" + decimalPlaces + "} {1}",
                dValue, 
                SizeSuffixes[i]
                );

            // Return the formatted string.
            return formatted;
        }

        #endregion
    }
}
