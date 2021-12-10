using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        public bool IsConnected => _connection != null && _connection.IsOpen && !_dispose;

        private readonly IConnectionFactory _connectionFactory;     //main class for RabbitMQ clinet
        private IConnection _connection;                            //open connection with rabbitmq
        private bool _dispose;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            if (!IsConnected)
                TryConnected();
        }

        public bool TryConnected()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(2000);
                _connection = _connectionFactory.CreateConnection();
            }
            return IsConnected;
        }

        public IModel CreateModel()         //Imodel is repesent queue that will define to hold messages
        {
            return IsConnected ? _connection.CreateModel() : throw new InvalidOperationException("No rabbit connection");
        }

        public void Dispose()
        {
            if (_dispose)
                return;
            try
            {
                _connection.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
