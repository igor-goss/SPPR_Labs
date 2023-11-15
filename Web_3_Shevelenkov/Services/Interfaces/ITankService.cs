using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Domain.Entities;

namespace Web_3_Shevelenkov.Services.Interfaces
{
    public interface ITankService
    {
        public Task<ResponseData<ProductListModel<Tank>>> GetTankListAsync(string? categoryNormalizedName = null, int pageNo = 1);
        public Task<ResponseData<Tank>> GetTankByIdAsync(int id);
        public Task UpdateTankAsync(int id, Tank tank, IFormFile? formFile);
        public Task DeleteTankAsync(int id);
        public Task<ResponseData<Tank>> CreateProductAsync(Tank tank, IFormFile? formFile);
        public Task SaveImageAsync(int id, IFormFile image);

    }
}
