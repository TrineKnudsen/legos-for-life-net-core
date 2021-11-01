using Innotech.LegosForLife.DataAccess.Repositories;
using InnoTech.LegosForLife.Domain.IRepositories;
using Xunit;

namespace InnoTech.LegosForLife.DataAccess.Test
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void ProductRepository_IsIProductRepository()
        {
            var reposiroty = new ProductRepository();
            Assert.IsAssignableFrom<IProductRepository>(reposiroty);
        }
    }
}