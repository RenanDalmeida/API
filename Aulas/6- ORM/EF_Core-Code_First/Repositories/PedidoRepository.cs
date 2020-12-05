using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Contexts;
using EF_Core_Code_First.Domains;
using EF_Core_Code_First.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_Code_First.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidoContext _ctx;

        /// <summary>
        ///     Construtor para instanciar nosso contexto, o banco de dados que salvará essas informações.
        /// </summary>
        public PedidoRepository()
        {
            _ctx = new PedidoContext();
        }

        /// <summary>
        ///     Adiciona um pedido no banco de dados.
        /// </summary>
        /// <param name="pedidosProdutos">Lista de pedidos produtos para quando adicionarmos na tabela Pedido, adicionar automaticamente na tabela PedidoProduto.</param>
        /// <returns>Retorna o pedido que foi adicionado para melhor visualização.</returns>
        public Pedido Adicionar(List<PedidoProduto> pedidosProdutos)
        {
            try
            {
                //Criamos um novo pedido, já atribuindo seu status e pegando a data atual para definir o dia da compra.
                Pedido pedido = new Pedido
                {
                    Status = "Pedido efetuado",
                    DiaDaCompra = DateTime.Now
                };

                //Varremos a lista de pedidosProdutos passada no body para dizer quais produtos esse pedido teve.
                foreach (PedidoProduto item in pedidosProdutos)
                {
                    //Adicionamos cada PedidoProduto na nossa lista de PedidosProdutos de Pedido.
                    pedido.PedidosProdutos.Add(new PedidoProduto
                    {
                        IdPedido = pedido.IdPedido,
                        IdProduto = item.IdProduto,
                        Quantidade = item.Quantidade
                    });
                }

                //Salvamos o pedido no contexto
                _ctx.Pedidos.Add(pedido);
                //Salvamos o contexto no banco.
                _ctx.SaveChanges();

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Pedido> Listar()
        {
            try
            {
                return _ctx.Pedidos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pedido BuscarPorId(Guid id)
        {
            try
            {
                //Vai retornar o pedido e os produtos associados a esse pedido, além dos dados desses produtos. É como se fosse um InnerJoin. Só tem um problema: Quanto mais rápida for a API melhor. O que vai definir se sua API é rápida ou não é a quantidade de dados que ela está levando. Se você utilizar dados redundantes, ela não vai funcionar bem. Isso pode acontecer aqui, pois pode retornar produtos nulos. Claro, essa API é pequena, mas imagina uma API grande retornando dados redundantes, seria bem lerda, o que faria o usuário desistir de acessar seu site, ou seu app, ou seu IOT, ou o que quer que seja. Portanto, vamos para a Startup.
                return _ctx.Pedidos
                    .Include(pedido => pedido.PedidosProdutos)
                    .ThenInclude(pedido => pedido.Produto)
                    .FirstOrDefault(pedido => pedido.IdPedido == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}