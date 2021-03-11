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
                return response != null ? Ok(response) : BadRequest("Неправильний логін або пароль");
            }
            else
            {
                return Conflict(ModelState);
            }
        }
    }
}
