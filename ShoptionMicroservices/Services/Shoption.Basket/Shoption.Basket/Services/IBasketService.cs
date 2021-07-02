
using System;
using System.Threading.Tasks;
using Shoption.Basket.Dtos;
using Shoption.Shared.Dto;

namespace Shoption.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
