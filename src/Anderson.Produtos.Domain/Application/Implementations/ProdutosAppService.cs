using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Util;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Anderson.Produtos.IntegrationTest")]
namespace Anderson.Produtos.Domain.Application.Implementations
{
    internal sealed class ProdutosAppService : IProdutosAppService
    {
        private const string ProdutoNaoEncontrado = "Produto não encontrado";
        private const string AlgoInesperadoAconteceu = "Algo inesperado aconteceu";
        private const string ImagemEObrigatorio = "Imagem é obrigatório";

        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoFileStorage _produtoFileStorage;
        private readonly IValidationResult _validationResult;
        private readonly IMapper _mapper;

        public ProdutosAppService(IProdutoRepository produtoRepository,
            IProdutoFileStorage produtoFileStorage,
            IValidationResult validationResult,
            IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _produtoFileStorage = produtoFileStorage;
            _mapper = mapper;
            _validationResult = validationResult;
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetAllAsync()
        {
            return (await _produtoRepository.GetAllAsync())
                .Select(x => _mapper.Map<ProdutoViewModel>(x));
        }

        public async Task<ProdutoViewModel> GetByIdAsync(long id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                _validationResult.AddError(ProdutoNaoEncontrado);
                return null;
            }

            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public async Task CreateAsync(ProdutoViewModel viewModel)
        {
            var domainModel = _mapper.Map<ProdutoDomainModel>(viewModel);

            if (!domainModel.IsValid(_validationResult))
                return;

            if (viewModel?.ImagemArquivo is null)
            {
                _validationResult.AddError(ImagemEObrigatorio);
                return;
            }

            using (var transaction = _produtoRepository.BeginTransacion())
            {
                try
                {
                    domainModel.ImagemPath = await _produtoFileStorage.InsertAsync(viewModel.ImagemArquivo.OpenReadStream());
                    await _produtoRepository.CreateAsync(domainModel);
                    await transaction.CommitAsync();
                }
                catch (System.Exception e)
                {
                    _validationResult.Erros.Add(AlgoInesperadoAconteceu);
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task UpdateAsync(ProdutoViewModel viewModel)
        {
            var domainModel = await _produtoRepository.GetByIdAsync(viewModel.Id);
            if (domainModel is null)
            {
                _validationResult.AddError(ProdutoNaoEncontrado);
                return;
            }

            domainModel = domainModel.RealizarManutencao(viewModel);

            if (!domainModel.IsValid(_validationResult))
                return;

            using (var transaction = _produtoRepository.BeginTransacion())
            {
                try
                {
                    if (viewModel?.ImagemArquivo is not null)
                    {
                        domainModel.ImagemPath = await _produtoFileStorage.InsertAsync(viewModel.ImagemArquivo.OpenReadStream());
                    }

                    await _produtoRepository.UpdateAsync(domainModel);
                    await transaction.CommitAsync();
                }
                catch (System.Exception e)
                {
                    _validationResult.Erros.Add(AlgoInesperadoAconteceu);
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task DeleteAsync(long id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto is null)
            {
                _validationResult.AddError(ProdutoNaoEncontrado);
                return;
            }

            using (var transaction = _produtoRepository.BeginTransacion())
            {
                try
                {
                    await _produtoRepository.DeleteAsync(id);
                    await transaction.CommitAsync();
                }
                catch (System.Exception e)
                {
                    _validationResult.Erros.Add(AlgoInesperadoAconteceu);
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
