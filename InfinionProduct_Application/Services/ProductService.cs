using AutoMapper;
using InfinionProduct_Application.DTOs.ProductDTOs;
using InfinionProduct_Application.Interfaces;
using InfinionProduct_Core.DTOs.ProductDTOs;
using InfinionProduct_Core.Entities;
using InfinionProduct_Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace InfinionProduct_Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsWithFilterAsync(ProductFilterDto filter)
        {
            var products = await _productRepository.GetAllMatchingAsync(filter.SearchTerm,
                filter.MinPrice,
                filter.MinPrice,
                filter.PageNumber,
                filter.PageSize);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }


        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.Now;
            var createdProduct = await _productRepository.CreateProductAsync(product);
            _logger.LogInformation("Created new product with ID: {ProductId}", createdProduct.Id);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task UpdateProductAsync(string id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Attempted to update non-existent product with ID: {ProductId}", id);
                throw new Exception("Product not found");
            }
            var result = _mapper.Map<Product>(updateProductDto);

            await _productRepository.UpdateProductAsync(result);
            _logger.LogInformation("Updated product with ID: {ProductId}", id);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productRepository.DeleteProductAsync(id);
            _logger.LogInformation("Deleted product with ID: {ProductId}", id);
        }

    }
}
