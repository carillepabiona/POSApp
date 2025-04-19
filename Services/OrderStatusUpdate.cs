using POSApp.Models;
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
        public static async Task SendStatusUpdateAsync(string newStatus, List<OrderItem> orders)
        {
            using var client = new TcpClient();
            await client.ConnectAsync("192.168.254.105", 5000); // IP of Menu App & port

            using var stream = client.GetStream();
            using var writer = new StreamWriter(stream) { AutoFlush = true };

            foreach (var order in orders)
            {
                string message = $"{order.Id}|{newStatus}";
                await writer.WriteLineAsync(message);
            }
        }
    }

}
