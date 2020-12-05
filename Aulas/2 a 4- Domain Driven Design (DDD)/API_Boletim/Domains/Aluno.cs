using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Boletim.Domains
{
    public class Aluno
    {
        /*Aqui no domínio ficam todos os atributos.*/
        public int IdAluno { get; set; }
        public string Nome { get; set; }
        public string RA { get; set; }
        public int Idade { get; set; }
    }
}
