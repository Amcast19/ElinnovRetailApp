using ElinnovRetail.Models.Entities;

namespace ElinnovRetail.Data.Context
{
    public class RetailAppDbContext
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
