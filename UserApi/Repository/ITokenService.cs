using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using userDataApi.Models;

namespace UserApi.Repository
{
   public interface ITokenService
    {
        string CreateToken(UserDetails user);

    }
}
