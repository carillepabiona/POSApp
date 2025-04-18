using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using POSApp.Models;

namespace POSApp.Services
{
    public class OrderReceiverService
    {
        private TcpListener listener;
        private bool isListening;

        public event Action<string, List<OrderItem>>? OnOrdersReceived;

        public void StartListening()
        {
            listener = new TcpListener(IPAddress.Any, 9000);
            listener.Start();
            isListening = true;

            Task.Run(async () =>
            {
                while (isListening)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(async () => await HandleClient(client));
                }
            });
        }

        private async Task HandleClient(TcpClient client)
        {
            using (client)
            {
                var clientEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                string clientIP = clientEndPoint?.Address.ToString() ?? "Unknown";

                using var stream = client.GetStream();
                using var reader = new StreamReader(stream, Encoding.UTF8);

                string json = await reader.ReadToEndAsync();

                try
                {
                    var orders = JsonSerializer.Deserialize<List<OrderItem>>(json);

                    if (orders != null)
                    {
                        OnOrdersReceived?.Invoke(clientIP, orders);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling order: {ex.Message}");
                }
            }
        }

        public void StopListening()
        {
            isListening = false;
            listener?.Stop();
        }
    }
}
