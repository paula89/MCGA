using Helpers.Enum;
using System;
using System.Collections.Generic;

namespace Helpers
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MachineStatusEnum Status { get; set; }
        public bool IsIdle { get; set; }
        public bool Sensor { get; set; }
    }
}
