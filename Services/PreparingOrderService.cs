using POSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Models;

namespace POSApp.Services
{
    public class PreparingOrderService
    {
        public List<OrderItem> PreparingOrders { get; set; } = new();

        public void AddPreparingOrders(List<OrderItem> orders)
        {
            PreparingOrders.AddRange(orders);
        }

        public List<OrderItem> GetPreparingOrders()
        {
            return PreparingOrders;
        }
    }
}
