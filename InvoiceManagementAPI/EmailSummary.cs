//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceManagementAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailSummary
    {
        public int Id { get; set; }
        public string EmailType { get; set; }
        public string EmailTemplate { get; set; }
        public string EmailSubject { get; set; }
        public string EmailReceiver { get; set; }
        public string EmailSender { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
