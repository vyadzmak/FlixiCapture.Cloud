﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexiCapture.Cloud.Portal.Api.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FCCPortalEntities : DbContext
    {
        public FCCPortalEntities()
            : base("name=FCCPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AnalysisModeCatalog> AnalysisModeCatalog { get; set; }
        public virtual DbSet<DocumentCategories> DocumentCategories { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<DocumentStates> DocumentStates { get; set; }
        public virtual DbSet<DocumentTypes> DocumentTypes { get; set; }
        public virtual DbSet<EmailResponseSettings> EmailResponseSettings { get; set; }
        public virtual DbSet<EmailSettings> EmailSettings { get; set; }
        public virtual DbSet<ExportFormatsCatalog> ExportFormatsCatalog { get; set; }
        public virtual DbSet<FTPSettings> FTPSettings { get; set; }
        public virtual DbSet<LanguagesCatalog> LanguagesCatalog { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<PrintTypeCatalog> PrintTypeCatalog { get; set; }
        public virtual DbSet<RotationTypeCatalog> RotationTypeCatalog { get; set; }
        public virtual DbSet<ServiceTypes> ServiceTypes { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TaskStates> TaskStates { get; set; }
        public virtual DbSet<UserConfirmationEmails> UserConfirmationEmails { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserLoginStates> UserLoginStates { get; set; }
        public virtual DbSet<UserProfileExportFormats> UserProfileExportFormats { get; set; }
        public virtual DbSet<UserProfileLanguages> UserProfileLanguages { get; set; }
        public virtual DbSet<UserProfilePrintTypes> UserProfilePrintTypes { get; set; }
        public virtual DbSet<UserProfiles> UserProfiles { get; set; }
        public virtual DbSet<UserProfileServiceDefault> UserProfileServiceDefault { get; set; }
        public virtual DbSet<UserRoleTypes> UserRoleTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserServiceSubscribes> UserServiceSubscribes { get; set; }
        public virtual DbSet<UserServiceSubscribeStates> UserServiceSubscribeStates { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }
        public virtual DbSet<ZipDocuments> ZipDocuments { get; set; }
        public virtual DbSet<ZipTasks> ZipTasks { get; set; }
    }
}
