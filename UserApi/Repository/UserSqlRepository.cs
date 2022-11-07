using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApi.Models;
using userDataApi.Models;

namespace UserApi.Repository
{
    public class UserSqlRepository : IUser

    {
        private readonly UserContext context;
        private readonly SymmetricSecurityKey _key;
        public UserSqlRepository(UserContext context, IConfiguration config) =>
        this.context = context;

        public UserSqlRepository(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }


        public string CreateToken(UserDetails user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
           };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
            };



            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
   

        public async Task DeleteAsync(UserDetails user)
        {

            try
            {
                var u = context.UserDetails.First();
                context.Remove(u);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var t = ex;
            }

        }

        public async Task<List<UserDetails>> FindAllAsync() =>await context.UserDetails.AsNoTracking().ToListAsync();

        public async Task<UserDetails> FindByIdAsync(int id) => await context.UserDetails.FindAsync(id);
            
      


        public UserDetails FindlAsync(string UserName, string Password)
        {
            return (UserDetails)context.UserDetails.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
        }

        public void FouteLogin(string emailadres)
        {
            UserDetails p = context.UserDetails.Where(g => g.Email == emailadres).FirstOrDefault();
            if (p != null)
            {
                context.SaveChanges();
            }
        }

        public async Task InsertAsync(UserDetails user)
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public void JuisteLogin(string emailadres)
        {
            UserDetails p = context.UserDetails.Where(g => g.Email == emailadres).FirstOrDefault();
            if (p != null)
            {
                context.SaveChanges();
            }
        }

        public async Task UpdateAsync(UserDetails user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }



        public void UpdatePasswoord(string email, string Paswoord)
        {
            UserDetails b = context.UserDetails.Where(x => x.Email == email).FirstOrDefault();
            b.Password = Paswoord;
            context.SaveChanges();
        }

      

    }
}
