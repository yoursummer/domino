//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "12.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class EmailTemplates {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EmailTemplates() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.EmailTemplates", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        ///   Looks up a localized string similar to Hello [$Name],
        ///
        ///Your Domino password was changed at [$DateTime]. If you didn&apos;t
        ///change the password, please notify the administrator.
        ///
        ///===============================================================
        ///Domino.
        /// </summary>
        internal static string ChangePassword {
            get {
                return ResourceManager.GetString("ChangePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Domino password changed.
        /// </summary>
        internal static string ChangePasswordSubject {
            get {
                return ResourceManager.GetString("ChangePasswordSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello [$Name],
        ///
        ///A new reply to your submission at [$DateTime] was sent.
        ///
        ///To view the reply, follow the link below.
        ///
        ///[$BaseUrl][$Url]
        ///
        ///===============================================================
        ///Domino.
        /// </summary>
        internal static string Reply {
            get {
                return ResourceManager.GetString("Reply", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reply to a submission received.
        /// </summary>
        internal static string ReplySubject {
            get {
                return ResourceManager.GetString("ReplySubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello [$Name],
        ///
        ///You have requested a password reset for your Domino account.
        ///
        ///To reset your password, please click on the link below.
        ///
        ///[$BaseUrl][$Url]
        ///
        ///===============================================================
        ///Domino.
        /// </summary>
        internal static string ResetPassword {
            get {
                return ResourceManager.GetString("ResetPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Domino password reset.
        /// </summary>
        internal static string ResetPasswordSubject {
            get {
                return ResourceManager.GetString("ResetPasswordSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello [$Name],
        ///
        ///Your submission to [$Assignment] was received at [$DateTime].
        ///It contains the following files.
        ///
        ///[$Files]
        ///
        ///===============================================================
        ///Domino.
        /// </summary>
        internal static string Submission {
            get {
                return ResourceManager.GetString("Submission", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New submission received.
        /// </summary>
        internal static string SubmissionSubject {
            get {
                return ResourceManager.GetString("SubmissionSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello [$Name],
        ///
        ///Your Domino user account has been successfully updated.
        ///
        ///===============================================================
        ///Domino.
        /// </summary>
        internal static string UpdateAccount {
            get {
                return ResourceManager.GetString("UpdateAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Domino account updated.
        /// </summary>
        internal static string UpdateAccountSubject {
            get {
                return ResourceManager.GetString("UpdateAccountSubject", resourceCulture);
            }
        }
    }
}
