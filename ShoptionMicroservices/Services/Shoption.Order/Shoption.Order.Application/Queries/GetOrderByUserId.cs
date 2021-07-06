using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shoption.Order.Application.Dtos;
using Shoption.Order.Application.Mapping;
using Shoption.Services.Order.Infrastructure;
using Shoption.Shared.Dto;

namespace Shoption.Order.Application.Queries
{
    public class GetOrderByUserId : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }

    public class GetOderByUserIdHandler : IRequestHandler<GetOrderByUserId, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbContext;

        public GetOderByUserIdHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrderByUserId request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}
