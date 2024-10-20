using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SE.Models;
using SE.Models.DTOS;
using SE.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Services
{
    public class UserService: BaseService<UserInformation>, IUser
    {
        public UserService(PersonalFinanceTrackerContext context): base(context)
        {
        }

        public async Task<UserInformation> PostUser(UserInformation user)
        {
            var transaction = await BeginTransaction();
            try
            {
                _context.UserInformations.Add(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error saving user", ex);
            }
        }

        public async Task<UserInformation> getUserLogin(UserDTO userData)
        {
            var data = await _context.UserInformations.Where(x => x.Email == userData.LEmail && x.Password == userData.LPassword).FirstOrDefaultAsync();
            return data;
        }
    }
}
