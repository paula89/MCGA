using System;

namespace Helpers.Response
{
    public class PrivilegeResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public PrivilegeResponse(Privilege entity)
        {
            Id = entity.Id;
            Description = entity.Description;
        }
    }
}
