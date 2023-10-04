using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_3_Shevelenkov.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            ViewData["name"] = "Лабораторная работы №2";

            var listOptions = new List<ListDemo>()
            {
                new ListDemo(1, "Item 1"),
                new ListDemo(2, "Item 2"),
                new ListDemo(3, "Item 3")
            };

            SelectList options = new SelectList(listOptions, "Id", "Name");
            ViewBag.SelectOptions = options;

            return View();
        }

        public class ListDemo
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public ListDemo(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}
