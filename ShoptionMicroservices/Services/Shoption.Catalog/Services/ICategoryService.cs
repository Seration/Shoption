using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shoption.Catalog.Dto;
using Shoption.Catalog.Models;
using Shoption.Shared.Dto;

namespace Shoption.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(Category category);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
