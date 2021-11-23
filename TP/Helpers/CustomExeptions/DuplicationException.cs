using System;

namespace Helpers.CustomExeptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException() : base("Entity important value is duplicated")
        {            
        }
    }
}
