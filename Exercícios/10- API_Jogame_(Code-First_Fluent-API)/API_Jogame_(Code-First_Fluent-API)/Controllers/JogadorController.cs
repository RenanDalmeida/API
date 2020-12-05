using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogame__Code_First_Fluent_API_.Domains;
using API_Jogame__Code_First_Fluent_API_.Interfaces;
using API_Jogame__Code_First_Fluent_API_.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Jogame__Code_First_Fluent_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogadorController : ControllerBase
    {
        private readonly IJogadorRepository _jogadorRepository;

        /// <summary>
        ///     Construtor que garante que quando o controller for instanciado crie o repositório, utilizando injeção de dependência.
        /// </summary>
        public JogadorController()
        {
            _jogadorRepository = new JogadorRepository();
        }

        // GET: api/<JogadorController>
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é possível obter todos os objetos do tipo Jogador cadastrados no banco de dados.
        /// </summary>
        /// <returns>Todos objetos do tipo Jogador cadastrados no banco de dados (se não houver nenhum erro).</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var jogadores = _jogadorRepository.Ler();
                int qtdJogadores = jogadores.Count;

                if (qtdJogadores == 0)
                    return NoContent();

                return Ok(new { 
                    totalCount = qtdJogadores,
                    data = jogadores
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { 
                    statusCode = 400,
                    error = $"{ex.Message}. Para mais informações, envie um email para nossa equipe de suporte: suport@email.com"
                });;
            }
        }

        // GET api/<JogadorController>/buscar/id/5
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é possível obter o objeto do tipo Jogador que tenha o ID especificado.
        /// </summary>
        /// <param name="id">Id do objeto do tipo Jogador procurado.</param>
        /// <returns>Objeto do tipo Jogador (se não houver erros).</returns>
        [HttpGet("buscar/id/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var jogador = _jogadorRepository.Buscar(id);
                bool existe = jogador != null;

                if (!existe)
                    return NoContent();

                return Ok(jogador);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    error = $"{ex.Message}. Para mais informações, envie um email para nossa equipe de suporte: suport@email.com"
                }); ;
            }
        }

        // GET api/<JogadorController>/buscar/nome/Daniel
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é possível obter todos os objetos do tipo Jogador cadastrados no banco de dados que contenham o NOME especificado.
        /// </summary>
        /// <param name="nome">Nome do objeto procurado.</param>
        /// <returns>Objeto do tipo Jogador (se não houver erros).</returns>
        [HttpGet("buscar/nome/{nome}")]
        public IActionResult Get(string nome)
        {
            try
            {
                var jogadores = _jogadorRepository.Buscar(nome);
                int qtdJogadores = jogadores.Count;

                if (qtdJogadores == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdJogadores,
                    data = jogadores
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    error = $"{ex.Message}. Para mais informações, envie um email para nossa equipe de suporte: suport@email.com"
                }); ;
            }
        }

        // POST api/<JogadorController>
        /// <summary>
        ///     Quando acessada a rota acima com o método POST, é possível cadastrar no banco de dados um objeto do tipo Jogador passado via body.
        /// </summary>
        /// <param name="jogador">Objeto do tipo Jogador a ser cadastrado no banco de dados.</param>
        /// <returns>Objeto do tipo Jogador cadastrado.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Jogador jogador)
        {
            try
            {
                _jogadorRepository.Cadastrar(jogador);

                return Ok(jogador);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    error = $"{ex.Message}. Para mais informações, envie um email para nossa equipe de suporte: suport@email.com"
                });
            }
        }

        // PUT api/<JogadorController>
        /// <summary>
        ///     Quando acessada a rota acima com o método PUT, é possível alterar um jogador cadastrado no banco de dados.
        /// </summary>
        /// <param name="jogador">Objeto do tipo Jogador a ser alterado.</param>
        /// <returns>Objeto do tipo Jogador alterado.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] Jogador jogador)
        {
            try
            {
                _jogadorRepository.Alterar(jogador);

                return Ok(jogador);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    error = $"{ex.Message}. Para mais informações, envie um email para nossa equipe de suporte: suport@email.com"
                });
            }
        }

        // DELETE api/<JogadorController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método DELETE, é possível deletar um jogador cadastrado no banco de dados.
        /// </summary>
        /// <param name="id">Id do jogador a ser deletado.</param>
        /// <returns>Objeto do tipo Jogador deletado.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var jogador = _jogadorRepository.Buscar(id);

                if (jogador == null)
                    return NotFound();

                _jogadorRepository.Excluir(id);

                return Ok(jogador);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    error = $"{ex.Message}. Para mais informações, envie um email para nossa equipe de suporte: suport@email.com"
                });
            }
        }
    }
}
//TODO: ARRUMAR COMENTÁRIOS QUE EXPLICAM O INSERT AUTOMATICO DA TABELA AUXILIAR DE RELACIONAMENTO