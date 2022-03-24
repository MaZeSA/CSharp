using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var Categorys = new List<Category>
            {
                   new Category { Id=1, Name = "Комп'ютери та ноутбуки" },
                    new Category {Id=2, Name = "Смартфони" },
                    new Category { Id=3, Name = "Побутова техніка" },
                    new Category { Id=4, Name = "Дача, сад, город" },
                    new Category { Id=5, Name = "Спорт і захоплення" },
                    new Category { Id=6, Name = "Офіс, школа, книги" }
            };

            modelBuilder.Entity<Category>().HasData(Categorys);


            modelBuilder.Entity<Product>().HasData(
                new Product {Id=1, Name = "ПК Х123434", CategoryId=1, Description="test", ShortDesc="figna", Price = 436765, Image= "https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/noutbuki.png" },
                new Product { Id = 2, Name = "iHunt Titan P13000 PRO 2021", CategoryId = 2, Description = "Ми представляємо вам найпотужнішу, саму оснащену, ударотривкий та найефективнішу версію смартфона 2021 року від румунської компанії iHunt .", ShortDesc = "figna", Price = 13940, Image = "https://content2.rozetka.com.ua/goods/images/big/186736679.jpg" },
                new Product { Id = 3, Name = "BEKO CNA295K20XP", CategoryId = 3, Description = "Холодильники з системою NeoFrost ", ShortDesc = "figna", Price = 10999, Image = "https://content1.rozetka.com.ua/goods/images/big/175324656.jpg" },
                new Product { Id = 4, Name = "Bosch UniversalChain 40", CategoryId = 4, Description = "Ланцюгова пила Bosch UniversalChain ", ShortDesc = "figna", Price = 3958, Image = "https://content.rozetka.com.ua/goods/images/big/144435754.jpg" },
                new Product { Id = 5, Name = "Champion Spark 29 19.5 Black-neon yellow-white", CategoryId = 5, Description = "Велосипед Champion Spark 29 ", ShortDesc = "figna", Price = 5460, Image = "https://content2.rozetka.com.ua/goods/images/big/44538732.jpg" },
                new Product { Id = 6, Name = "Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла", CategoryId = 6, Description = "ВНабір паперу офісного Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла ", ShortDesc = "figna", Price = 1199, Image = "https://content2.rozetka.com.ua/goods/images/big/252699020.jpg" }

            );
        }
    }

}
