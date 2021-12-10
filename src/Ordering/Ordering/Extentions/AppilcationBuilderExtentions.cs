using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ordering.API.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extentions
{
    public static class AppilcationBuilderExtentions
    {
        public static BasketCheckoutEventConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitMQListener(this IApplicationBuilder applicationBuilder)
        {
            //GetService<T> :function come from Microsoft.Extentions.DependencyInjection library
            Listener = applicationBuilder.ApplicationServices.GetService<BasketCheckoutEventConsumer>();  //get instance from consumer

            var life = applicationBuilder.ApplicationServices.GetService<IHostApplicationLifetime>();    //track App life cycle at start, stop,end

            life.ApplicationStarted.Register(OnStart);    //register specific method callback/refernece when app start

            life.ApplicationStopped.Register(OnStoped);   //execute specific method callback when app stop
            //life.ApplicationStopped.Register(() => Listener.DisConnect() );   //use lambda instead of callback

            return applicationBuilder;
        }

        private static void OnStart()
        {
            Listener.Consume();     //start consume and start listening when app start
        }

        private static void OnStoped()
        {
            Listener.DisConnect();  //close and end listening when app stoped
        }
    }
}
