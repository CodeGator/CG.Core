﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CG.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CG.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument must contain at least one element!\r\n[called from {0} in {1}, line {2}].
        /// </summary>
        internal static string Guard_ArgIsEmpty {
            get {
                return ResourceManager.GetString("Guard_ArgIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument is not a readable stream!\r\n[called from {0} in {1}, line {2}].
        /// </summary>
        internal static string Guard_ArgNotReadable {
            get {
                return ResourceManager.GetString("Guard_ArgNotReadable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument is not a writable stream!\r\n[called from {0} in {1}, line {2}].
        /// </summary>
        internal static string Guard_ArgNotWritable {
            get {
                return ResourceManager.GetString("Guard_ArgNotWritable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument contains an invalid file extension!\r\n[called from {0} in {1}, line {2}].
        /// </summary>
        internal static string Guard_InvalidExtension {
            get {
                return ResourceManager.GetString("Guard_InvalidExtension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument contains an invalid file path!\r\n[called from {0} in {1}, line {2}].
        /// </summary>
        internal static string Guard_InvalidFilePath {
            get {
                return ResourceManager.GetString("Guard_InvalidFilePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument contains an invalid folder path!\r\n[called from {0} in {1}, line {2}].
        /// </summary>
        internal static string Guard_InvalidFolderPath {
            get {
                return ResourceManager.GetString("Guard_InvalidFolderPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error while iterating over an enumerable sequence! See any inner exception(s) for more details..
        /// </summary>
        internal static string IEnumerableExtensions_ForEach {
            get {
                return ResourceManager.GetString("IEnumerableExtensions_ForEach", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to create type: {0} as a singleton.
        /// </summary>
        internal static string SingletonBaseEx_Instance {
            get {
                return ResourceManager.GetString("SingletonBaseEx_Instance", resourceCulture);
            }
        }
    }
}
