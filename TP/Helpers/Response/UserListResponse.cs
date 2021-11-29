using System;

namespace Helpers.Response
{
    public class UserListResponse
    {
        private Package x;

        public int Id { get; set; }
        public string Username { get; set; }

        public UserListResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }

        public UserListResponse(Package x)
        {
            this.x = x;
        }
    }
}
