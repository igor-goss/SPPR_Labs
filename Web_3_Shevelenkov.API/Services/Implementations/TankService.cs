using Web_3_Shevelenkov.API.Data;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Web_3_Shevelenkov.API.Services.Implementations
{
    public class TankService : ITankService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private IHttpContextAccessor _httpContextAccessor;

        private readonly int _maxPageSize = 20;

        public TankService(AppDbContext context, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ResponseData<ProductListModel<Tank>>> CreateTankAsync(Tank tank, IFormFile? Image)
        {
            _context.Tanks.Add(tank);
            await SaveImageAsync(tank.Id, Image);
            await _context.SaveChangesAsync();
            return new ResponseData<ProductListModel<Tank>>()
            {
                Data = new ProductListModel<Tank> { Items = new List<Tank>() { tank } }
            };
        }

        public async Task<ResponseData<ProductListModel<Tank>>> GetTankByIdAsync(int id)
        {
            var tank = await _context.Tanks.FindAsync(id);
            var dataList = new ProductListModel<Tank>();
            dataList.Items = new List<Tank>() { tank };
            var response = new ResponseData<ProductListModel<Tank>>()
            {
                Data = dataList
            };

            return response;
        }

        public async Task<ResponseData<ProductListModel<Tank>>> GetTankListAsync(string? categoryNormalizedName = null, int page = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
                pageSize = _maxPageSize;

            int totalPages = (int)Math.Ceiling(_context.Tanks.Count() / (double)pageSize);

            ProductListModel<Tank> result;
            if (categoryNormalizedName == null)
            {
                result = new ProductListModel<Tank>
                {
                    Items = _context.Tanks.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                    CurrentPage = page,
                    TotalPages = totalPages
                };
            }
            else
            {
                var tankType = _context.TankTypes.ToList()
                    .FirstOrDefault(c => c.NormalizedName == categoryNormalizedName);
                result = new ProductListModel<Tank>
                {
                    Items = _context.Tanks.Where(t => t.TypeId == tankType.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                    CurrentPage = page,
                    TotalPages = totalPages
                };
            }

            ResponseData<ProductListModel<Tank>> response = new ResponseData<ProductListModel<Tank>>
            {
                Data = result,
                Success = true
            };

            return response;
        }


        public async Task UpdateTankAsync(int id, Tank tank)
        {
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTankAsync(int id)
        {
            var tank = await _context.Tanks.FindAsync(id);
            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();
        }


        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            var responseData = new ResponseData<string>();
            var tank = await _context.Tanks.FindAsync(id);
            if (tank == null)
            {
                responseData.Success = false;
                responseData.ErrorMessage = "No item found";
                return responseData;
            }
            var host = "https://" + _httpContextAccessor.HttpContext.Request.Host;
            var imageFolder = Path.Combine(_env.WebRootPath, "Images");
            if (formFile != null)
            {
                // Удалить предыдущее изображение
                if (!String.IsNullOrEmpty(tank.Path))
                {
                    var prevImage = Path.GetFileName(tank.Path);
                }
                // Создать имя файла
                var ext = Path.GetExtension(formFile.FileName);
                var fName = Path.ChangeExtension(Path.GetRandomFileName(), ext);
                // Сохранить файл

                var filePath = Path.Combine(imageFolder, fName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // Указать имя файла в объекте
                tank.Path = $"{host}/Images/{fName}";
                await _context.SaveChangesAsync();
            }
            responseData.Data = tank.Path;
            return responseData;
        }

    }
}
