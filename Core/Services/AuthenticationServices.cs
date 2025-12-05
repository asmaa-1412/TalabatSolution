using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstractionLayer;
using Shared.Dtos.IdentityDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServicesLayer
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager,
        IConfiguration _configration ) : IAuthenticationServices
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
                    Token= await CreateTokenAsync(user)
                };
            }
            else throw new UnauthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName?? registerDto.Email.Split("@")[0],
            };
            var res = await _userManager.CreateAsync(user,registerDto.Password);
            if (res.Succeeded) return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user)
            };
            else
            {
                var errors=res.Errors.Select(e => e.Description).ToList();
                throw new BadReguestException(errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                   new Claim(ClaimTypes.Email, user.Email!),
                   new Claim(ClaimTypes.Name, user.UserName!),
                   new Claim(ClaimTypes.NameIdentifier, user.Id!),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = _configration["JwtOptions:SecretKey"];
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configration["JwtOptions:Issuer"],
                audience: _configration["JwtOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds );

             return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
