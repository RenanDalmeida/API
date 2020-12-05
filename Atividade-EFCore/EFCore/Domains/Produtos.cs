using System;
using System.Collections.Generic;

namespace EF_Core_Database_First.Domains
{
    public partial class Produtos
    {
        public Produtos()
        {
            PedidosProdutos = new HashSet<PedidosProdutos>();
        }

        public string Nome { get; set; }
        public float Preco { get; set; }
        public Guid IdProduto { get; set; }

        public virtual ICollection<PedidosProdutos> PedidosProdutos { get; set; }
    }
}
