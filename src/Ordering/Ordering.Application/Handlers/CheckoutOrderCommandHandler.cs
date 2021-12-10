using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.mapping;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepo;

        public CheckoutOrderCommandHandler(IOrderRepository repository)
        {
            this._orderRepo = repository;
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            //mapping among command and entity
            var order = OrderMapper.Mapper.Map<Order>(request);
            if (order == null)
                throw new ApplicationException("order is invalid");

            var orderEntity = await _orderRepo.AddAsync(order);
            var newOrder = OrderMapper.Mapper.Map<OrderResponse>(orderEntity);
            return newOrder;     //reverse mapping in both direction    
        }
    }
}
