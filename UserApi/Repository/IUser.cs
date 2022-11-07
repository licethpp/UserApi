using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;
using userDataApi.Models;

namespace UserApi.Repository
{
   public  interface IUser
    {

        Task<List<UserDetails>> FindAllAsync();

        Task<UserDetails> FindByIdAsync(int id);

        UserDetails FindlAsync(string UserName, string Password);

        Task InsertAsync(UserDetails user);

        Task DeleteAsync(UserDetails user);

        Task UpdateAsync(UserDetails user);

        void JuisteLogin(string emailadres);

        void FouteLogin(string emailadres);

        void UpdatePasswoord(string email, string Paswoord);
        string CreateToken(UserDetails user);
    }
}
