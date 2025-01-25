using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Controllers;

public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper) : BaseApiController
{

    [HttpPost("register")] // account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        var user = mapper.Map<AppUser>(registerDto);

        user.UserName=registerDto.Username.ToLower();
        #region Not Using anymore after using Identity
             //using var hmac = new HMACSHA512();
            // user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            // user.PasswordSalt=hmac.Key;
    
            //context.Users.Add(user);
            //await context.SaveChangesAsync(); 
        #endregion  

        var result = await userManager.CreateAsync(user,registerDto.Password);

        if(!result.Succeeded) return BadRequest(result.Errors);
        
        return new UserDto
        {
         Username = user.UserName,
         Token= await tokenService.CreateToken(user),
         KnownAs=user.KnownAs,
         Gender=user.Gender
        }; 
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.Users
        .Include(p=>p.Photos)
        .FirstOrDefaultAsync(x =>
        x.NormalizedUserName == loginDto.Username.ToUpper());

        #region Old code before using Identity
            // if(user == null) return Unauthorized("Invalid username");
    
            // using var hmac = new HMACSHA512(user.PasswordSalt);
    
            // var computedHast = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
    
            // for (int i = 0; i< computedHast.Length; i++)
            // {
            //     if(computedHast[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
    
            // }
        #endregion

        if(user == null || user.UserName==null) return Unauthorized("Invalid username");

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if(!result) return Unauthorized();

        return new UserDto{
            Username = user.UserName,
            KnownAs=user.KnownAs,
            Token = await tokenService.CreateToken(user),
            Gender=user.Gender,
            PhotoUrl = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
        };
    }

    public async Task<bool> UserExists(string username)
    {
        return await userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper());
    }
}
