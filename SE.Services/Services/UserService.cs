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

        public async Task<LoginResult> PostUser(UserInformation user)
        {
            var transaction = await BeginTransaction();
            try
            {
                user.Password = EncodePasswordToBase64(user.Password);
                _context.UserInformations.Add(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                user.Password = DecodeFrom64(user.Password);
                return new LoginResult
                {
                    IsSuccess = true,
                    Message = "User created Succesfully",
                    User = user
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new LoginResult
                {
                    IsSuccess= false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<LoginResult> getUserLogin(UserDTO userData)
        {
            var data = await _context.UserInformations.Where(x => x.Email == userData.LEmail).FirstOrDefaultAsync();
            if (data != null && userData.LPassword == DecodeFrom64(data.Password))
            {
                data.Password = DecodeFrom64(data.Password);
                return new LoginResult
                {
                    IsSuccess = true,
                    User = data,
                    Message = "Login Succesfully"
                };
            }
            return new LoginResult
            {
                IsSuccess = false,
                Message = "Could not found the user"
            };
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
