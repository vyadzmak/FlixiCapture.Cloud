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
    
    public partial class Tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tasks()
        {
            this.Documents = new HashSet<Documents>();
            this.TaskStatistics = new HashSet<TaskStatistics>();
            this.ZipTasks = new HashSet<ZipTasks>();
        }
    
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int TaskStateId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string ProfileContent { get; set; }
        public string ResponseContent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documents> Documents { get; set; }
        public virtual ServiceTypes ServiceTypes { get; set; }
        public virtual TaskStates TaskStates { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskStatistics> TaskStatistics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZipTasks> ZipTasks { get; set; }
    }
}
