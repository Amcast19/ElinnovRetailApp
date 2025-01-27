using ElinnovRetail.Models.Common;
using ElinnovRetail.Models.DTOs;
using ElinnovRetail.Models.Entities;

namespace ElinnovRetail.Data.Repositories.Interfaces
{
    public interface IInventoryManagerRepo
    {
        /// <summary>
        /// Adds a new <see cref="Product"/> to the product list.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>A <see cref="ResultValue"/> indicating whether the operation was successful, including a success status and message.</returns>
        ResultValue AddProduct(Product product);

        /// <summary>
        /// Removes the <see cref="Product"/> from the product list with the specified ProductId.
        /// </summary>
        /// <param name="productId">The productId of the <see cref="Product"/> to be deleted.</param>
        /// <returns>A <see cref="ResultValue"/> indicating whether the operation was successful, including a success status and message.</returns>
        ResultValue RemoveProduct(int productId);

        /// <summary>
        /// Updates the <see cref="Product"/> with the specified <see cref="Product.ProductId"/> and updates its quantity.
        /// </summary>
        /// <param name="productId">The productId of the Product to be updated.</param>
        /// <param name="newQuantity">The new quantity to assign to the product.</param>
        /// <returns>A <see cref="ResultValue"/> indicating whether the operation was successful, including a success status and message.</returns>
        ResultValue UpdateProduct(int productId, int newQuantity);

        /// <summary>
        /// Retrieves the list of products.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{ProductDto}"/> containing the products.</returns>
        IEnumerable<ProductDto> ListProducts();

        /// <summary>
        /// Calculates and retrieves the total value of the inventory
        /// </summary>
        /// <returns>The total value of the inventory as a <see cref="decimal"/>.</returns>
        decimal GetTotalValue();

        /// <summary>
        /// Retrieves a <see cref="Product"/> from the context based on its unique ProductId.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve.</param>
        /// <returns>Returns the <see cref="Product"/> object if found, or null if no product with the specified ProductId exists.</returns>
        Product? GetById(int productId);
    }
}
