﻿using CG.Validations;
using System;
using System.Text.Json;

namespace CG
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="Object"/>
    /// type.
    /// </summary>
    public static partial class ObjectExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method performs a quick clone of the specified object.
        /// </summary>
        /// <param name="source">The object to be cloned.</param>
        /// <param name="sourceType">The tye of the object to be cloned.</param>
        /// <returns>The cloned object.</returns>
        public static object QuickClone(
            this object source,
            Type sourceType
            ) 
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(source, nameof(source));

            // Serialize the object to JSON.
            var json = JsonSerializer.Serialize(source, sourceType);

            // Deseralize the JSON to an object.
            var obj = JsonSerializer.Deserialize(json, sourceType);

            // Return the results.
            return obj;
        }

        // *******************************************************************

        /// <summary>
        /// This method performs a quick clone of the specified object.
        /// </summary>
        /// <typeparam name="T">The type of object to be cloned.</typeparam>
        /// <param name="source">The object to be cloned.</param>
        /// <returns>The cloned object.</returns>
        public static T QuickClone<T>(
            this T source
            ) where T : class
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(source, nameof(source));

            // Serialize the object to JSON.
            var json = JsonSerializer.Serialize<T>(source);

            // Deseralize the JSON to an object.
            var obj = JsonSerializer.Deserialize<T>(json);

            // Return the results.
            return obj;
        }

        #endregion
    }
}
