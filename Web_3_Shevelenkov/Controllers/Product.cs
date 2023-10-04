using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;
using Web_3_Shevelenkov.Services.TankService;

namespace Web_3_Shevelenkov.Controllers
{
    public class Product : Controller
    {
        private readonly ITankService _tankService;
        private readonly ITankTypeService _tankTypeService;

        public Product(ITankService tankService, ITankTypeService tankTypeService)
        {
            _tankService = tankService;
            _tankTypeService = tankTypeService;
        }   

        public IActionResult Index(string? categoryName, int pageNo = 1)
        {
           
            var productResponse =
                _tankService.GetTankListAsync(categoryName, pageNo);

            
            return View(new ProductListModel<Tank> 
            {
                Items = productResponse.Result.Data.Items,
                CurrentPage = pageNo,
                TotalPages = productResponse.Result.Data.TotalPages
            });
        }
    }
}
