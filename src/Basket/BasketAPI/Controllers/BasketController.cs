using AutoMapper;
using BasketAPI.Entities;
using BasketAPI.Repositories;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Producers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repo;
        private readonly IMapper _mapper;
        private readonly BasketCheckoutEventProducer _eventProducer;

        public BasketController(IBasketRepository repo, IMapper mapper, BasketCheckoutEventProducer eventProducer)
        {
            _repo = repo;
            _mapper = mapper;
            _eventProducer = eventProducer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            return Ok(await _repo.GetBasket(userName) ?? new BasketCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> Update(BasketCart basket)
        {
            return Ok(await _repo.UpdateBasket(basket));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(type: typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string userName)
        {
            return Ok(await _repo.DeleteBasket(userName));
        }

        //define checkout of basket and send event to eventBus if valid basket

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckOut basketCheckOut)
        {
            //get total price, remove basket, send checkout event
            var basket = await _repo.GetBasket(basketCheckOut.UserName);
            if (basket is null)
                return BadRequest();

            var deleteStatus = await _repo.DeleteBasket(basketCheckOut.UserName);
            if (!deleteStatus)
                return BadRequest();
         
            //mapping among basketCheckout and basketCheckoutEvent message
            var checkOutEventMessage = _mapper.Map<BasketCheckOutEvent>(basketCheckOut);
            checkOutEventMessage.RequestId = Guid.NewGuid();                //set ID of message in RabbitMQ queue
            checkOutEventMessage.TotalPrice = basket.TotalPrice;        //set total price to last total price form Db

            try
            {
                //publish event using event producer field
                _eventProducer.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, checkOutEventMessage);
            }
            catch (Exception)
            {
                throw;
            }
            return Accepted();
        }
    }
}
