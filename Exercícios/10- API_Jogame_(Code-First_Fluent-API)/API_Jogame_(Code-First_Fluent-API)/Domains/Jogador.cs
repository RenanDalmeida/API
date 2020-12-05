using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogame__Code_First_Fluent_API_.Domains
{
    public class Jogador : Base
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

        //Para relacionar Jogador com Jogo automaticamente, servindo como um relacionamento 1-n entre Jogadores e JogosJogadores.
        public ICollection<JogosJogadores> JogosJogadores { get; set; }
    }
}