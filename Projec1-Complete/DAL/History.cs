//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projec1_Complete.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class History
    {
        public int HistoryID { get; set; }
        public string Action { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string ProductID { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
