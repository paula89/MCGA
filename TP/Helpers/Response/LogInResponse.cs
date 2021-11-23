using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers.Response
{
    public class LogInResponse
    {

        public string Token { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Username { get; set; }
        public List<Privilege> Privileges { get; set; }
       
        public LogInResponse(User user, RefreshToken token, List<Privilege> privileges)
        {
            Token = token.Token;
            TimeStamp = DateTime.Now.AddHours(2);
            Username = user.Username;
            Privileges = privileges;
        }

        public LogInResponse()
        {
        }
    }
}
