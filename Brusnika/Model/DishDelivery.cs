//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Brusnika.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DishDelivery
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int DeliveryId { get; set; }
    
        public virtual Delivery Delivery { get; set; }
        public virtual DIsh DIsh { get; set; }
    }
}
