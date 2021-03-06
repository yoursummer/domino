﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Complex.Domino.Lib {
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
    internal class SqlScripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SqlScripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Complex.Domino.Lib.SqlScripts", typeof(SqlScripts).Assembly);
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
        ///   Looks up a localized string similar to USE [Domino]
        ///
        ///GO
        ///
        ///
        ///CREATE TABLE [dbo].[UserRoleType]
        ///(
        ///	[ID] int NOT NULL,
        ///	[Name] nvarchar(50) NOT NULL,
        ///	
        ///	CONSTRAINT [PK_UserRoleType] PRIMARY KEY CLUSTERED 
        ///	(
        ///		ID ASC
        ///	)
        ///)
        ///
        ///GO
        ///
        ///
        ///CREATE TABLE [dbo].[GradeType]
        ///(
        ///	[ID] int NOT NULL,
        ///	[Name] nvarchar(50) NOT NULL,
        ///	
        ///	CONSTRAINT [PK_GradeType] PRIMARY KEY CLUSTERED 
        ///	(
        ///		ID ASC
        ///	)
        ///)
        ///
        ///GO
        ///
        ///
        ///CREATE TABLE [dbo].[Semester]
        ///(
        ///	[ID] int IDENTITY(1,1) NOT NULL,
        ///	[Name] nvarchar(50) NOT NULL,
        ///	[Description] nvarchar(250) NOT NU [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateSchema {
            get {
                return ResourceManager.GetString("CreateSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [Domino]
        ///
        ///GO
        ///
        ///IF OBJECT_ID (N&apos;UserRoleType&apos;, N&apos;U&apos;) IS NOT NULL
        ///DROP TABLE [dbo].[UserRoleType]
        ///
        ///GO
        ///
        ///IF OBJECT_ID (N&apos;GradeType&apos;, N&apos;U&apos;) IS NOT NULL
        ///DROP TABLE [dbo].[GradeType]
        ///
        ///GO
        ///
        ///IF OBJECT_ID (N&apos;Submission&apos;, N&apos;U&apos;) IS NOT NULL
        ///DROP TABLE [dbo].[Submission]
        ///
        ///GO
        ///
        ///IF OBJECT_ID (N&apos;AssignmentGrade&apos;, N&apos;U&apos;) IS NOT NULL
        ///DROP TABLE [dbo].[AssignmentGrade]
        ///
        ///GO
        ///
        ///IF OBJECT_ID (N&apos;Assignment&apos;, N&apos;U&apos;) IS NOT NULL
        ///DROP TABLE [dbo].[Assignment]
        ///
        ///GO
        ///
        ///IF OBJECT_ID (N&apos;UserRole&apos;, N&apos;U&apos;) IS NOT NUL [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DropSchema {
            get {
                return ResourceManager.GetString("DropSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [Domino]
        ///
        ///-- Create contants
        ///
        ///INSERT [dbo].[UserRoleType]
        ///VALUES
        ///	(1, &apos;admin&apos;),
        ///	(2, &apos;teacher&apos;),
        ///	(3, &apos;student&apos;)
        ///	
        ///INSERT [dbo].[GradeType]
        ///VALUES
        ///	(1, &apos;aláírás&apos;),
        ///	(2, &apos;osztályzat&apos;),
        ///	(3, &apos;pont&apos;)
        ///	
        ///
        ///-- Create dummy semester for admin
        ///
        ///SET IDENTITY_INSERT [dbo].[Semester] ON
        ///
        ///INSERT [dbo].[Semester]
        ///	(ID, Name, Description, Hidden, ReadOnly, CreatedDate, ModifiedDate, Comments, StartDate, EndDate)
        ///VALUES
        ///	(-1, &apos;admin&apos;, &apos;admin&apos;, 1, 1, GETDATE(), GETDATE(), &apos;&apos;, &apos;2015-01-01&apos;, &apos;2015- [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string InitData {
            get {
                return ResourceManager.GetString("InitData", resourceCulture);
            }
        }
    }
}
