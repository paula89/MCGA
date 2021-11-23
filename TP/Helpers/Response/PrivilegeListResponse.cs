using System;

namespace Helpers.Response
{
    public class PrivilegeListResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public PrivilegeListResponse(Privilege entity)
        {
            Id = entity.Id;
            Description = entity.Description;
        }
    }
}
