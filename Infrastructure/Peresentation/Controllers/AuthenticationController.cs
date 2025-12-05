using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServicesAbstractionLayer;
using Shared.Dtos.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController(IServiceManger _serviceManger): ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManger.AuthenticationServices.LoginAsync(loginDto);
            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManger.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }
    }
}
