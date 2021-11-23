using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Request
{
    public class CreatePrivilegeRequest
    {
        public string Description { get; set; }

        public Privilege GetPrivilege() => new Privilege() { Description = Description };

    }
}
