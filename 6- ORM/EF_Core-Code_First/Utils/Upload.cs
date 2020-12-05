using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EF_Core_Code_First.Utils
{
    public static class Upload
    {
        public static string Local(IFormFile file)
        {
            //Duas pessoas diferentes, podem cadastrar duas imagens com nomes diferentes. Isso faria uma confusão no banco, pois elas usariam o mesmo caminho, mas seriam diferentes. Por isso o guid. Além disso, um guid tem tracinhos, então os tiramos, apenas por estética. Só tem mais uma coisa: Nós mudamos o nome, mas ainda queremos a extensão do arquivo. Então, utilizamos outro método.
            var nomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

            //Agora que já temos o nome, vamos criar o caminho. Para isso, criamos em nosso projeto algumas pastas, como a wwwroot, que guardará arquivos estáticos (html, css, js, mp3, jpg, etc.) O wwwroot é a raiz, onde fica ali o começo da aplicação, o site em si, etc.
            //O path combine está combinando o primeiro diretório (C:, D:, etc.) do pc ou do servidor da nuvem (onde quer q a api esteja) até o local atual, e depois o caminho final onde ficará a nossa imagem. Assim temos o caminho absoluto. 
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Upload/Imagens", nomeArquivo);

            //Agora, já temos o caminho completo da imagem. Agora precisamos criar esse caminho e essa imagem.
            using var streamImagem = new FileStream(caminhoArquivo, FileMode.Create);

            //Já com o caminho criado, adicionamos a imagem nele.
            file.CopyTo(streamImagem);

            //OBS: ISSO TUDO SALVA NO SERVIDOR, E NÃO NO BANCO. O COMANDO QUE SALVA NO BANCO O CAMINHO ESTÁ ABAIXO.

            //Passamos a url a partir de nossa pasta atual. Não colocamos o wwwroot pois está subentendido. Fazendo apenas isso não vai funcionar, vc precisa ajustar algumas coisas no Startup.
            return "https://localhost:44329/Upload/Imagens/" + nomeArquivo;
        }
    }
}