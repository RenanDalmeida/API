using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_Core_Code_First.Domains
{
    public class PedidoProduto
    {
        [Key]
        public Guid IdPedidoProduto { get; set; }

        public Guid IdPedido { get; set; } //Id do pedido, FK
        [ForeignKey("IdPedido")] //Indica que a foreign key do pedido está na propriedade IdPedido, indicando assim qual é esse pedido.
        public Pedido Pedido { get; set; } //Pedido

        public Guid IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        public Produto Produto { get; set; }

        [Required] //Not null
        public int Quantidade { get; set; }

        /// <summary>
        ///     Método construtor que garante que toda vez que um Produto for instanciado, ele receba um id único.
        /// </summary>
        public PedidoProduto()
        {
            IdPedidoProduto = Guid.NewGuid();
        }
    }
}