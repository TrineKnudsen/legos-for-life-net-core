using System.Linq;
using System.Reflection;
using InnoTech.LegosForLife.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace InnoTech.LegosForLife.WebApi.Test.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public void ProductController_IsOfTypeControllerBase()
        {
            var controller = new ProductController();
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }

        [Fact]
        public void ProductController_UsesApiControllerAttribute()
        {
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attribute = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            //Assert
            Assert.NotNull(attribute);

        }
    }
}