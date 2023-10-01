using Microsoft.AspNetCore.Mvc;

namespace Web_3_Shevelenkov.ViewComponents
{
    public class CartComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
