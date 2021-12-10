using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure
{
    public class OrderingDbContext : DbContext 
    {
        public OrderingDbContext( DbContextOptions<OrderingDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
