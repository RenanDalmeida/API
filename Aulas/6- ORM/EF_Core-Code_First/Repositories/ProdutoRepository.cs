using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Contexts;
using EF_Core_Code_First.Domains;
using EF_Core_Code_First.Interfaces;

namespace EF_Core_Code_First.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PedidoContext _ctx;

        //PARA QUE NOSSA API SE COMPORTE COMO UMA RESTFUL API, PRECISAMOS TRATAR ERROS DE BANCO DE DADOS E CAPTURAR, ASSIM ELES FICARÃO NO LOG DO BANCO DE DADOS (QUANDO FOREM ERROS DE BANCO). A PARTIR DE AGORA, UTILIZAREMOS TRY CATCH. LEMBRE-SE QUE VOCÊ PODE USAR MAIS DE UM CATCH TAMBÉM PARA CAPTURAR DIFERENTES ERROS. VEJA A ULA DE TRY CATCH.

        /// <summary>
        ///     Construtor que certifica que toda vez que um ProdutoRepository for criado, o contexto seja instanciado.
        /// </summary>
        public ProdutoRepository()
        {
            _ctx = new PedidoContext();
        }

        //Region não é nada mais que uma indicação de que, por exemplo, essa parte do script contém métodos de leitura.
        #region Leitura 
        /// <summary>
        ///     Retorna todos os produtos cadastrados no banco de dados.
        /// </summary>
        /// <returns>Uma lista de produtos cadastrados no banco de dados.</returns>
        public List<Produto> Ler()
        {
            //Colocamos todos dentro de um try, pois ao dar erro, ele gravará a exception no log do banco de dados.
            try
            {
                //Pega todos os produtos do DbSet Produtos e joga em uma lista.
                return _ctx.Produtos.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///     Retorna o produto cadastrado no banco de dados que tenha o id especificado.
        /// </summary>
        /// <param name="id">Id do produto a ser retornado.</param>
        /// <returns>Retorna o produto que tenha o id especificado.</returns>
        public Produto BuscarPorId(Guid id)
        {
            try
            {
                //Busca o produto pelo id e retorna. É o mesmo que fazer isso: Produto produto = _ctx.Produtos.Find(id); return produto; Está refatorado.
                return _ctx.Produtos.Find(id);

                //Este é outro jeito de se fazer. Pega qual o primeiro produto com o id especificado de toda a tabela (top em sql), instancia um produto com os valores encontrados e retorna.
                //return _ctx.Produtos.FirstOrDefault(produto => produto.IdProduto == id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///     Filtra e retorna todos os produtos que tenha o nome especificado.
        /// </summary>
        /// <param name="nome">Nome a ser encontrado no produto a ser retornado.</param>
        /// <returns>Retorna o produto que contenha o nome especificado.</returns>
        public List<Produto> BuscarPorNome(string nome)
        {
            try
            {
                //Retorna uma lista de produtos que contenha o nome em seu nome (kk).
                return _ctx.Produtos.Where(produto => produto.Nome.Contains(nome)).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Gravação
        /// <summary>
        ///     Adiciona um produto ao banco de dados.
        /// </summary>
        /// <param name="produto">Produto a ser adicionado ao banco de dados.</param>
        public void Adicionar(Produto produto)
        {
            //Comece a utilizar mais tratamentos de erros. O usuário não merece fazer uma ação e ver o erro caso algo dê errado.
            try
            {
                //Adiciona o produto no DbSet (que é a tabela do bd). Se você quiser, pode adicionar uma lista de produtos passadas pelo usuário. Para isso, só desenvolver um for que tenha o comando abaixo dentro do for.
                _ctx.Produtos.Add(produto);
                /*  Os códigos abaixo também adicionam no bd.
                    _ctx.Set<Produto>().Add(produto);
                    _ctx.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                */

                //Salva as alterações que até então só foram feitas no contexto no banco de dados. Necessário em todas ações que não forem de consulta.
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///     Altera um produto no banco de dados.
        /// </summary>
        /// <param name="produto">Produto com alterações já feitas.</param>
        public void Alterar(Produto produto)
        {
            try
            {
                //Armazenamos o produto atual neste objeto. Porque não localizamos o objeto com o Find? Por que se mais tarde precisarmos alterar a forma como achamos produtos buscando por id, é só alterar o método BuscarPorId, assim só iremos precisar alterar em um lugar.
                Produto produtoTemporario = BuscarPorId(produto.IdProduto);

                //Se o produto não existir gera uma exceção personalizada.
                if (produtoTemporario == null)
                    throw new Exception("Produto não encontrado");

                //Caso exista insere os novos dados nas propriedades.
                produtoTemporario.Nome = produto.Nome;
                produtoTemporario.Preco = produto.Preco;

                //Coloca o novo produto no contexto.
                _ctx.Produtos.Update(produtoTemporario);
                //Salva.
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///     Exclui um produto do banco de dados.
        /// </summary>
        /// <param name="id">Id do produto a ser removido.</param>
        public void Excluir(Guid id)
        {
            try
            {
                //Armazenamos o produto atual neste objeto.
                Produto produtoTemporario = BuscarPorId(id);

                //Se o produto não existir gera uma exceção.
                if (produtoTemporario == null)
                    throw new Exception("Produto não encontrado");

                //Caso exista, remove.
                _ctx.Produtos.Remove(produtoTemporario);
                //Salva.
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO : Toda vez que você colocar um TODO, ele aparecerá no Task List. Vc pode usar como um lembrete como algo para fazer depois.
                //TODO: Cadastrar erros no log.
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}