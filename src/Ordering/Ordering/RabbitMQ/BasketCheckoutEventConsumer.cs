using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Ordering.API.RabbitMQ
{
    public class BasketCheckoutEventConsumer 
    {
        private readonly IRabbitMQConnection _rabbitMQConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepo;

        public BasketCheckoutEventConsumer(IRabbitMQConnection rabbitMQConnection, IMediator mediator, IMapper mapper, IOrderRepository orderRepo)
        {
            _rabbitMQConnection = rabbitMQConnection;
            _mediator = mediator;
            _mapper = mapper;
            _orderRepo = orderRepo;
        }

        public void Consume()
        {
            var channel = _rabbitMQConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;
            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutData = JsonConvert.DeserializeObject<BasketCheckOutEvent>(message);
                var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutData);

                var res = await _mediator.Send(command);

            }
        }

        public void DisConnect()
        {
            _rabbitMQConnection.Dispose();
        }
    }
}
