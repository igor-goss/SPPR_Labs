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
    public class IndexModel : PageModel
    {
        private readonly ITankService _service;

        public IndexModel(ITankService service)
        {
            _service = service;
        }

        public IList<Tank> Tank { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var tmp = await _service.GetTankListAsync();
            Tank =  tmp.Data.Items;
        }
    }
}
