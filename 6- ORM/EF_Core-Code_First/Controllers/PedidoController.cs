using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Domains;
using EF_Core_Code_First.Interfaces;
using EF_Core_Code_First.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_Core_Code_First.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController()
        {
            _pedidoRepository = new PedidoRepository();
        }

        /// <summary>
        ///     Retorna todos os pedidos se não der erro.
        /// </summary>
        /// <returns>Retorna todos os pedidos se não der erro.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var pedidos = _pedidoRepository.Listar();

                if (pedidos.Count == 0)
                    return NoContent();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var pedido = _pedidoRepository.BuscarPorId(id);
                if (pedido == null)
                    return NoContent();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Cadastra um pedido e o PedidoProduto de uma vez.
        /// </summary>
        /// <param name="pedidosProdutos">Lista de pedidos produtos a serem cadastrados e já relacionados.</param>
        /// <returns>Retorna o Pedido cadastrado.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] List<PedidoProduto> pedidosProdutos)
        {
            try
            {
                Pedido pedido = _pedidoRepository.Adicionar(pedidosProdutos);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}