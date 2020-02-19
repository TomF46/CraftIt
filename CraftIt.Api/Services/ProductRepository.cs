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
    /// <summary>Class <c>ProductRepository</c>Class used to implement methods in the IProductRepository interface</summary>    
    public class ProductRepository : IProductRepository
    {

        private CraftItContext _context;


        public ProductRepository(CraftItContext context)
        {
            _context = context;
        }

        public void AddProduct(ProductCreationDto productDto, User creator)
        {

            if(creator == null) throw new AppException("User not found");

            var product = new Product{
                Name = productDto.Name,
                Description = productDto.Description,
                AddedBy = creator,
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

            //Remove instructions first as they get orphaned and re added with updated data in next step
            _context.Instructions.RemoveRange(productToUpdate.Instructions);

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
