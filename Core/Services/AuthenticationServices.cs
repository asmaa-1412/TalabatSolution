using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using ServicesAbstractionLayer;
using Shared.Dtos.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServicesLayer
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager) : IAuthenticationServices
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UserNotFoundException(loginDto.Email);
            var ispassValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (ispassValid)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token= "To Do"
                };
            }
            else throw new UnauthorizedException();
        }

        public async Task<UserDto> RegisterDto(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            var res = await _userManager.CreateAsync(user,registerDto.Password);
            if (res.Succeeded) return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Token To Do"
            };
            else
            {
                var errors=res.Errors.Select(e => e.Description).ToList();
                throw new BadReguestException(errors);
            }
        }
    }
}
