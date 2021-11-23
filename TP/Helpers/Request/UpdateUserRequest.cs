using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<Privilege> Privileges { get; set; }

        public User GetUser() => new User() { Id=Id, Username = Username, PasswordHash = Password, Salt = Salt };
    }
}
