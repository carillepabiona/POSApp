using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using POSApp.Models;

namespace POSApp.Services
{
    public class OrderSyncService
    {
        private readonly int menuAppPort = 12345; // same port used in Menu App

        public async Task UpdateOrderStatusAsync(OrderItem order)
        {
            try
            {
                using TcpClient client = new TcpClient("192.168.1.5", menuAppPort); // Replace with actual Menu App IP
                using NetworkStream stream = client.GetStream();
                var json = JsonSerializer.Serialize(order);
                byte[] data = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to send order update: {ex.Message}");
            }
        }
    }
}
