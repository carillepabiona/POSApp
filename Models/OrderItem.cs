using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace POSApp.Models
{
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = "pending..."; // default
        public DateTime OrderedAt { get; set; } = DateTime.Now;
        public string TableNumber { get; set; } // optional
        public string SourceIP { get; set; } // Optional: to track where the order came from
    }
}
