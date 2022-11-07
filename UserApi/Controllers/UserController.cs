using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Repository;
using userDataApi.Models;

namespace UserApi.Controllers
{

  
    [Route("api/[controller]")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser repository;
        public UserController(IUser repository) =>
        this.repository = repository;

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult> FindAll() => base.Ok(await repository.FindAllAsync());


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            var u = repository.FindlAsync(user.UserName, user.Password);

            if ( u != null)//  user.UserName == "johndoe" && user.Password == "def@123")
              

            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44379",
                    audience: "https://localhost:44379",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Post(UserDetails user)
        {
            if (this.ModelState.IsValid)
            {
                await repository.InsertAsync(user);
                return base.CreatedAtAction(nameof(FindById), new
                {
                    id = user.UserId
                }, null);

            }
            return base.BadRequest(this.ModelState);


        }


        [HttpGet("id"), Authorize]
        public async Task<ActionResult> FindById(int id)
        {
            var user = await repository.FindByIdAsync(id);
            return user == null ? base.NotFound() : base.Ok(user);
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult>UserId(int id)
        //{
        //    var user = await repository.FindByIdAsync(id);
        //    return user == null ? base.NotFound() : base.Ok(user);
        //}


    





    }
  }

