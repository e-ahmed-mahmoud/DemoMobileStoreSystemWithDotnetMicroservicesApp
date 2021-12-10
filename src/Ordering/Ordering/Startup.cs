using EventBusRabbitMQ;
using EventBusRabbitMQ.Producers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ordering.API.Extentions;
using Ordering.API.RabbitMQ;
using Ordering.Application.Handlers;
using Ordering.Core.Repositories;
using Ordering.Core.Repositories.Base;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Repositories.Base;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ordering
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<OrderingDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))
            , ServiceLifetime.Singleton);
            
            services.AddScoped (typeof(IRepository<>), typeof(Repository<>));         //requested by MediatoR
            services.AddScoped (typeof(IOrderRepository) , typeof(OrderRepository)); //requested by MediatoR

            services.AddTransient<IOrderRepository, OrderRepository>();         //requested by rabbitMQ 

            services.AddAutoMapper(typeof(Startup));        //register DI autoMaper 


            services.AddMediatR(typeof(CheckoutOrderCommandHandler).GetTypeInfo().Assembly);        //register DI Mediator on Application layer

            services.AddSingleton<BasketCheckoutEventConsumer>();           // register consumer

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))          //define user name and pass if find in json
                    factory.UserName = Configuration["EventBus:UserName"];
                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                    factory.Password = Configuration["EventBus:Password"];

                return new RabbitMQConnection(factory);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Order API" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitMQListener();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1"));
        }
    }
}
