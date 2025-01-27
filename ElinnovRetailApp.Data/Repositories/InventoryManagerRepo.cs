using ElinnovRetail.Data.Context;
using ElinnovRetail.Data.Repositories.Interfaces;
using ElinnovRetail.Models.Common;
using ElinnovRetail.Models.DTOs;
using ElinnovRetail.Models.Entities;

namespace ElinnovRetail.Data.Repositories
{
    public class InventoryManagerRepo : IInventoryManagerRepo
    {
        private readonly RetailAppDbContext _context;
        public InventoryManagerRepo(RetailAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new product to the product list by first determining the next available <see cref="Product.ProductId"/>.
        /// The <see cref="Product.ProductId"/> is incremented from the current maximum, and the new <see cref="Product"/> is added to the context.
        /// After saving the <see cref="Product"/>, the method verifies whether the product was successfully added by checking 
        /// if it exists in the lisst with the new <see cref="Product.ProductId"/>.
        /// </summary>
        /// <param name="product">The product to add</param>
        /// <returns>A <see cref="ResultValue"/> indicating whether the operation was successful, 
        /// including a success status and a corresponding message.</returns>
        public ResultValue AddProduct(Product product)
        {
            var result = new ResultValue(Constants.RecordNotSavedMessage);

            //Get and increment the max product id
            int productId = _context.Products.Count > 0 ? (_context.Products.Max(a => a.ProductId) + 1) : 1;

            //Assign as new product id
            product.ProductId = productId;

            _context.Products.Add(product);

            //Check if the product now exists
            if (_context.Products.Any(a => a.ProductId == productId))
            {
                result.Success = true;
                result.Message = Constants.RecordSavedMessage;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Removes the <see cref="Product"/> from the product list by checking if the specified product exists
        /// with the specified <see cref="Product.ProductId"/>. After removing, it verifies wether the product was successfully removed
        /// by checking if it exists in the list with the ProductId.
        /// </summary>
        /// <param name="productId">The ProductId of the Product to be deleted.</param>
        /// <returns>A <see cref="ResultValue"/> indicating whether the operation was successful, including a success status and message.</returns>
        public ResultValue RemoveProduct(int productId)
        {
            var result = new ResultValue(Constants.RecordNotDeletedMessage);
            var product = GetById(productId);

            if(product == null)
            {
                result.Message = Constants.RecordNotFoundMessage;
                return result;
            }

            //Remove the product from the list
            _context.Products.Remove(product);

            //Check if the product still exists
            if (_context.Products.Any(a => a.ProductId == productId))
            {
                return result;
            }

            //Successfully deleted
            result.Success = true;
            result.Message = Constants.RecordDeletedMessage;
            return result;
        }

        /// <summary>
        /// Updates the <see cref="Product"/> with the specified <see cref="Product.ProductId"/> and updates its quantity.
        /// After updating, it verifies wether the product was successfully updated
        /// by checking if it exists in the list by using the ProductId and NewQuantity.
        /// </summary>
        /// <param name="productId">The productId of the Product to be updated.</param>
        /// <param name="newQuantity">The new quantity to assign to the product.</param>
        /// <returns>A <see cref="ResultValue"/> indicating whether the operation was successful, including a success status and message.</returns>
        public ResultValue UpdateProduct(int productId, int newQuantity)
        {
            var result = new ResultValue(Constants.RecordNotUpdatedMessage);
            var product = GetById(productId);

            if (product == null)
            {
                result.Message = Constants.RecordNotFoundMessage;
                return result;
            }

            if (product.QuantityInStock == newQuantity)
            {
                result.Message = "No changes made.";
                return result;
            }

            //Assign the new quantity to the product
            product.QuantityInStock = newQuantity;

            //Check if the product with id and quantity exists
            if (_context.Products.Any(a => a.ProductId == productId && a.QuantityInStock == newQuantity))
            {
                result.Success = true;
                result.Message = Constants.RecordUdpatedMessage;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Retrieves the list of products.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{ProductDto}"/> containing the products.</returns>
        public IEnumerable<ProductDto> ListProducts()
        {
            return _context.Products.Select(a => a.MapToDto());
        }

        /// <summary>
        /// Calculates and retrieves the total value of the inventory based on the current stock and product prices.
        /// </summary>
        /// <returns>The total value of the inventory as a <see cref="decimal"/>.</returns>
        public decimal GetTotalValue()
        {
            var total = _context.Products.Where(a => a.QuantityInStock > 0).Sum(a => a.Price * a.QuantityInStock);
            return total;
        }

        /// <summary>
        /// Retrieves a <see cref="Product"/> from the context based on its unique ProductId.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve.</param>
        /// <returns>Returns the <see cref="Product"/> object if found, or null if no product with the specified ProductId exists.</returns>
        public Product? GetById(int productId)
        {
            return _context.Products.FirstOrDefault(a => a.ProductId == productId);
        }
    }
}
