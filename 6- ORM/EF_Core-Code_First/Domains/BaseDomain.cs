using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_Core_Code_First.Domains
{
    //Todos domains vão ter essa propriedade e esse método. Entao essa é uma forma de refatorar nosso código. Não apliquei pra não ficar confuso quando eu for estudar esse código.
    public abstract class BaseDomain
    {
        [Key] //DataAnnotation, indicando que o campo abaixo é uma PK.
        public Guid Id { get; set; } //A partir de agora iremos utilizar o tipo Guid para os IDs, tornando as informações cadastradas em nosso banco mais seguras. Isso por que o Guid garante que nosso id vire um tipo de chave, assim tornando difícil alguém mal intencionado acertar um id de um cadastro específico para hackear ou obter informações que ela normalmente não teria acesso. Por também ser uma chave bem grande, há muitas combinações diferentes, resultando em IDs diferentes para cada cadastro, sendo semelhante ao identity do SQL Server. Mas não apenas um id, onde por exemplo, um hacker quer descobrir o id do Daniel. Se ele sabe que o id do David é 12, o do Daniel está pra baixo do 12. Com o Guid não é assim.. ele teria que acertar a chave.

        /// <summary>
        ///     Método construtor que garante que toda vez que um Produto for instanciado, ele receba um id único.
        /// </summary>
        public BaseDomain()
        {
            this.Id = Guid.NewGuid();
        }
    }
}