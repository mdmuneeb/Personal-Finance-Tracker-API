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
        public string UserType { get; set; }
        public string Token { get; set; }
    }
}
