using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;

namespace Web_3_Shevelenkov.Services.TankService
{
    public class MemoryTankTypeService : ITankTypeService
    {
        public Task<ResponseData<List<TankType>>> GetTankTypeListAsync()
        {
            var tanks = new List<TankType>
            {
                new TankType { Id = 1, Name = "Тяжелые танки", NormalizedName = "heavytanks"},
                new TankType { Id = 2, Name = "Средние танки", NormalizedName = "mediumtanks"},
                new TankType { Id = 3, Name = "Лёгкие танки", NormalizedName = "lighttanks"},
                new TankType { Id = 4, Name = "ПТ-САУ", NormalizedName = "ptsau"}
            };
            var result = new ResponseData<List<TankType>>();
            result.Data = tanks;
            return Task.FromResult(result);
        }
    }
}
