//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class UserProfileServiceDefault
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int ServiceTypeId { get; set; }
    
        public virtual ServiceTypes ServiceTypes { get; set; }
        public virtual UserProfiles UserProfiles { get; set; }
    }
}
