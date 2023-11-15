using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Services.Interfaces;

namespace Web_3_Shevelenkov.Areas.Admin.Pages.Tanks
{
    public class DeleteModel : PageModel
    {
        private readonly ITankService _service;

        public DeleteModel(ITankService service)
        {
            _service = service;
        }


        [BindProperty]
        public Tank Tank { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tank = await _service.GetTankByIdAsync(id.Value);

            if (tank == null)
            {
                return NotFound();
            }
            else 
            {
                Tank = tank.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _service.DeleteTankAsync(id.Value);
            

            return RedirectToPage("./Index");
        }
    }
}
