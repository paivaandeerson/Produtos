using Microsoft.EntityFrameworkCore;

namespace Anderson.Produtos.Domain.Repository.EFImplementations
{
    internal class ProdutoContext : DbContext
    {
        public DbSet<ProdutoDomainModel> ProdutoModels { get; set; }
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //em um projeto real eu usaria 
            // o 'Fluent Api' Mapping pra cada entidade implementando o IEntityTypeConfiguration<TEntity>
            //ao invés de configurar aqui
            modelBuilder.Entity<ProdutoDomainModel>().HasKey(x=> x.Id);            
        }
    }
}
