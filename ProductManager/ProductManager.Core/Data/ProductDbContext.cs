using System;
using Microsoft.EntityFrameworkCore;
using ProductManager.Core.Domain;

namespace ProductManager.Core.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}

