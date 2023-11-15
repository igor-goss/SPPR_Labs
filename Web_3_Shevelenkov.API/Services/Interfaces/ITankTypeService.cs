using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.API.Services.Interfaces
{
    public interface ITankTypeService
    {
        public Task<ResponseData<List<TankType>>> GetTankTypeListAsync();
    }
}
