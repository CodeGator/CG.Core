using CG.Collections.Generic;
using CG.Validations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CG.Reflection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="AppDomain"/>
    /// type.
    /// </summary>
    public static partial class AppDomainExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method searches among the assemblies loaded into the current 
        /// app-domain for any public extension methods associated with the 
        /// specified type and signature.
        /// </summary>
        /// <param name="appDomain">The application domain to use for the operation.</param>
        /// <param name="extensionType">The type to match against.</param>
        /// <param name="methodName">The method name to match against.</param>
        /// <param name="parameterTypes">An optional list of parameter type(s) 
        /// to match against.</param>
        /// <param name="assemblyWhiteList">An optional white list of assembly
        /// names - for narrowing the range of assemblies searched.</param>
        /// <param name="assemblyBlackList">An optional white list of assembly
        /// names - for narrowing the range of assemblies searched.</param>
        /// <returns>A sequence of <see cref="MethodInfo"/> objects representing
        /// zero or more matching extension methods.</returns>
        public static IEnumerable<MethodInfo> ExtensionMethods(
            this AppDomain appDomain,
            Type extensionType,
            string methodName,
            Type[] parameterTypes = null,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(appDomain, nameof(appDomain))
                .ThrowIfNull(extensionType, nameof(extensionType))
                .ThrowIfNullOrEmpty(methodName, nameof(methodName));

            // Should we supply a valid value?
            if (parameterTypes == null)
            {
                parameterTypes = new Type[0];
            }

            // Get the list of currently loaded assemblies.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            // Was a white list specified?
            if (!string.IsNullOrEmpty(assemblyWhiteList))
            {
                // Look for assemblies in the white list we might need to load.
                var toLoad = assemblyWhiteList.Split(',').Where(
                    x => assemblies.Any(y => !y.GetName().Name.IsMatch(x))
                    );

                // Did we find any?
                if (toLoad.Any())
                {
                    // Loop and load any missing whitelisted assemblies.
                    toLoad.ForEach(x =>
                    {
                        // Look for matching files.
                        var files = Directory.GetFiles(
                            AppDomain.CurrentDomain.BaseDirectory,
                            x.EndsWith(".dll") ? x : $"{x}.dll"
                            );
                        foreach (var file in files)
                        {
                            try
                            {
                                Assembly.LoadFile(file);
                            }
                            catch (Exception)
                            {
                                // Don't care, just won't search this file.
                            }
                        }
                    });

                    // Get the list of currently loaded assemblies.
                    assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

                    // Filter out anything that doesn't belong.
                    assemblies = assemblies.ApplyWhiteList(
                        x => x.GetName().Name, assemblyWhiteList
                        ).ToList();
                }

                // At this point the assembly list should only contain assemblies 
                //   that are already loaded in the app-domain, and have their 
                //   name(s) on the white list.
            }

            // Was a black list specified?
            if (!string.IsNullOrEmpty(assemblyBlackList))
            {
                // Split the black list into parts.
                var blackParts = assemblyBlackList.Split(',');

                // Filter out anything that doesn't belong.
                assemblies = assemblies.ApplyBlackList(
                    x => x.GetName().Name, assemblyBlackList
                    ).ToList();

                // At this point the assembly list should only contains those
                //   assemblies that are already loaded in the app-domain and/or
                //   white listed, and are not on the black list.
            }

            // Now we have a list of assemblies that contains juuuuuust the right items
            //   and we can use that list to perform our next search.

            var methods = new List<MethodInfo>();

            // Create options for the parallel operation.
            var options = new ParallelOptions()
            {
#if DEBUG
                MaxDegreeOfParallelism = 1 // <-- to make debugging easier.
#else
                MaxDegreeOfParallelism = Environment.ProcessorCount
#endif
            };

            // We should be able to conduct the search in parallel.
            Parallel.ForEach(assemblies, options, (assembly) =>
            {
                // Look for types that are public, sealed, non-nested, non-generic classes.
                var types = assembly.GetTypes()
                        .Where(x => x.IsClass && x.IsPublic && x.IsSealed && !x.IsNested && !x.IsGenericType);

                // Loop through each matching type.
                foreach (var type in types)
                {
                    // Look for an extension method with a matching name.
                    var method = type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                            .FirstOrDefault(x => x.Name == methodName && x.IsDefined(typeof(ExtensionAttribute), false));

                    // Did we find one?
                    if (method != null)
                    {
                        // Get the parameter info.
                        var pi = method.GetParameters();

                        // Get the LHS of the comparison.
                        var lhs = pi.Select(x => x.ParameterType).ToArray();

                        // Get the RHS of the comparison.
                        var rhs = new Type[] { extensionType }.Concat(parameterTypes).ToArray();

                        // Don't add methods with mismatched sigs.
                        if (lhs.Count() != rhs.Count())
                        {
                            // Skip it.
                            continue;
                        }

                        // Try to match assignable types.
                        for (int z = 1; z < lhs.Length; z++)
                        {
                            // Are the types not assignable?
                            if (!lhs[z].IsAssignableFrom(rhs[z]))
                            {
                                // Skip it.
                                continue;
                            }
                        }

                        // Add the method to the collection.
                        methods.Add(method);
                    }
                }
            });

            // Return the results.
            return methods;
        }

        #endregion
    }
}
