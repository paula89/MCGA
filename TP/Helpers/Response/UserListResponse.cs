using System;

namespace Helpers.Response
{
    public class UserListResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public UserListResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }
    }
}
