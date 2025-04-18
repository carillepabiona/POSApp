using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace POSApp.Services
{
  public class OrderStatusUpdate
    {
        public async Task SendOrderStatusUpdateAsync(int orderId, string status)
        {
            using var client = new TcpClient("menu-app-ip", 6001); // Replace with actual IP or "localhost"
            var data = new
            {
                OrderId = orderId,
                NewStatus = status
            };

            string json = JsonSerializer.Serialize(data);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await client.GetStream().WriteAsync(bytes, 0, bytes.Length);
        }
    }

}
