using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Domain.IRepositories;
using InnoTech.LegosForLife.Domain.Services;
using Moq;
using Xunit;

namespace InnoTech.LegosForLife.Domain.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public void ProductService_IsIProductService()
        {
            var mock = new Mock<IProductRepository>();
            
            var service = new ProductService(mock.Object);
            Assert.True(service is IProductService);
        }
    }
}