using ElinnovRetail.Models.Entities;

namespace ElinnovRetail.Models.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }

    //Mapping extension
    public static class ProductMappingExtensions
    {
        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            };
        }

        public static Product MapToEntity(this ProductDto productDto)
        {
            return new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Price = productDto.Price,
                QuantityInStock = productDto.QuantityInStock
            };
        }
    }

}
