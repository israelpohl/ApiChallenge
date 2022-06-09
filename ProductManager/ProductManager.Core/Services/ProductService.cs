using System;
using System.Collections.Generic;
using System.Linq;
using ProductManager.Core.Data;
using ProductManager.Core.Domain;

namespace ProductManager.Core.Services
{
    public class ProductService
    {
        private ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public Product Get(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id");

            return _context.Products.Single(p=>p.Id == id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product Save(Product product)
        {
            if(productAlreadyExists(product))
            {
                throw new ArgumentException("Product can not be created, an identical one already exists");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        /// <summary>
        /// Checks if an identical product already exists
        /// </summary>
        /// <remarks>
        /// products are defined as identical if they have the same name and category
        /// </remarks>
        private bool productAlreadyExists(Product product)
        {
            return _context.Products.Any(p =>
                p.Name.Equals(product.Name, StringComparison.InvariantCultureIgnoreCase)
                && p.Category.Equals(product.Category, StringComparison.InvariantCultureIgnoreCase)
                );
        }

        public Product Update(int id, Product value)
        {
            var p = _context.Products.Single(p => p.Id == id);
            p.Name = value.Name;
            p.Category = value.Category;
            _context.SaveChanges();
            return p;
        }

        public void Delete(int id)
        {
            var p = _context.Products.Single(p => p.Id == id);
            _context.Products.Remove(p);
            _context.SaveChanges();
        }
    }
}