using System;
using CG.Validations;
using System.Linq;
using System.Collections.Generic;
using CG.Collections.Generic;

namespace CG
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="Type"/>
    /// type.
    /// </summary>
    public static class TypeExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns a list of all the public, concrete types that 
        /// are derived from the specified type.
        /// </summary>
        /// <param name="type">The type to use for the operation.</param>
        /// <param name="assemblyWhiteList">An optional white list, for filtering
        /// the assemblies used in the operation.
        /// <param name="assemblyBlackList">An optional black list, for filtering
        /// the assemblies used in the operation.
        /// <returns>An array of matching types.</returns>
        public static Type[] DerivedTypes(
            this Type type,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft.*,System.*,netstandard"
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(type, nameof(type));

            // Get a list of all currently loaded, non-dynamic assemblies.
            var asmList = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic);

            // Should we apply a white list?
            if (!string.IsNullOrEmpty(assemblyWhiteList))
            {
                // Apply the white list.
                asmList = asmList.ApplyWhiteList(x => 
                    x.GetName().Name, assemblyWhiteList
                    );
            }

            // Should we apply a black list?
            if (!string.IsNullOrEmpty(assemblyBlackList))
            {
                // Apply the black list.
                asmList = asmList.ApplyBlackList(x => 
                    x.GetName().Name, assemblyBlackList
                    );
            }

            // Find any concrete types that are public and derive from 
            //   the specified type.
            var types = new List<Type>();
            foreach (var asm in asmList)
            {
                // Get the derived types from this assembly.
                var assemblyTypes = asm.GetTypes().Where(x =>
                    x.IsSubclassOf(type) &&
                    !x.IsAbstract &&
                    x.IsPublic
                    );

                // Add the derived types to the list.
                types.AddRange(assemblyTypes);
            }

            // Return the list.
            return types.ToArray();
        }

        #endregion
    }
}
