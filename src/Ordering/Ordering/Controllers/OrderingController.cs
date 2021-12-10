using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Controllers
{
    [ApiController]
    [Route("api/v1/ordering")]
    public class OrderingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrderByUserName(string userName)
        {
            var query = new GetOrdersByUserNameQuery(userName);
            var orders = await _mediator.Send(query);

            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof (OrderResponse) , (int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<CheckoutOrderCommand>> PostCheckoutOrder([FromBody] CheckoutOrderCommand orderCommand)
        {
            if (orderCommand == null)
                return NotFound();
            var order = await _mediator.Send(orderCommand);
            return Ok(order);
        }
    }
}
