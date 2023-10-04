using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.Services.TankService
{
    public interface ITankTypeService
    {
        public Task<ResponseData<List<TankType>>> GetTankTypeListAsync();
    }
}
