using Anderson.Produtos.Domain.Application.Implementations;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.EFImplementations.Repository;
using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Repository.EFImplementations;
using Anderson.Produtos.Domain.Repository.FSImplementations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Anderson.Produtos.Domain.Util
{
    public static class ProdutoDependency
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProdutoContext>(options =>
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    options.UseSqlServer(connectionString);
                    return;
                }
                options.UseInMemoryDatabase("InMemory");
            });

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdutoViewModel, ProdutoDomainModel>();
                cfg.CreateMap<ProdutoDomainModel, ProdutoViewModel>();
            });

            services.AddSingleton(typeof(IMapper), new Mapper(configuration));
            services.AddScoped<IValidationResult, ValidationResult>();
            services.AddTransient<IProdutosAppService, ProdutosAppService>();
            services.AddTransient<IProdutoFileStorage, ProdutoFileSystem>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
