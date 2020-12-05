using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogame__Code_First_Fluent_API_.Domains
{
    public abstract class Base
    {
        //PK
        public Guid Id { get; private set; }

        /// <summary>
        ///     Construtor que atribui um id único com uma hash complexa para maior segurança assim que um objeto que herda dessa classe for instanciado.
        /// </summary>
        public Base()
        {
            Id = Guid.NewGuid();
        }
    }
}