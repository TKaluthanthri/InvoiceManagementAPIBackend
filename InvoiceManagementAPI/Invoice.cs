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
    
    public partial class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public System.DateTime invoiceDate { get; set; }
        public decimal Amount { get; set; }
        public int CustomerRef { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsMailSent { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}