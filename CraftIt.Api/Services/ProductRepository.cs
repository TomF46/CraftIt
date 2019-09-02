using System.Collections.Generic;
using System.Collections.ObjectModel;
using CraftIt.Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Http;
using CraftIt.Api.Helpers;

namespace CraftIt.Api.Services
{
    public class ProductRepository : IProductRepository
    {

        private CraftItContext _context;
        private IHttpContextAccessor _contextAccessor;


        public ProductRepository(CraftItContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public void AddProduct(ProductCreationDto productDto)
        {

            var user = _context.Users.FirstOrDefault(x => x.Id == int.Parse(_contextAccessor.HttpContext.User.Identity.Name));

            if(user == null) throw new AppException("User not found");

            var product = new Product{
                Name = productDto.Name,
                Description = productDto.Description,
                AddedBy = user,
                TimeEstimate = productDto.TimeEstimate,
                Requirements = JsonConvert.SerializeObject(productDto.Requirements),
                Instructions = CreateInstructions(productDto.Instructions),
                ProductImage = productDto.ProductImage != null ? ConvertBase64ToBinary(productDto.ProductImage) : null
            };
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetMostPopularProducts()
        {
            throw new System.NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Include(x => x.Instructions).Include(x => x.AddedBy).Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateProduct(ProductUpdateDto product)
        {
            var productToUpdate = GetProduct(product.Id);

            if(productToUpdate.AddedBy.Id != int.Parse(_contextAccessor.HttpContext.User.Identity.Name)) throw new AppException("User does not have permission to update this product");

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.TimeEstimate = product.TimeEstimate;
            productToUpdate.Requirements = JsonConvert.SerializeObject(product.Requirements);
            productToUpdate.Instructions = CreateInstructions(product.Instructions);
            productToUpdate.ProductImage =  product.ProductImage != null ? Convert.FromBase64String(product.ProductImage) : null;
            _context.SaveChanges();

            return;
        }

        private ICollection<Instruction> CreateInstructions(ICollection<InstructionCreationDto> instructionDtos)
        {

            var instructions = instructionDtos.Select((x, index) => new Instruction{
                Description = x.Description,
                Image = x.Image != null ? ConvertBase64ToBinary(x.Image) : null,
                Ordinal = index
            }).ToList();

            return instructions;
        }

        private byte[] ConvertBase64ToBinary(string base64String){
            //strip out useless metadata that causes conversion to fail
            if(base64String.Contains(',')) base64String = base64String.Split(',')[1];
            return Convert.FromBase64String(base64String);
        }
    }
}
