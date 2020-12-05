using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Domains;
using EF_Core_Code_First.Interfaces;
using EF_Core_Code_First.Repositories;
using EF_Core_Code_First.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Core_Code_First.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        //Por que instanciamos como a interface e não como o repositório? Tem a ver com Injeção de Dependência. Uma dependência é simplesmente um objeto que a sua classe precisa para funcionar. Ao fazer a injeção de dependencia você coloca a responsabilidade das classes externas na classe que está chamando e não na classe chamada. Injeção de dependência é um Design Pattern que prega um tipo de controle externo, um container, uma classe, configurações via arquivo, etc., inserir uma dependência em uma outra classe. Tentando melhorar: "O padrão de injeção de dependências visa remover dependências desnecessárias entre as classes". Para entender o conceito é também necessário aprofundar o conhecimento em Inversão de Controle e um pouco do principio SOLID, afinal ele é a Letra D (Dependa de uma abstração e não de uma implementação).
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController()
        {
            _produtoRepository = new ProdutoRepository();
        }

        //PARA QUE NOSSA API SE COMPORTE COMO UMA RESTFUL API, PRECISAMOS TRATAR ERROS DE REQUISIÇÃO/RESPONSE E RETORNAR STATUS CODE. A PARTIR DE AGORA, UTILIZAREMOS TRY CATCH E RETORNAREMOS OS STATUS CODE. LEMBRE-SE QUE VOCÊ PODE USAR MAIS DE UM CATCH TAMBÉM PARA CAPTURAR DIFERENTES ERROS. VEJA A ULA DE TRY CATCH.


        //IActionResult: Resultado de uma ação, GET, POST, etc.

        /// <summary>
        ///     Quando acessada a rota Produto com o método GET, retorna todos os produtos do banco de dados.
        /// </summary>
        /// <returns>Todos os produtos do banco de dados ou erros.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var produtos = _produtoRepository.Ler();

                if (produtos.Count == 0)
                    return NoContent(); //204

                //Note que aqui passamos mais informações no que nos outros métodos. Isso é um exemplo. É inteerssante retornar algumas informações a mais, mas claro, depende do projeto.
                return Ok(new
                { // ok = 200
                    totalCount = produtos.Count, //Mostra a qtd de objetos
                    data = produtos //Mostra os objetos
                }); 
            }
            //Talvez dê pra incrementar mais esse catch...
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //400
            }
        }

        /// <summary>
        ///     Quando acessada a rota Produto/{id} com o método GET, retorna o produto do banco de dados que tenha o id especificado.
        /// </summary>
        /// <param name="id">Id do produto a ser retornado.</param>
        /// <returns>Produto com o id especificado ou erros.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Produto produto = _produtoRepository.BuscarPorId(id);

                if (produto == null)
                    return NotFound(); //404

                ConversorMonetario dolar = new ConversorMonetario();

                return Ok(new { 
                    produto,
                    valorDolar = produto.Preco / dolar.GetDolarValue()
                });
            }
            catch (Exception ex)
            {
                //O BadRequest aceita qualquer objeto como parâmetro.
                return BadRequest(new
                {
                    statusCode = 400,
                    error = ex.Message //Aqui você também pode colocar uma mensagem de erro personalizada.
                });
            }
        }

        /// <summary>
        ///     Quando acessada a rota Produto/FiltrarPorNome/{nome} com o método GET, retorna o produto do banco de dados que contenha o nome especificado.
        /// </summary>
        /// <param name="nome">Nome do produto a ser retornado.</param>
        /// <returns>Produto que contém o nome especificado ou erros.</returns>
        [HttpGet("FiltrarPorNome/{nome}")]
        public IActionResult Get(string nome)
        {
            try
            {
                var produtos = _produtoRepository.BuscarPorNome(nome);

                if (produtos.Count == 0)
                    return NoContent();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Quando acessada a rota Produto com o método POST, você pode cadastrar um produto enviado via Body.
        /// </summary>
        /// <param name="produto">Produto a ser cadastrado.</param>
        /// <returns>Retorna o produto que foi cadastrado.</returns>
        [HttpPost]
        public IActionResult Post([FromForm] Produto produto) //Aqui vamos fazer algumas alterações para o cadastro de arquivos, que em nosso caso só será imagens. A partir de agora, o usuário não vai mais colocar o produto via body, mas via formulário. Então colocamos FromForm. OBS: O usuário vai mandar a imagem, mas nós nãov amos salvar a imagem, mas sim o caminho dela. Ao acessar produto.Imagem. vai aparecer algumas propriedades.. por exemplo, vc pode escolher receber apenas imagens .png .
        {
            try
            {
                if(produto.Imagem != null)
                {
                    var urlImagem = Upload.Local(produto.Imagem);
                    produto.UrlImagem = urlImagem;
                }

                _produtoRepository.Adicionar(produto);

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Quando acessada a rota Produto com o método PUT, você pode alterar um produto enviado via Body. O método vai pegar o id do objeto enviado via Body e já vai saber quem tem que alterar.
        /// </summary>
        /// <param name="produtoAlterado">Produto já alterado.</param>
        /// <returns>Retorna o produto que foi alterado.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] Produto produtoAlterado)
        {
            try
            {
                _produtoRepository.Alterar(produtoAlterado);

                return Ok(produtoAlterado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Quando acessada a rota Produto/{id} com o método DELETE, você pode deletar um produto.
        /// </summary>
        /// <param name="id">Id do produto a ser deletado.</param>
        /// <returns>Retorna o produto que foi deletado ou erros.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var produto = _produtoRepository.BuscarPorId(id);

                if (produto == null)
                    return NotFound();

                _produtoRepository.Excluir(id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}