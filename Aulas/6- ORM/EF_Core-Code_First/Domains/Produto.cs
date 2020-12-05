using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EF_Core_Code_First.Domains
{
    public class Produto
    {
        [Key] //DataAnnotation, indicando que o campo abaixo é uma PK.
        public Guid IdProduto { get; set; } //A partir de agora iremos utilizar o tipo Guid para os IDs, tornando as informações cadastradas em nosso banco mais seguras. Isso por que o Guid garante que nosso id vire um tipo de chave, assim tornando difícil alguém mal intencionado acertar um id de um cadastro específico para hackear ou obter informações que ela normalmente não teria acesso. Por também ser uma chave bem grande, há muitas combinações diferentes, resultando em IDs diferentes para cada cadastro, sendo semelhante ao identity do SQL Server. Mas não apenas um id, onde por exemplo, um hacker quer descobrir o id do Daniel. Se ele sabe que o id do David é 12, o do Daniel está pra baixo do 12. Com o Guid não é assim.. ele teria que acertar a chave.
        public string Nome { get; set; }
        public float Preco { get; set; }

        //Propriedade adicionada depois. Para guardar um arquivo, seja ele uma imagem, vídeo, áudio etc, você usará esse IFormFile. Você também pode armazenar um array de bytes (nem um pouco recomendável). Mas a melhor maneira é armazenar o CAMINHO do arquivo.
        [NotMapped] //NotMapped serve para não criar a tabela no banco de dados.. é uma prop auxiliar, não um atributo.
        [JsonIgnore] //Não vai mostrar alguns dados da imagem na api pois seria desnecessário e aumentaria o tráfego de dados desnecessariamente.
        public IFormFile Imagem { get; set; }
        public string UrlImagem { get; set; }

        //Relacionamento com a tabela PedidoProduto, que é 1,n
        public List<PedidoProduto> PedidosProdutos { get; set; }

        /// <summary>
        ///     Método construtor que garante que toda vez que um Produto for instanciado, ele receba um id único.
        /// </summary>
        public Produto()
        {
            this.IdProduto = Guid.NewGuid();
        }
    }
}