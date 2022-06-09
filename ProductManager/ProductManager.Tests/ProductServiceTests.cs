using System;
using ProductManager.Core.Data;
using ProductManager.Core.Domain;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductManager.Core.Services
{
    public class ProductServiceTests
    {
        private readonly ProductDbContext _context;
        private readonly ProductService _productService;
        private readonly Product _xylophone;

        public ProductServiceTests()
        {
            var _contextOptions = new DbContextOptionsBuilder<ProductDbContext>()
                    .UseInMemoryDatabase("ProductManagerTest")
                    .Options;
            _context = new ProductDbContext(_contextOptions);

            _productService = new ProductService(_context);
            
            _xylophone = new Product
            {
                Name = "Xylophone",
                Category = "Instrument"
            };
        }

        [Fact]
        public void ShouldThrowExceptionIfIdLessThanZero()
        {

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _productService.Get(-1));

            Assert.Equal("id", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveProduct()
        {
            clearTestDb();

            Product savedProduct = null;

            savedProduct = _productService.Save(_xylophone);

            Assert.NotNull(savedProduct);
            Assert.True(savedProduct.Id > 0);
        }

        [Fact]
        public void ShouldNotSaveDuplicateProduct()
        {
            clearTestDb();
            _productService.Save(_xylophone);
            Product p2 = new Product
            {
                Name = _xylophone.Name,
                Category = _xylophone.Category
            };

            Assert.Throws<ArgumentException>(() => _productService.Save(p2));

            Assert.Equal(1, _context.Products.Count());
        }

        [Fact]
        public void UpdatedProductShouldHaveNewValues()
        {
            clearTestDb();
            _productService.Save(_xylophone);
            Product p2 = new Product
            {
                Name = _xylophone.Name,
                Category = "Percussion"
            };
            int idBeforeUpdate = _xylophone.Id;

            _productService.Update(_xylophone.Id, p2);

            Assert.Equal("Percussion", _xylophone.Category);
            Assert.Equal(idBeforeUpdate, _xylophone.Id);
                        
        }

        private void clearTestDb()
        {
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
        }
    }
}

