using System;
using System.Collections.Generic;

namespace Helpers.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public List<Privilege> Privileges { get; set; }

        public UserResponse(User user, List<Privilege> privileges)
        {
            Id = user.Id;
            Username = user.Username;
            Salt = user.Salt;
            Privileges = privileges;
        }
    }
}
