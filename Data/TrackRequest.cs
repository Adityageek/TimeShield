//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeShield.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrackRequest
    {
        public int TrackingId { get; set; }
        public string RequestGUID { get; set; }
        public System.DateTime ApproveTime { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
    }
}
