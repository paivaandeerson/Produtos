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
    internal sealed class ProductAppService : IProductAppService
    {
        private const string ProductNotFound = "Produto não encontrado";
        private const string UnexpectedError = "Algo inesperado aconteceu";

        private readonly IProductRepository _productRepository;
        private readonly IProductFileStorage _productFileStorage;
        private readonly IValidationResult _validationResult;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository,
            IProductFileStorage productFileStorage,
            IValidationResult validationResult,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productFileStorage = productFileStorage;
            _mapper = mapper;
            _validationResult = validationResult;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return (await _productRepository.GetAllAsync())
                .Select(x => _mapper.Map<ProductViewModel>(x));
        }

        public async Task<ProductViewModel> GetByIdAsync(long id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }

        public async Task CreateAsync(ProductViewModel viewModel)
        {
            var domainModel = _mapper.Map<ProductDomainModel>(viewModel);

            if (!domainModel.IsValid(_validationResult))
                return;

            using (var transaction = _productRepository.BeginTransacion())
            {
                try
                {
                    //domainModel.ImagePath = await _productFileStorage.InsertAsync(viewModel.ImagePath.OpenReadStream());
                    await _productRepository.CreateAsync(domainModel);
                    await transaction.CommitAsync();

                    viewModel.Id = domainModel.Id;
                }
                catch (System.Exception e)
                {
                    _validationResult.Errors.Add(UnexpectedError);
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task UpdateAsync(ProductViewModel viewModel)
        {
            var domainModel = await _productRepository.GetByIdAsync(viewModel.Id);
            if (domainModel is null)
            {
                _validationResult.AddError(ProductNotFound);
                return;
            }

            domainModel = domainModel.Maintain(viewModel);

            if (!domainModel.IsValid(_validationResult))
                return;

            using (var transaction = _productRepository.BeginTransacion())
            {
                try
                {
                    if (viewModel?.ImageStream is not null)
                    {
                        domainModel.ImagePath = await _productFileStorage.InsertAsync(viewModel.ImageStream.OpenReadStream());
                    }

                    await _productRepository.UpdateAsync(domainModel);
                    await transaction.CommitAsync();
                }
                catch (System.Exception e)
                {
                    _validationResult.Errors.Add(UnexpectedError);
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task DeleteAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                _validationResult.AddError(ProductNotFound);
                return;
            }

            using (var transaction = _productRepository.BeginTransacion())
            {
                try
                {
                    await _productRepository.DeleteAsync(id);
                    await transaction.CommitAsync();
                }
                catch (System.Exception e)
                {
                    _validationResult.Errors.Add(UnexpectedError);
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
