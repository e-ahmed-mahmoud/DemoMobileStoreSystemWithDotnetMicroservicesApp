using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQConnection
    {
        bool IsConnected { get; }

        bool TryConnected();

        IModel CreateModel();

        void Dispose();
    }
}
