using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Models.DTOS
{
    public class UserModel
    {
        public int userId { get; set; }
        public string? UserType { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
    }

    public class UserDTO 
    {
        public string? LEmail { get; set; }
        public string? LPassword { get; set; }
    }

    public class LoginResult
    {
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; }
        public UserInformation? User { get; set; }
        public string? Token { get; set; }
    }
}
