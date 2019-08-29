using System.Collections.Generic;
using System.Collections.ObjectModel;
using CraftIt.Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace CraftIt.Api.Services
{
    public class ProductRepository : IProductRepository
    {

        private CraftItContext _context;

        public ProductRepository(CraftItContext context)
        {
            _context = context;
        }

        public void AddProduct(ProductCreationDto productDto)
        {
            var product = new Product{
                Name = productDto.Name,
                Description = productDto.Description,
                TimeEstimate = productDto.TimeEstimate,
                Requirements = JsonConvert.SerializeObject(productDto.Requirements),
                Instructions = CreateInstructions(productDto)
            };
            _context.Products.Add(product);
        }

        private ICollection<Instruction> CreateInstructions(ProductCreationDto productDto)
        {

            var instructions = productDto.Instructions.Select((x, index) => new Instruction{
                Description = x.Description,
                Ordinal = index
            }).ToList();

            return instructions;
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(x => x.Instructions);
        }

        public IEnumerable<Product> GetMostPopularProducts()
        {
            throw new System.NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Include(x => x.Instructions).FirstOrDefault(x => x.Id == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateProduct(int id, ProductCreationDto product)
        {
            var productToUpdate = GetProduct(id);

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.TimeEstimate = product.TimeEstimate;
            productToUpdate.Requirements = JsonConvert.SerializeObject(product.Requirements);
            productToUpdate.Instructions = CreateInstructions(product);
            _context.SaveChanges();

            return;
        }
    }
}