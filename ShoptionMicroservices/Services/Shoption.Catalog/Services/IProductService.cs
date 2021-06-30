using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shoption.Catalog.Dto;
using Shoption.Catalog.Models;
using Shoption.Shared.Dto;

namespace Shoption.Catalog.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> GetAllAsync();
        Task<Response<ProductDto>> GetByIdAsync(string id);
        Task<Response<ProductDto>> CreateAsync(ProductCreateDto productCreateDto);
        Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);

    }
}
