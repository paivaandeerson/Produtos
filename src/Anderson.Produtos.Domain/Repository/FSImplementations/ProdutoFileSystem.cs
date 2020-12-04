using System;
using System.IO;
using System.Threading.Tasks;

namespace Anderson.Produtos.Domain.Repository.FSImplementations
{
    internal class ProdutoFileSystem : IProdutoFileStorage
    {
        public const string ImagemBasePath = "C:\\Produto\\Imagens\\";//deveria buscar do appsettings

        public async Task<string> InsertAsync(Stream imagem)
        {
            if (imagem is null)
                return string.Empty;

            var nomeArquivo = $"{ImagemBasePath}{Guid.NewGuid()}.jpg";
            Directory.CreateDirectory(ImagemBasePath);

            using (var fileStream = new FileStream(nomeArquivo, FileMode.Create))
            {
                await imagem.CopyToAsync(fileStream);
            }

            return nomeArquivo;
        }
    }
}
