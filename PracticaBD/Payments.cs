//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticaBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payments
    {
        public int payment_id { get; set; }
        public Nullable<int> work_order_id { get; set; }
        public decimal amount { get; set; }
        public System.DateTime payment_date { get; set; }
        public string payment_method { get; set; }
    
        public virtual WorkOrders WorkOrders { get; set; }
    }
}