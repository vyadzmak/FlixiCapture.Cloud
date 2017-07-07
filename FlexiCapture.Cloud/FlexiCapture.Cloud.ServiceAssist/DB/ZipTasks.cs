//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexiCapture.Cloud.ServiceAssist.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZipTasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZipTasks()
        {
            this.ZipDocuments = new HashSet<ZipDocuments>();
        }
    
        public int Id { get; set; }
        public int OuterTaskId { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int TaskStateId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string ProfileContent { get; set; }
        public string ResponseContent { get; set; }
    
        public virtual ServiceTypes ServiceTypes { get; set; }
        public virtual Tasks Tasks { get; set; }
        public virtual TaskStates TaskStates { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZipDocuments> ZipDocuments { get; set; }
    }
}
