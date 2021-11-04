using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InnoTech.LegosForLife.WebApi.Test.Controllers
{
    public class ProductControllerTest
    {
        #region Controller Initialization
        
        [Fact]
        public void ProductController_IsOfTypeControllerBase()
        {
            var service = new Mock<IProductService>();
            var controller = new ProductController(service.Object);
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }

        [Fact]
        public void ProductController_WithNullProductService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
        }
        
        [Fact]
        public void ProductController_WithNullProductService_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => new ProductController(null));
            Assert.Equal("ProductService cannot be null", exception.Message);
        }

        [Fact]
        public void ProductController_UsesApiControllerAttribute()
        {
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attribute = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType()
                    .Name.Equals("ApiControllerAttribute"));
            //Assert
            Assert.NotNull(attribute);

        }

        [Fact]
        public void ProductController_UsesRouteAttribute()
        {
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attribute = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType()
                    .Name.Equals("RouteAttribute"));
            //Assert
            Assert.NotNull(attribute);
        }

        [Fact]
        public void ProductController_UserRouteAttribute_WithParamApiControllerNameRoute()
        {
             
            //Arrange
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attribute = typeInfo
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType()
                    .Name.Equals("RouteAttribute")) as RouteAttribute;
            //Assert
            Assert.Equal("api/[controller]", attribute.Template);
        }
        #endregion

        #region GetAll Method
        [Fact]
        public void ProductController_HasGetAllMethod()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void ProductController_HasGetAllMethod_IsPublic()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void ProductController_HasGetAllMethod_ReturnsListOfProductsInActionResult()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<List<Product>>).FullName, method.ReturnType.FullName);
        }

        [Fact]
        public void GetAll_WithNoParams_HasGetHttpAttribute()
        {
            var methodinfo = typeof(ProductController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "GetAll");
            var attribute = methodinfo.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attribute);
        }

        [Fact]
        public void GetAll_CallServicesGetProducts_Once()
        {
            //Arrange 
            var mockService = new Mock<IProductService>();
            var controller = new ProductController(mockService.Object);
            
            //Act
            controller.GetAll();
            
            //Assert
            mockService.Verify(s => s.GetProducts(), Times.Once);
        }
        #endregion

        #region Post

        

        #endregion
    }
}