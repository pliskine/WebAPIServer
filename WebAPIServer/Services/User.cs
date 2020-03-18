using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Services
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public bool Locked { get; set; }
    }
}
