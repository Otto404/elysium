﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Elysium.Platform.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class GadgetErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GadgetErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Elysium.Platform.Properties.GadgetErrors", typeof(GadgetErrors).Assembly);
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
        ///   Looks up a localized string similar to ColumnSpan value must be greater than zero..
        /// </summary>
        internal static string ColumnSpanValueMustBeGreaterThanZero {
            get {
                return ResourceManager.GetString("ColumnSpanValueMustBeGreaterThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ColumnSpan value must be less than or equal two..
        /// </summary>
        internal static string ColumnSpanValueMustBeLessThanOrEqualToTwo {
            get {
                return ResourceManager.GetString("ColumnSpanValueMustBeLessThanOrEqualToTwo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Column value must be greater than or equal to zero..
        /// </summary>
        internal static string ColumnValueMustBeGreaterThanOrEqualToZero {
            get {
                return ResourceManager.GetString("ColumnValueMustBeGreaterThanOrEqualToZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot host gadgets..
        /// </summary>
        internal static string HostingFailed {
            get {
                return ResourceManager.GetString("HostingFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gadget is not registered..
        /// </summary>
        internal static string IsNotRegistered {
            get {
                return ResourceManager.GetString("IsNotRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Row value must be greater than or equal to zero..
        /// </summary>
        internal static string RowValueMustBeGreaterThanOrEqualToZero {
            get {
                return ResourceManager.GetString("RowValueMustBeGreaterThanOrEqualToZero", resourceCulture);
            }
        }
    }
}