using Microsoft.EntityFrameworkCore;

namespace Anderson.Produtos.Domain.Repository.EFImplementations
{
    internal class ProductContext : DbContext
    {
        public DbSet<ProductDomainModel> Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            // if it grow up use 'Fluent Api' Mapping             
            modelBuilder.Entity<ProductDomainModel>().HasKey(x=> x.Id);            
        }
    }
}
