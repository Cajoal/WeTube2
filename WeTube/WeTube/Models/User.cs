using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTube.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string AvatarName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
