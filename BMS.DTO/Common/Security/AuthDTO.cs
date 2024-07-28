using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DTO.Common.Security
{
    public class AuthDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public int expires_in { get; set; }
        public DateTime ActionTime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public long UserId { get; set; }
    }
}
