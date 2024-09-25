using InfinionProduct_Application.DTOs.ProductDTOs;
using InfinionProduct_Core.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinionProduct_Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductsWithFilterAsync(ProductFilterDto filter);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(string id, UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
    }
}
