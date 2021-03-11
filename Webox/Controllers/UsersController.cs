using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;

namespace Webox.API.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAccountService service;

        public UsersController(IUserAccountService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO data)
        {
            if (ModelState.IsValid)
            {
                var response = await service.Login(data);
                return response != null ? Ok(response) : BadRequest("Неправильна адреса електронної пошти або пароль");
            }
            return Conflict(ModelState);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO data)
        {
            if (ModelState.IsValid)
            {
                var response = await service.Register(data);
                return response != null ? Ok(response) : BadRequest("Неправильні дані для реєстрації");
            }
            return Conflict(ModelState);
        }

        [Authorize]
        [HttpGet("account-information")]
        public async Task<IActionResult> GetAccountInformation()
        {
            return Ok(await service.GetUserAccountInformation(User.Identity.Name));
        }
    }
}
