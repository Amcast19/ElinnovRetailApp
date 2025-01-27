using ElinnovRetail.App.Extensions;
using ElinnovRetail.App.Services;
using ElinnovRetail.Data.Repositories.Interfaces;
using ElinnovRetail.Models.Common;
using ElinnovRetail.Models.DTOs;
using System.Reflection.Metadata;
using static ElinnovRetail.Models.Common.Enums;

namespace ElinnovRetail.App
{
    public class InventoryMain
    {
        private readonly IInventoryManager _inventoryManager;
        public InventoryMain(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        //Start here
        public void Run()
        {
            LoadMenuOptions();
        }

        /// <summary>
        /// Render the ADD, UPDATE, DELETE, LIST, TOTAL VALUE product menu options
        /// </summary>
        public void LoadMenuOptions(string message = "")
        {
            ConsoleRenderer.RenderHeader();
            ConsoleRenderer.RenderMessage(message);

            //Display ADD, UPDATE, DELETE, LIST, TOTAL VALUE options
            ConsoleRenderer.RenderManageProductOptions();

            //Read the user input
            int chosenOption = NavigationPrompt.MenuOptionsPromptInput();
            LoadProductUIFromOption(chosenOption);
        }

        /// <summary>
        /// Decides which product UI to display based on the user input
        /// </summary>
        public void LoadProductUIFromOption(int chosenMenuOption)
        {
            switch (chosenMenuOption)
            {
                case (int)ManageProductOptionsEnum.Add_Product:
                    LoadAddProductUI();
                    break;
                case (int)ManageProductOptionsEnum.Update_Product:
                    LoadUpdateProductUI();
                    break;
                case (int)ManageProductOptionsEnum.Remove_Product:
                    LoadDeleteProductUI();
                    break;
                case (int)ManageProductOptionsEnum.Product_List:
                    LoadProductTableUI();
                    break;
                case (int)ManageProductOptionsEnum.Show_Total_Value:
                    LoadProductTotalValueUI();
                    break;
                default:
                    //Reload the inventory main UI with warning message
                    LoadMenuOptions(Constants.InvalidInput);
                    break;
            }
        }

        #region MENU PRODUCT OPTIONS UI

        /// <summary>
        /// Displays the product add UI
        /// </summary>
        public void LoadAddProductUI(string message = "")
        {
            while (true)
            {
                ConsoleRenderer.RenderHeader();
                ConsoleRenderer.RenderMessage(message);

                // Get user input from add product
                var addProductInput = ProductInputPrompt.AddProductPromptInput();
                if (!addProductInput.Success)
                {
                    message = addProductInput.Message;
                    continue; // Retry
                }

                bool confirmSave = NavigationPrompt.ConfirmSavePromptInput();
                if (!confirmSave)
                {
                    // Go back to menu
                    LoadMenuOptions(Constants.RecordNotSavedMessage);
                    break;
                }

                var addProduct = _inventoryManager.AddProduct(addProductInput.ReturnData.MapToEntity());
                if (!addProduct.Success)
                {
                    message = addProduct.Message;
                    continue; // Retry
                }

                // Go back to menu
                LoadMenuOptions(addProduct.Message);
                break;
            }
        }

        /// <summary>
        /// Displays the product update UI
        /// </summary>
        public void LoadUpdateProductUI(string message = "")
        {
            while (true)
            {
                ConsoleRenderer.RenderHeader();
                ConsoleRenderer.RenderMessage(message);

                // Get user input from add product
                var updateProductInput = ProductInputPrompt.UpdateProductPromptInput();
                if (!updateProductInput.Success)
                {
                    message = updateProductInput.Message;
                    continue; // Retry
                }

                bool confirmSave = NavigationPrompt.ConfirmSavePromptInput();
                if (!confirmSave)
                {
                    // Go back to menu
                    LoadMenuOptions(Constants.RecordNotUpdatedMessage);
                    break;
                }

                int productId = updateProductInput.ReturnData.ProductId;
                int newQuantity = updateProductInput.ReturnData.QuantityInStock;

                var updateProduct = _inventoryManager.UpdateProduct(productId, newQuantity);
                if (!updateProduct.Success)
                {
                    message = updateProduct.Message;
                    continue; // Retry
                }

                // Go back to menu
                LoadMenuOptions(updateProduct.Message);
                break;
            }
        }

        /// <summary>
        /// Displays the product delete confirmation UI
        /// </summary>
        public void LoadDeleteProductUI(string message = "")
        {
            while (true)
            {
                ConsoleRenderer.RenderHeader();
                ConsoleRenderer.RenderMessage(message);

                // Get user input from add product
                var deleteProductInput = ProductInputPrompt.DeleteProductPromptInput();
                if (!deleteProductInput.Success)
                {
                    message = deleteProductInput.Message;
                    continue; // Retry
                }

                bool confirmSave = NavigationPrompt.ConfirmSavePromptInput();
                if (!confirmSave)
                {
                    // Go back to menu
                    LoadMenuOptions(Constants.RecordNotDeletedMessage);
                    break;
                }

                int productId = deleteProductInput.ReturnData.ProductId;

                var deleteProduct = _inventoryManager.RemoveProduct(productId);
                if (!deleteProduct.Success)
                {
                    message = deleteProduct.Message;
                    continue; // Retry
                }

                // Go back to menu
                LoadMenuOptions(deleteProduct.Message);
                break;
            }
        }

        /// <summary>
        /// Displays the products table UI
        /// </summary>
        public void LoadProductTableUI()
        {
            var tableHeaders = new List<string> { "Product Id", "Product Name", "Quantity", "Price" };
            while (true)
            {
                ConsoleRenderer.RenderHeader();
                var list = _inventoryManager.ListProducts();
                decimal totalValue = _inventoryManager.GetTotalValue();
                Console.WriteLine("");
                ConsoleRenderer.RenderTable("PRODUCT LIST", tableHeaders, list);
                Console.WriteLine("");
                Console.WriteLine($" TOTAL VALUE: {totalValue.ConvertDecimalToCurrency()}");
                Console.WriteLine("");

                bool backToMenu = NavigationPrompt.BackToMenuPromptInput();
                if (!backToMenu)
                {
                    continue;
                }

                //Back to menu
                LoadMenuOptions();
                break;
            }
        }

        /// <summary>
        /// Displays the total value of the inventory
        /// </summary>
        public void LoadProductTotalValueUI()
        {
            while (true)
            {
                ConsoleRenderer.RenderHeader();
                decimal totalValue = _inventoryManager.GetTotalValue();
                Console.WriteLine("");
                Console.WriteLine($" TOTAL VALUE: {totalValue.ConvertDecimalToCurrency()}");
                Console.WriteLine("");
                bool backToMenu = NavigationPrompt.BackToMenuPromptInput();
                if (!backToMenu)
                {
                    continue;
                }

                //Back to menu
                LoadMenuOptions();
                break;
            }
        }

        #endregion
    }
}
