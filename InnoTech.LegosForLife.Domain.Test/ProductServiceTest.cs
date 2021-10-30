using System.Collections.Generic;
using System.IO;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.Domain.IRepositories;
using InnoTech.LegosForLife.Domain.Services;
using Moq;
using Xunit;

namespace InnoTech.LegosForLife.Domain.Test
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _mock;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _mock = new Mock<IProductRepository>();
            _service = new ProductService(_mock.Object);
        }
        [Fact]
        public void ProductService_IsIProductService()
        {
            Assert.True(_service is IProductService);
        }
        
        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsInvalidDataException(){
            Assert.Throws<InvalidDataException>
                (() => new ProductService(null));
             }
        
        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsExceptionWithMessage(){
            var exception = Assert.Throws<InvalidDataException>
                (() => new ProductService(null));
            Assert.Equal("ProductRepo cannot be null", exception.Message);
        }
        [Fact]
        public void GetProducts_CallsProductRepositoriesFindAll_ExactlyOnce()
        {
            _service.GetProducts();
            _mock.Verify(r => r.FindAll(), Times.Once);
        }

        [Fact]
        public void GetProducts_NoFilter_ReturnsListOfAllProducts()
        {
            var expected = new List<Product>
            {
                new Product {Id = 1, Name = "Brie"},
                new Product {Id = 2, Name = "Cheddar"}
            };
            _mock.Setup(r => r.FindAll())
                .Returns(expected);
            var actual = _service.GetProducts();
            Assert.Equal(expected, actual);
        }
        
    }
    
    
}