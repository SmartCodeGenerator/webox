using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;

namespace Webox.API.Controllers
{
    [Produces("application/json")]
    [Route("api/deliverers")]
    [ApiController]
    public class DeliverersController : ControllerBase
    {
        private readonly IDelivererService service;

        public DeliverersController(IDelivererService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "employee")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await service.GetDeliverers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliverer(string id)
        {
            try
            {
                var model = await service.GetDeliverer(id);
                return model != null ? Ok(model) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpPost]
        public async Task<IActionResult> SaveDeliverer([FromBody] DelivererDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.SaveDeliverer(data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Conflict(ModelState);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliverer(string id, [FromBody] DelivererDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.UpdateDeliverer(id, data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Conflict(ModelState);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliverer(string id)
        {
            try
            {
                await service.DeleteDeliverer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
