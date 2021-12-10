using MediatR;
using Ordering.Application.mapping;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using Ordering.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class GetOrderByUserNameHandler : IRequestHandler<GetOrdersByUserNameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepo;

        public GetOrderByUserNameHandler( IOrderRepository repository )
        {
            _orderRepo = repository;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            var data = await _orderRepo.GetOrdersByUserName(request.UserName);
            //mapping
            var orders = OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(data);

            return orders;
        }

    }
}
