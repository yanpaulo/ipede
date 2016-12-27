using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class OrderStatusNames
    {
        public static readonly string
            Open = "Aberto",
            Placed = "Realizado",
            Confirmed = "Confirmado",
            Canceled = "Cancelado";
    }

    public class Order
    {

        public int Id { get; set; }

        public int TableId { get; set; }

        public string StatusName { get; set; }

        public virtual ObservableCollection<OrderItem> Items { get; set; } = new ObservableCollection<OrderItem>();

        public string DisplayName => $"Pedido {Id}";
    }
}
