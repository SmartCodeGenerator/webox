using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;

namespace Webox.API.Controllers
{
    [Produces("application/json")]
    [Route("api/storage-lots")]
    [ApiController]
    public class StorageLotsController : ControllerBase
    {
        private readonly IStorageLotService service;

        public StorageLotsController(IStorageLotService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "employee")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await service.GetStorageLots());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorageLot(string id)
        {
            try
            {
                var data = await service.GetStorageLot(id);
                return data != null ? Ok(data) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpGet("laptop-amount/{id}")]
        public async Task<IActionResult> GetLaptopAmount(string id)
        {
            try
            {
                return Ok(await service.GetLaptopStorageAmount(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "employee")]
        [HttpPost]
        public async Task<IActionResult> SaveStorageLot([FromBody] StorageLotDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.SaveStorageLot(data);
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
        public async Task<IActionResult> UpdateStorageLot(string id, [FromBody] StorageLotDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.UpdateStorageLot(id, data);
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
        [HttpPut("replenish/{id}")]
        public async Task<IActionResult> ReplenishStorageLot(string id, [FromBody] StorageReplenishmentDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.ReplenishStorageLot(id, data);
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
        public async Task<IActionResult> DeleteStorageLot(string id)
        {
            try
            {
                await service.DeleteStorageLot(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
