using System.Collections.Generic;
using CraftIt.Api.Models;
namespace CraftIt.Api.Services
{
    /// <summary>Class <c>IProductRepository</c>Class used to set out the required interactions with the database for products.</summary>
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        void AddProduct(ProductCreationDto product, User Creator);
        void DeleteProduct(Product product);
        void UpdateProduct(ProductUpdateDto product);
        bool Save();
    }
}