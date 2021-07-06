using System;
using System.Threading.Tasks;
using MassTransit;
using Shoption.Services.Order.Infrastructure;
using Shoption.Shared.Messages;

namespace Shoption.Services.Order.Application.Consumer
{
    public class CreateOrderMessageCommandConsumer: IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.Address(
                context.Message.Province,
                context.Message.District,
                context.Message.Street,
                context.Message.ZipCode,
                context.Message.Line);

            Domain.Order order = new Domain.Order(context.Message.BuyerId, newAddress);

            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _orderDbContext.Orders.AddAsync(order);

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
