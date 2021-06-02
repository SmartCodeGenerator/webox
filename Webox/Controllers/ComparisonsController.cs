using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Webox.BLL.Interfaces;

namespace Webox.API.Controllers
{
    [Produces("application/json")]
    [Route("api/comparisons")]
    [ApiController]
    public class ComparisonsController : ControllerBase
    {
        private readonly IComparisonService service;

        public ComparisonsController(IComparisonService service)
        {
            this.service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await service.GetComparisons(User.Identity.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComparison([FromBody] string laptopId)
        {
            try
            {
                await service.AddComparison(User.Identity.Name, laptopId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("check-presence")]
        public async Task<IActionResult> CheckPresence([FromBody] string laptopId)
        {
            try
            {
                return await service.CheckComparisonsPresence(User.Identity.Name, laptopId) ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveComparison(string id)
        {
            try
            {
                await service.RemoveComparison(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
