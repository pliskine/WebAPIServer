using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIServer.Services
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenValue { get; set; }
        public DateTime ValidBefore { get; internal set; }
    }
}
