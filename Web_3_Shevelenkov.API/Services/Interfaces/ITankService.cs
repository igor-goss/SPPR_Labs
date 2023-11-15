using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.API.Services.Interfaces
{
    public interface ITankService
    {
        public Task<ResponseData<ProductListModel<Tank>>> GetTankListAsync(string? categoryNormalizedName = null, int pageNo = 1, int pageSize = 3);
        public Task<ResponseData<ProductListModel<Tank>>> GetTankByIdAsync(int id);
        public Task UpdateTankAsync(int id, Tank tank);
        public Task DeleteTankAsync(int id);
        public Task<ResponseData<ProductListModel<Tank>>> CreateTankAsync(Tank tank, IFormFile? formFile);
        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);

    }
}
