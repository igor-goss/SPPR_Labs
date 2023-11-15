using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Services.Interfaces;

namespace Web_3_Shevelenkov.Areas.Admin.Pages.Tanks
{
    public class CreateModel : PageModel
    {
        private readonly ITankService _service;

        public CreateModel(ITankService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tank Tank { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.CreateProductAsync(Tank, Image);

            return RedirectToPage("./Index");
        }
    }
}
