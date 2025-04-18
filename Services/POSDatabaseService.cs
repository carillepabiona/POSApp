using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Models;

namespace POSApp.Services
{
    public class POSDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public POSDatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "POSOrders.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<OrderItem>().Wait();
        }

        public Task<List<OrderItem>> GetOrdersAsync()
        {
            return _database.Table<OrderItem>().ToListAsync();
        }

        public Task<int> SaveOrderAsync(OrderItem item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> ClearOrdersAsync()
        {
            return _database.DeleteAllAsync<OrderItem>();
        }
        public async Task UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = await _database.Table<OrderItem>().Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                order.Status = newStatus;
                await _database.UpdateAsync(order);
            }
        }

        public Task<List<OrderItem>> GetPendingOrdersAsync()
        {
            return _database.Table<OrderItem>()
                .Where(o => o.Status == "pending")
                .ToListAsync();
        }
    }
}
