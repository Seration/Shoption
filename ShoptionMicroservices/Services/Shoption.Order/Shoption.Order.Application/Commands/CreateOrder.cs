using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shoption.Order.Application.Dtos;
using Shoption.Order.Domain.OrderAggregate;
using Shoption.Order.Infrastructure;
using Shoption.Shared.Dto;

namespace Shoption.Order.Application.Commands
{
    public class CreateOrder : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrder, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        async Task<Response<CreatedOrderDto>> IRequestHandler<CreateOrder, Response<CreatedOrderDto>>.Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(
                request.Address.District,
                request.Address.Line,
                request.Address.Province,
                request.Address.Street,
                request.Address.ZipCode
            );

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(newAddress, request.BuyerId);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OderId = newOrder.Id }, 200);
        }
    }
}
