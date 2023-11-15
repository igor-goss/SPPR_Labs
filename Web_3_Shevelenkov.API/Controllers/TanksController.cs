using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.API.Services.Interfaces;
using Web_3_Shevelenkov.Domain.Entities;
using Web_3_Shevelenkov.Domain.Models;

namespace Web_3_Shevelenkov.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TanksController : ControllerBase
    {
        private readonly ITankService _tankService;
        private readonly string _imagesPath;
        private readonly string _appUri;

        public TanksController(ITankService service,
                IWebHostEnvironment env,
                IConfiguration configuration)
        {
            _tankService = service;
            _imagesPath = Path.Combine(env.WebRootPath, "Images");
            _appUri = configuration.GetSection("appUri").Value;
        }

        // GET: api/Tanks
        [HttpGet]
        [Route("page{pageNo:int}pageSize{pageSize:int}")]
        public async Task<ActionResult<IEnumerable<Tank>>> GetTanks(string? tankType, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _tankService.GetTankListAsync(tankType, pageNo, pageSize));
        }

        // GET: api/Tanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tank>> GetTank(int id)
        {
            return Ok(await _tankService.GetTankByIdAsync(id));
        }

        // PUT: api/Tanks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTank(int id, Tank tank)
        {
            await _tankService.UpdateTankAsync(id, tank);
            return NoContent();
        }

        // POST: api/Tanks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tank>> PostTank(Tank tank)
        {
            return Ok(/*await _tankService.CreateTankAsync(tank, formFile)*/);
        }

        // DELETE: api/Tanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTank(int id)
        {
            await _tankService.DeleteTankAsync(id);

            return NoContent();
        }

        // POST: api/Tanks/5
        //[HttpPost("{id}")]
        //public async Task<ActionResult<ResponseData<string>>> PostImage(int id, IFormFile formFile)
        //{
        //    var response = await _tankService.SaveImageAsync(id, formFile);
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }
        //    return NotFound(response);
        //}
    }
}
