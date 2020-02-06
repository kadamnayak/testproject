using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Contrado.DA
{
    public partial class ContradoDBContext : DbContext, IContradoDBContext
    {
        public ContradoDBContext()
            : base("name=ContradoDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttributeLookup> ProductAttributeLookups { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
    }
    public interface IContradoDBContext : IDisposable
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductAttributeLookup> ProductAttributeLookups { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<ProductAttribute> ProductAttributes { get; set; }

        int SaveChanges();
    }
}
