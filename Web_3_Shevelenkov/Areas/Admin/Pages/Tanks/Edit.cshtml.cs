using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Services.Interfaces;

namespace Web_3_Shevelenkov.Areas.Admin.Pages.Tanks
{
    public class EditModel : PageModel
    {
        private readonly ITankService _service;

        public EditModel(ITankService service)
        {
            _service = service;
        }

        [BindProperty]
        public Tank Tank { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tank =  await _service.GetTankByIdAsync(id.Value);
            if (tank == null)
            {
                return NotFound();
            }
            Tank = tank.Data;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.UpdateTankAsync(Tank.Id, Tank, Image);

            return RedirectToPage("./Index");
        }

    }
}
