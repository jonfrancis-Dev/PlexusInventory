//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlexusInventoryManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int productsID { get; set; }
        public string EPC { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Model { get; set; }
        public string Specs { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string UPC { get; set; }
        public Nullable<System.DateTime> Date_Received { get; set; }
        public string SerialNumber { get; set; }
        public string Grade { get; set; }
    }
}
