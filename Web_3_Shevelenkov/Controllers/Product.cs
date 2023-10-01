using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(string? categoryName)
        {
            var productResponse =
                _tankService.GetTankListAsync(categoryName);

            return View(productResponse.Result.Data.Items);
        }
    }
}
