using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.DA
{
    public class ProductAttributeRepository : BaseRepository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(ECommerceDemoEntities context) : base()
        {
        }
        public ProductAttribute Get(long productId, int attributeId)
        {
            return _dbContext.ProductAttributes.SingleOrDefault(p => p.ProductId == productId && p.AttributeId == attributeId);
        }
        public List<ProductAttribute> GetByProduct(long productId)
        {
            return _dbContext.ProductAttributes.Where(p => p.ProductId == productId).ToList();
        }
        public void Add(ProductAttribute productAttribute)
        {
            _dbContext.Database.ExecuteSqlCommand("insert into ProductAttribute(ProductId,AttributeId,AttributeValue)Values(@ProductId,@AttributeId,@AttributeValue)",
                new SqlParameter("ProductId", productAttribute.ProductId),
                new SqlParameter("AttributeId", productAttribute.AttributeId),
                new SqlParameter("AttributeValue", productAttribute.AttributeValue));
        }
        public override void Update(ProductAttribute productAttribute)
        {
            _dbContext.Database.ExecuteSqlCommand("update ProductAttribute set AttributeValue=@AttributeValue where ProductId=@ProductId and AttributeId=@AttributeId",
                new SqlParameter("AttributeValue", productAttribute.AttributeValue),
                new SqlParameter("ProductId", productAttribute.ProductId),
                new SqlParameter("AttributeId", productAttribute.AttributeId));
        }
        public List<ProductAttribute> GetByAttribute(int attributeId)
        {
            return _dbContext.ProductAttributes.Where(p => p.AttributeId== attributeId).ToList();
        }
        public void Delete(long productId, int attributeId)
        {
            _dbContext.Database.ExecuteSqlCommand("delete from ProductAttribute where ProductId=@ProductId and AttributeId=@AttributeId",
                new SqlParameter("ProductId", productId),
                new SqlParameter("AttributeId", attributeId));
        }
    }
    public interface IProductAttributeRepository : IBaseRepository<ProductAttribute>
    {
        void Add(ProductAttribute productAttribute);
        ProductAttribute Get(long productId, int attributeId);
        List<ProductAttribute> GetByProduct(long productId);
        List<ProductAttribute> GetByAttribute(int attributeId);
        void Update(ProductAttribute productAttribute);
        void Delete(long productId, int attributeId);
    }
}
