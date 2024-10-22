using SE.Models;
using SE.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Inteface
{
    public interface IUser
    {
        Task<LoginResult> PostUser(UserInformation user);
        Task<LoginResult> getUserLogin(UserDTO userData);
    }
}
