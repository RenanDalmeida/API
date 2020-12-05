using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogame__Code_First_Fluent_API_.Domains
{
    public class JogosJogadores : Base
    {
        public Guid IdJogo { get; set; }
        public Jogo Jogo { get; set; }

        public Guid IdJogador { get; set; }
        public Jogador Jogador { get; set; }
    }
}