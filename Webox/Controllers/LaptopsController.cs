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
            try
            {
                var laptop = await laptopService.GetById(id);
                return laptop != null ? Ok(laptop) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [Authorize]
        [HttpGet("names")]
        public async Task<IActionResult> GetLaptopNames([FromQuery] string name)
        {
            try
            {
                return Ok(await laptopService.GetModelNameList(name ?? ""));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("manufacturers")]
        public async Task<IActionResult> GetManufacturers()
        {
            try
            {
                return Ok(await laptopService.GetManufacturers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("processors")]
        public async Task<IActionResult> GetProcessors()
        {
            try
            {
                return Ok(await laptopService.GetProcessors());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("graphics")]
        public async Task<IActionResult> GetGraphics()
        {
            try
            {
                return Ok(await laptopService.GetGraphics());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ram")]
        public async Task<IActionResult> GetRAM()
        {
            try
            {
                return Ok(await laptopService.GetRAM());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ssd")]
        public async Task<IActionResult> GetSSD()
        {
            try
            {
                return Ok(await laptopService.GetSSD());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("screens")]
        public async Task<IActionResult> GetScreens()
        {
            try
            {
                return Ok(await laptopService.GetScreens());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("OS")]
        public async Task<IActionResult> GetOS()
        {
            try
            {
                return Ok(await laptopService.GetOS());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("min-weight")]
        public async Task<IActionResult> GetMinWeight()
        {
            try
            {
                return Ok(await laptopService.GetMinWeight());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("max-weight")]
        public async Task<IActionResult> GetMaxWeight()
        {
            try
            {
                return Ok(await laptopService.GetMaxWeight());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("min-price")]
        public async Task<IActionResult> GetMinPrice()
        {
            try
            {
                return Ok(await laptopService.GetMinPrice());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("max-price")]
        public async Task<IActionResult> GetMaxPrice()
        {
            try
            {
                return Ok(await laptopService.GetMaxPrice());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
