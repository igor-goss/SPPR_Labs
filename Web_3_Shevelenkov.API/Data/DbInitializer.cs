using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var context =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            var tankTypes = new List<TankType>
            {
                new TankType { Id = 0, Name = "Тяжелые танки", NormalizedName = "heavytanks"},
                new TankType { Id = 0, Name = "Средние танки", NormalizedName = "mediumtanks"},
                new TankType { Id = 0, Name = "Лёгкие танки", NormalizedName = "lighttanks"},
                new TankType { Id = 0, Name = "ПТ-САУ", NormalizedName = "ptsau"}
            };

            context.TankTypes.AddRange(tankTypes);


            var tanks = new List<Tank>()
            {
                new Tank { Id = 0, Name = "КВ-2", Description = "Бревномёт",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 1),
                    Price = 25000, Path = "/images/KV2.png" } ,
                new Tank { Id = 0, Name = "E 50 M", Description = "Имба СТ",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 2),
                    Price = 1000000, Path = @"/images/E50M.png" } ,
                new Tank { Id = 0, Name = "FV215b 183", Description = "Бабаха",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 4),
                    Price = 25000, Path = "/images/FV215b_183.png" } ,
                new Tank { Id = 0, Name = "ИС-7", Description = "За Сталина!",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 1),
                    Price = 25000, Path = "/images/IS7.png" } ,
                new Tank { Id = 0, Name = "ТВП 50/51", Description = "ТВП ГО ВЗВОД",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 2),
                    Price = 25000, Path = "/images/TVP.png" } ,
                new Tank { Id = 0, Name = "Яга Е100", Description = "Давай-давай, нападай, я пока борт подверну",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 4),
                    Price = 25000, Path = "/images/JagaE100.png" } ,
                new Tank { Id = 0, Name = "Grille 15", Description = "Снайпер",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 4),
                    Price = 25000, Path = "/images/Grille_15.png" } ,
                new Tank { Id = 0, Name = "Object 263", Description = "Уронщик",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 4),
                    Price = 25000, Path = "/images/Object263.png" } ,
                new Tank { Id = 0, Name = "Sheridan", Description = "Легкий и быстрый",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 3),
                    Price = 25000, Path = "/images/Sher.png" } ,
                new Tank { Id = 0, Name = "T-100 LT", Description = "Скорость и маневренность",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 3),
                    Price = 25000, Path = "/images/T100LT.png" } ,
                new Tank { Id = 0, Name = "Vickers Light", Description = "Британская легенда",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 3),
                    Price = 25000, Path = "/images/Vickers_Light.png" },
                new Tank { Id = 0, Name = "Concept 1B", Description = "Современный концепт",
                    Type = context.TankTypes.FirstOrDefault(t => t.Id == 1),
                    Price = 25000, Path = "/images/Concept.png" }
                };

            context.Tanks.AddRange(tanks);
            

            context.SaveChanges();


        }
    }
}
