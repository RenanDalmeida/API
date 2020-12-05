using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_PetShop.Domains;
using API_PetShop.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PetShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TipoDePetController : ControllerBase
    {
        TipoDePetRepository tipoDePetRepository = new TipoDePetRepository();

        // GET: <TipoDePetController>
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é exibido todos os tipos de pet cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista contendo todos os tipos de pet cadastrados no banco de dados.</returns>
        [HttpGet]
        public List<TipoDePet> Get()
        {
            return tipoDePetRepository.Ler();
        }

        // GET <TipoDePetController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é exibido o tipo de pet cadastrado no banco de dados que tenha o id especificado.
        /// </summary>
        /// <param name="id">Id do tipo de pet desejado.</param>
        /// <returns>Retorna o tipo de pet que tem o id especificado.</returns>
        [HttpGet("{id}")]
        public TipoDePet Get(int id)
        {
            return tipoDePetRepository.BuscarPorId(id);
        }

        // GET <TipoDePetController>/Filtrar/Cachorro
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é exibido o tipo de pet cadastrado no banco de dados que contém a descrição especificada.
        /// </summary>
        /// <param name="descricao">Descrição do tipo de pet desejado.</param>
        /// <returns>Retorna o tipo de pet que contém a descrição especificada.</returns>
        [HttpGet("Filtrar/{descricao}")]
        public TipoDePet Get(string descricao)
        {
            return tipoDePetRepository.BuscarPorDescricao(descricao);
        }

        // POST <TipoDePetController>
        /// <summary>
        ///     Quando acessada a rota acima com o método POST, é possível cadastrar um tipo de pet no banco de dados a partir de um objeto em JSON inserido no body da requisição.
        /// </summary>
        /// <param name="tipoDePet">Tipo de pet inserido no body da requisição que será cadastrado no banco de dados.</param>
        /// <returns>Retorna o tipo de pet que foi cadastrado.</returns>
        [HttpPost]
        public TipoDePet Post([FromBody] TipoDePet tipoDePet)
        {
            return tipoDePetRepository.Cadastrar(tipoDePet);
        }

        // PUT <TipoDePetController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método PUT, é possível alterar um tipo de pet no banco de dados a partir de um objeto em JSON inserido no body da requisição e o Id do tipo de pet cadastrado no banco de dados a ser alterado.
        /// </summary>
        /// <param name="id">Id do tipo de pet a ser alterado.</param>
        /// <param name="tipoDePet">Tipo de pet já alterado, que irá substituir o tipo de pet a ser alterado.</param>
        /// <returns>Retorna o tipo de pet que já foi alterado e já está cadastrado no banco de dados.</returns>
        [HttpPut("{id}")]
        public TipoDePet Put(int id, [FromBody] TipoDePet tipoDePetAlterado)
        {
            return tipoDePetRepository.Alterar(id, tipoDePetAlterado);
        }

        // DELETE <TipoDePetController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método DELETE, o tipo de pet que contém o id especificado será deletado do banco de dados.
        /// </summary>
        /// <param name="id">Id do tipo de pet a ser deletado.</param>
        /// <returns>Retorna o tipo de pet que foi deletado para uma melhor visualização e compreendimento do usuário.</returns>
        [HttpDelete("{id}")]
        public TipoDePet Delete(int id)
        {
            return tipoDePetRepository.Excluir(id);
        }
    }
}