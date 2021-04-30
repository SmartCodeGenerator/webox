using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Infrastructure;
using Webox.BLL.Infrastructure.QueryParams;
using Webox.BLL.Interfaces;

namespace Webox.API.Controllers
{
    [Produces("application/json")]
    [Route("api/laptops")]
    [ApiController]
    public class LaptopsController : ControllerBase
    {
        private readonly ILaptopService laptopService;

        public LaptopsController(ILaptopService laptopService)
        {
            this.laptopService = laptopService;
        }

        [Authorize(Roles = "employee")]
        [HttpPost]
        public async Task<IActionResult> CreateLaptop([FromBody] LaptopDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await laptopService.AddLaptop(data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Conflict(ModelState);
        }

        [Authorize(Roles = "employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaptop(string id)
        {
            try
            {
                await laptopService.DeleteLaptop(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLaptopById(string id)
        {
            var laptop = await laptopService.GetById(id);
            return laptop != null ? Ok(laptop) : NotFound();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string sortOrder, [FromQuery] LaptopParams queryParams, [FromQuery] int? pageIndex)
        {
            var sortOrderEnumValue = sortOrder switch
            {
                "rating_desc" => SortOrder.SortByRatingDescending,
                "price_asc" => SortOrder.SortByPriceAscending,
                "price_desc" => SortOrder.SortByPriceDescending,
                _ => SortOrder.SortByRatingDescending,
            };

            try
            {
                var data = await laptopService.Index(sortOrderEnumValue, queryParams, pageIndex ?? 1);

                var metadata = new
                {
                    data.Count,
                    PageSize = 14,
                    data.PageIndex,
                    data.TotalPages,
                    data.HasNextPage,
                    data.HasPreviousPage
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLaptop(string id, [FromBody] LaptopDTO data)
        {
            try
            {
                await laptopService.UpdateLaptop(id, data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
