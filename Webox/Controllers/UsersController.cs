using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Authorize]
        [HttpPut("account-information")]
        public async Task<IActionResult> EditAccountInformation([FromBody] EditUserInfoDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.EditUserInfo(User.Identity.Name, data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Conflict(ModelState);
        }

        [Authorize]
        [HttpPut("profile-image")]
        public async Task<IActionResult> UpdateProfileImage([FromBody] string profileImagePath)
        {
            try
            {
                await service.UpdateUserProfileImage(User.Identity.Name, profileImagePath);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("email")]
        public async Task<IActionResult> GetEmailUpdateVerificationCode()
        {
            try
            {
                var code = await service.GetEmailUpdateVerificationCode(User.Identity.Name);
                return code != null ? Ok(code) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("email")]
        public async Task<IActionResult> UpdateEmail([FromBody] string email)
        {
            try
            {
                await service.UpdateUserEmail(User.Identity.Name, email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("password/change")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.ChangeUserPassword(User.Identity.Name, data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Conflict(ModelState);
        }

        [Authorize]
        [HttpPut("password/restore")]
        public async Task<IActionResult> RestorePassword([FromBody] string email)
        {
            try
            {
                await service.RestoreUserPassword(User.Identity.Name, email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("password/reset")]
        public async Task<IActionResult> GetPasswordResetVerificationCode()
        {
            try
            {
                var code = await service.GetPasswordResetVerificationCode(User.Identity.Name);
                return code != null ? Ok(code) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("password/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.ResetUserPassword(User.Identity.Name, data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Conflict(ModelState);
        }
    }
}
