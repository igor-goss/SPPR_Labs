using Microsoft.AspNetCore.Mvc;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Services.Interfaces;

namespace Web_3_Shevelenkov.Services.Implementations
{
    public class MemoryTankService : ITankService
    {
        private readonly ITankTypeService _service;
        private List<Tank> _items;
        private readonly IConfiguration _config;

        public MemoryTankService([FromServices] IConfiguration configuration, ITankTypeService service)
        {
            _config = configuration;
            _service = service;
            _items = SetupData();
        }

        public Task<ResponseData<Tank>> CreateProductAsync(Tank tank, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTankAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Tank>> GetTankByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Tank>>> GetTankListAsync(string? categoryNormalizedName = null, int pageNo = 1)
        {
            var paginationInfo = GetPaginationInfo();
            var itemsPerPage = paginationInfo["itemsPerPage"];
            var totalPages = paginationInfo["totalPages"];


            ProductListModel<Tank> result;
            if (categoryNormalizedName == null)
            {
                result = new ProductListModel<Tank>
                {
                    Items = _items.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                    CurrentPage = pageNo,
                    TotalPages = totalPages
                };
            }
            else
            {
                var a = _service.GetTankTypeListAsync().Result.Data.ToList();
                var tankType = a.FirstOrDefault(c => c.NormalizedName == categoryNormalizedName);
                result = new ProductListModel<Tank>
                {
                    Items = _items.Where(t => t.Type.Id == tankType.Id).Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                    CurrentPage = pageNo,
                    TotalPages = totalPages
                };
            }

            ResponseData<ProductListModel<Tank>> response = new ResponseData<ProductListModel<Tank>>
            {
                Data = result,
                Success = true
            };

            return Task.FromResult(response);
        }

        public Dictionary<string, int> GetPaginationInfo()
        {
            var itemsPerPage = _config.GetValue<int>("ItemsPerPage");
            var totalPages = _items.Count() % itemsPerPage == 0 ? _items.Count() / itemsPerPage : _items.Count() / itemsPerPage + 1;
            return new Dictionary<string, int>() { { "itemsPerPage", itemsPerPage }, { "totalPages", totalPages } };
        }

        public Task UpdateTankAsync(int id, Tank tank, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private List<Tank> SetupData()
        {
            return new List<Tank>()
            {
                new Tank { Id = 1, Name = "КВ-2", Description = "Бревномёт",
                    Type = _service.GetTankTypeListAsync().Result.Data[0],
                    Price = 25000, Path = "/images/KV2.png" } ,
                new Tank { Id = 2, Name = "E 50 M", Description = "Имба СТ",
                    Type = _service.GetTankTypeListAsync().Result.Data[1],
                    Price = 1000000, Path = @"/images/E50M.png" } ,
                new Tank { Id = 3, Name = "FV215b 183", Description = "Бабаха",
                    Type = _service.GetTankTypeListAsync().Result.Data[3],
                    Price = 25000, Path = "/images/FV215b_183.png" } ,
                new Tank { Id = 4, Name = "ИС-7", Description = "За Сталина!",
                    Type = _service.GetTankTypeListAsync().Result.Data[0],
                    Price = 25000, Path = "/images/IS7.png" } ,
                new Tank { Id = 5, Name = "ТВП 50/51", Description = "ТВП ГО ВЗВОД",
                    Type = _service.GetTankTypeListAsync().Result.Data[1],
                    Price = 25000, Path = "/images/TVP.png" } ,
                new Tank { Id = 6, Name = "Яга Е100", Description = "Давай-давай, нападай, я пока борт подверну",
                    Type = _service.GetTankTypeListAsync().Result.Data[3],
                    Price = 25000, Path = "/images/JagaE100.png" } ,
                new Tank { Id = 7, Name = "Grille 15", Description = "Снайпер",
                    Type = _service.GetTankTypeListAsync().Result.Data[3],
                    Price = 25000, Path = "/images/Grille_15.png" } ,
                new Tank { Id = 8, Name = "Object 263", Description = "Уронщик",
                    Type = _service.GetTankTypeListAsync().Result.Data[3],
                    Price = 25000, Path = "/images/Object263.png" } ,
                new Tank { Id = 9, Name = "Sheridan", Description = "Легкий и быстрый",
                    Type = _service.GetTankTypeListAsync().Result.Data[2],
                    Price = 25000, Path = "/images/Sher.png" } ,
                new Tank { Id = 10, Name = "T-100 LT", Description = "Скорость и маневренность",
                    Type = _service.GetTankTypeListAsync().Result.Data[2],
                    Price = 25000, Path = "/images/T100LT.png" } ,
                new Tank { Id = 11, Name = "Vickers Light", Description = "Британская легенда",
                    Type = _service.GetTankTypeListAsync().Result.Data[2],
                    Price = 25000, Path = "/images/Vickers_Light.png" },
                new Tank { Id = 12, Name = "Concept 1B", Description = "Современный концепт",
                    Type = _service.GetTankTypeListAsync().Result.Data[0],
                    Price = 25000, Path = "/images/Concept.png" }
        };
        }
    }
}
