using System;
using System.Collections.Generic;

namespace EF_Core_Database_First.Domains
{
    public partial class Pedidos
    {
        public Pedidos()
        {
            PedidosProdutos = new HashSet<PedidosProdutos>();
        }

        public string Status { get; set; }
        public DateTime DiaDaCompra { get; set; }
        public Guid IdPedido { get; set; }

        public virtual ICollection<PedidosProdutos> PedidosProdutos { get; set; }
    }
}
