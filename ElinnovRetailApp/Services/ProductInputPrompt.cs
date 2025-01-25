using ElinnovRetail.Models.Common;
using ElinnovRetail.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElinnovRetail.App.Services
{
    public static class ProductInputPrompt
    {
        public static ResultValue<ProductDto> AddProductPromptInput()
        {
            var result = new ResultValue<ProductDto>(Constants.InvalidInput);
            var productDto = new ProductDto();

            Console.WriteLine("");
            Console.WriteLine(" ADD PRODUCT ");
            Console.WriteLine("");
            Console.Write(" Enter product name: ");
            string productName = Console.ReadLine();
            Console.Write(" Enter product price: ");
            string productPrice = Console.ReadLine();
            Console.Write(" Enter product quantity: ");
            string productQuantity = Console.ReadLine();
            
            //Validate product name
            if (string.IsNullOrWhiteSpace(productName))
            {
                result.Message = "Invalid input for product name. Please try again.";
                return result;
            }

            //Validate product quantity
            if (int.TryParse(productQuantity, out int quantity))
            {
                if (quantity < 0)
                {
                    result.Message = "Invalid input for product quantity. Please try again.";
                    return result;
                }
            }
            else
            {
                result.Message = "Invalid input for product quantity. Please try again.";
                return result;
            }

            //Validate product price
            if (decimal.TryParse(productPrice, out decimal price))
            {
                if (price < 0)
                {
                    result.Message = "Invalid input for product price. Please try again.";
                    return result;
                }
            }
            else
            {
                result.Message = "Invalid input for product quantity. Please try again.";
                return result;
            }

            //No validation errors
            //Assign the values to dto
            productDto.Name = productName;
            productDto.Price = price;
            productDto.QuantityInStock = quantity;

            result.Success = true;
            result.ReturnData = productDto;
            return result;
        }

        public static ResultValue<ProductDto> UpdateProductPromptInput()
        {
            var result = new ResultValue<ProductDto>(Constants.InvalidInput);
            var productDto = new ProductDto();
            Console.WriteLine("");
            Console.WriteLine(" UPDATE PRODUCT ");
            Console.WriteLine("");
            Console.Write(" Enter product id: ");
            string productId = Console.ReadLine();
            Console.Write(" Enter product quantity: ");
            string productQuantity = Console.ReadLine();

            //Validate product quantity
            if (!int.TryParse(productId, out int updateProductId))
            {
                result.Message = "Invalid input for product id. Please try again.";
                return result;
            }

            //Validate product quantity
            if (int.TryParse(productQuantity, out int quantity))
            {
                if (quantity < 0)
                {
                    result.Message = "Invalid input for product quantity. Please try again.";
                    return result;
                }
            }
            else
            {
                result.Message = "Invalid input for product quantity. Please try again.";
                return result;
            }

            //Success
            //Assign values to result and dto
            productDto.ProductId = updateProductId;
            productDto.QuantityInStock = quantity;
            result.ReturnData = productDto;
            result.Success = true;
            return result;
        }

        public static ResultValue<ProductDto> DeleteProductPromptInput()
        {
            var result = new ResultValue<ProductDto>(Constants.InvalidInput);
            var productDto = new ProductDto();
            Console.WriteLine("");
            Console.WriteLine(" DELETE PRODUCT ");
            Console.WriteLine("");
            Console.Write(" Enter product id: ");
            string productId = Console.ReadLine();

            //Validate product quantity
            if (!int.TryParse(productId, out int updateProductId))
            {
                result.Message = "Invalid input for product id. Please try again.";
                return result;
            }

            //Success
            //Assign values to result and dto
            productDto.ProductId = updateProductId;
            result.ReturnData = productDto;
            result.Success = true;
            return result;
        }
    }
}
