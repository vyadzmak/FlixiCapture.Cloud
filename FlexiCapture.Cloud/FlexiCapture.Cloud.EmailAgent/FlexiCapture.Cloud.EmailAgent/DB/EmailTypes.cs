//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlexiCapture.Cloud.EmailAgent.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailTypes()
        {
            this.Emails = new HashSet<Emails>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public bool IsSend { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emails> Emails { get; set; }
    }
}
