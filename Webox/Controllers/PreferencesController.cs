using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.BLL.Interfaces;

namespace Webox.API.Controllers
{
    [Produces("application/json")]
    [Route("api/preferences")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly IPreferenceService service;

        public PreferencesController(IPreferenceService service)
        {
            this.service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await service.GetPreferences(User.Identity.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPreference([FromBody] string laptopId)
        {
            try
            {
                await service.AddPreference(User.Identity.Name, laptopId);
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
                return await service.CheckPreferencesPresence(User.Identity.Name, laptopId) ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePreference(string id)
        {
            try
            {
                await service.RemovePreference(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
