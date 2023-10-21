using System;
using static rpruitt_final.Enums;

namespace rpruitt_final
{
    public class Order
    {
        public int orderId { get; set; }

        public OrderStatus orderStatusId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public Menu MenuId { get; set; }

        public int MenuItemId { get; set; }
    }
}