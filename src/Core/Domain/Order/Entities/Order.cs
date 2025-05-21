using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Order.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public double Total { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtUpdated { get; set; }
        
    }
}