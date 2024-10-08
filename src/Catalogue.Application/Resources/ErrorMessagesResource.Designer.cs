﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Catalogue.Application.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessagesResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessagesResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Catalogue.Application.Resources.ErrorMessagesResource", typeof(ErrorMessagesResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred in the user information transmitted to the server.
        /// </summary>
        public static string AUTHENTICATION_ERROR {
            get {
                return ResourceManager.GetString("AUTHENTICATION_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The date of birth must be greater than {0} and less than {1}.
        /// </summary>
        public static string BIRTH_DATE_INVALID {
            get {
                return ResourceManager.GetString("BIRTH_DATE_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The key must be 32 bytes and the IV must be 16 bytes..
        /// </summary>
        public static string ENV_VARIABLE_INVALID {
            get {
                return ResourceManager.GetString("ENV_VARIABLE_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The encryption key and IV must be defined in the environment variables..
        /// </summary>
        public static string ENV_VARIABLES_NULL {
            get {
                return ResourceManager.GetString("ENV_VARIABLES_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name &apos;{0}&apos; already exists.
        /// </summary>
        public static string NAME_EXISTS_MESSAGE {
            get {
                return ResourceManager.GetString("NAME_EXISTS_MESSAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not found category name is &apos;{0}&apos;.
        /// </summary>
        public static string NOT_FOUND_CATEGORY_NAME {
            get {
                return ResourceManager.GetString("NOT_FOUND_CATEGORY_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not found {0} with id = &apos;{1}&apos;.
        /// </summary>
        public static string NOT_FOUND_ID_MESSAGE {
            get {
                return ResourceManager.GetString("NOT_FOUND_ID_MESSAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not found role = {0}.
        /// </summary>
        public static string NOT_FOUND_ROLE {
            get {
                return ResourceManager.GetString("NOT_FOUND_ROLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected server error occurred.
        /// </summary>
        public static string SERVER_ERROR_MESSAGE {
            get {
                return ResourceManager.GetString("SERVER_ERROR_MESSAGE", resourceCulture);
            }
        }
    }
}
