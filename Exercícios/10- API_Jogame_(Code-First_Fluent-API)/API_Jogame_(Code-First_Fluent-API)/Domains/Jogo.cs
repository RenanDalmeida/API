using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogame__Code_First_Fluent_API_.Domains
{
    public class Jogo : Base
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataLancamento { get; set; }

        //Para relacionar Jogo com Jogador automaticamente, servindo como um relacionamento 1-n entre Jogo e JogosJogadores.
        public ICollection<JogosJogadores> JogosJogadores { get; set; }
    }
}