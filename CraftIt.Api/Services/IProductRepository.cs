using System.Collections.Generic;
using CraftIt.Api.Models;
namespace CraftIt.Api.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetMostPopularProducts();
        Product GetProduct(int id);
        void AddProduct(ProductCreationDto product, User Creator);
        void DeleteProduct(Product product);
        void UpdateProduct(ProductUpdateDto product);
        bool Save();
    }
}