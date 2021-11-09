﻿using InnoTech.LegosForLife.DataAccess.Entities;

namespace InnoTech.LegosForLife.DataAccess
{
    public class DbSeeder
    {
        private readonly MainDbContext _ctx;

        public DbSeeder(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            _ctx.Products.Add(new ProductEntity{Name = "Lego1"});
            _ctx.Products.Add(new ProductEntity{Name = "Lego2"});
            _ctx.Products.Add(new ProductEntity{Name = "Lego3"});
            _ctx.SaveChanges();
        }
    }
}