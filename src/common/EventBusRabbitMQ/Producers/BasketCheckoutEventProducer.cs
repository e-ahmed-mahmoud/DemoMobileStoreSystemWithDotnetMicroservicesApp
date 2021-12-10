using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Producers
{
    public class BasketCheckoutEventProducer
    {
        private readonly IRabbitMQConnection _connection;

        public BasketCheckoutEventProducer(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public void PublishBasketCheckout(string queueName, BasketCheckOutEvent basketCheckOutEvent)
        {
            using (var channel = _connection.CreateModel())         //define Model and open 
            {
                //define queue with queueName, durable: save Db or not, exclusive: delete after end, autodelete: manual, no argues
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(basketCheckOutEvent);     //define message as JSON
                var body = Encoding.UTF8.GetBytes(message);                         //convert message into bytes and encapsulate in body

                IBasicProperties properties = channel.CreateBasicProperties();      //use intial properties
                properties.Persistent = true;                                       // set Delivery Mode to true
                properties.DeliveryMode = 2;                                        // set it to 2 so enable delivery mode

                channel.ConfirmSelect();                       //enable publish acknowledgment 
                    //basic function for publish event for define event: using basic properties and send encoding body
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body: body);    
                channel.WaitForConfirmsOrDie();             //wait confirmation back

                //adding any addtional behavior at get acknowledgement
                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("sending data rabbitMQ");
                };
                channel.ConfirmSelect();        //get final acknoledgment

            }
        }


    }
}
