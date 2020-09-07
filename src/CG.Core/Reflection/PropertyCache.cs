using CG.Core.Properties;
using CG.Validations;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace CG.Reflection
{
    /// <summary>
    /// This class represents cached property reflection information.
    /// </summary>
    /// <remarks>
    /// <para>The idea, with this class, is to make it easier to reach out and 
    /// quickly grab property information for a type. Also, to have that same 
    /// information cached locally to avoid unnecessary reflection. </para>
    /// </remarks>
    public class PropertyCache
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains a property type cache.
        /// </summary>
        internal readonly ConcurrentDictionary<string, PropertyInfo> _cache
            = new ConcurrentDictionary<string, PropertyInfo>();

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This indexer gets/sets cached property information.
        /// </summary>
        /// <param name="type">The type to use for the operation.</param>
        /// <param name="property">The property to use for the operation.</param>
        /// <returns>Cached property information.</returns>
        public PropertyInfo this[Type type, string property]
        {
            get
            {
                // Validate the parameter before attempting to use it.
                Guard.Instance().ThrowIfNull(type, nameof(type))
                    .ThrowIfNullOrEmpty(property, nameof(property));

                // Make a (hopefully) unique key.
                var key = $"{type.FullName}:{property}";

                // Try to find any previously cached info.
                if (!_cache.TryGetValue(key, out PropertyInfo info))
                {
                    // Try to find info for the property.
                    info = type.GetTypeInfo().GetProperty(property);

                    // Did we fail?
                    if (info == null)
                    {
                        // Panic!!
                        throw new ArgumentException(
                            message: string.Format(
                                Resources.PropertyCache_NotFound,
                                type,
                                property
                                )
                            );
                    }

                    // Try to cache the info.
                    _cache.TryAdd(key, info);
                }

                // Return the results.
                return info;
            }
        }

        #endregion
    }
}
