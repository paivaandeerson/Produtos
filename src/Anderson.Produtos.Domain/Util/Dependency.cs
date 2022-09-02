using Anderson.Produtos.Domain.Application.Implementations;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.EFImplementations.Repository;
using Anderson.Produtos.Domain.Repository;
using Anderson.Produtos.Domain.Repository.EFImplementations;
using Anderson.Produtos.Domain.Repository.FSImplementations;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Anderson.Produtos.Domain.Util
{
    public static class ProdutoDependency
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductViewModel, ProductDomainModel>();
                cfg.CreateMap<ProductDomainModel, ProductViewModel>();
            });

            services.AddSingleton(typeof(IMapper), new Mapper(mapperConfiguration));
            services.AddScoped<IValidationResult, ValidationResult>();
            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<IProductFileStorage, ProdutoFileSystem>();
            services.AddScoped<IProductRepository, ProductRepository>();


            var connectionString = configuration.GetConnectionString("ProductDB");

            if (configuration.GetSection("Environment")?.Value == "NarrowIntegratedTest")
            {
                string id = string.Format("{0}.db", Guid.NewGuid().ToString());

                var builder = new SqliteConnectionStringBuilder()
                {
                    DataSource = id,
                    Mode = SqliteOpenMode.Memory,
                    Cache = SqliteCacheMode.Shared
                };

                var connection = new SqliteConnection(builder.ConnectionString);
                connection.Open();
                connection.EnableExtensions(true);
                services.AddDbContext<ProductContext>(options => options.UseSqlite(connection));

                return;
            }

            services.AddDbContext<ProductContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
