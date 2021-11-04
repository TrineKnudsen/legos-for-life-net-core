﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntityFrameworkCore.Testing.Moq;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.DataAccess;
using InnoTech.LegosForLife.DataAccess.Entities;
using InnoTech.LegosForLife.DataAccess.Repositories;
using InnoTech.LegosForLife.Domain.IRepositories;
using Xunit;

namespace InnoTech.LegosForLife.DataAccess.Test
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void ProductRepository_IsIProductRepository()
        {
            var fakeContext = Create.MockedDbContextFor<MainDbContext>(); 
            var repository = new ProductRepository(fakeContext);
            Assert.IsAssignableFrom<IProductRepository>(repository);
        }

        [Fact]
        public void ProductRepository_WithNullDBContext_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ProductRepository(null));
        }

        [Fact]
        public void ProductRepository_WithNullDBContext_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>
                (() => new ProductRepository(null));
            Assert.Equal("Product Repository must have a DB context", exception.Message);
        }

        [Fact]
        public void FindAll_GetAllProductsEntitiesInDBContext_AsAListOfProducts()
        {
            //Arrange
            var fakeContext = Create.MockedDbContextFor<MainDbContext>();
            var repository = new ProductRepository(fakeContext);
            var list = new List<ProductEntity>
            {
                new ProductEntity {Id = 1, Name = "Harry Potter"},
                new ProductEntity {Id = 2, Name = "Star Wars"},
                new ProductEntity {Id = 3, Name = "Friends"},
            };
            fakeContext.Set<ProductEntity>().AddRange(list);
            fakeContext.SaveChanges();
            
            var expectedList = list
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .ToList();
            
            //Act
            var actual = repository.FindAll();
            
            //Assert
            Assert.Equal(expectedList, actual, new Comparer());

        }
        
        public class Comparer: IEqualityComparer<Product>
        {
            public bool Equals(Product x, Product y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Name == y.Name;
            }

            public int GetHashCode(Product obj)
            {
                return HashCode.Combine(obj.Id, obj.Name);
            }
        }
    }
}