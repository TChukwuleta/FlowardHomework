using Floward.Domain.Entities;
using Floward.Domain.Enums;
using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Model;
using Floward.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Commands.ProductCommands
{
    public class CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        public CreateProductCommandHandler(IProductRepository productRepository, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }
        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductByName(request.Name);
                if(existingProduct != null)
                {
                    return Result.Failure("A product already exist with that name");
                }
                var newProduct = new Product
                {
                    Name = request.Name,
                    Status = Status.Active,
                    StatusDesc = Status.Active.ToString(),
                    Price = request.Price,
                    CreatedDate = DateTime.Now,
                    ProductImage = request.ProductImage,
                    ProductType = request.ProductType
                };
                var result = await _productRepository.AddAsync(newProduct);
                return Result.Success("Product creation was successful", result);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Product creation was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
