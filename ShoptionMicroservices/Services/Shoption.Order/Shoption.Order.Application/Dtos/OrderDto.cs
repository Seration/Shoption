using System;
using System.Collections.Generic;

namespace Shoption.Order.Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public AddressDto Address { get; set; }
        public string BuyerId { get; set; }

        public List<OrderItemDto> _orderItems { get; set; }
    }
}
