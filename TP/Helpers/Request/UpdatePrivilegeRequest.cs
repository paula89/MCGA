using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Request
{
    public class UpdatePrivilegeRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Privilege GetPrivilege() => new Privilege() { Id = Id, Description = Description };
    }
}
