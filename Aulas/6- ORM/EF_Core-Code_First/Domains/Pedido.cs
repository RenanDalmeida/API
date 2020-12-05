using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_Core_Code_First.Domains
{
    public class Pedido
    {
        [Key]
        public Guid IdPedido { get; set; }
        public string Status { get; set; }
        public DateTime DiaDaCompra { get; set; }

        //Relacionamento com a tabela PedidoProduto, que é 1,n
        public List<PedidoProduto> PedidosProdutos { get; set; }

        /// <summary>
        ///     Método construtor que garante que toda vez que um Pedido for instanciado, ele receba um id único.
        /// </summary>
        public Pedido()
        {
            IdPedido = Guid.NewGuid();
            PedidosProdutos = new List<PedidoProduto>();
        }
    }
}