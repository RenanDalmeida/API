using System;
using System.Collections.Generic;

namespace EF_Core_Database_First.Domains
{
    public partial class PedidosProdutos
    {
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
        public Guid IdPedidoProduto { get; set; }

        public virtual Pedidos IdPedidoNavigation { get; set; }
        public virtual Produtos IdProdutoNavigation { get; set; }
    }
}
