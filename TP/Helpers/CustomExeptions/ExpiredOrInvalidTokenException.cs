using System;

namespace Helpers.CustomExeptions
{
    public class ExpiredOrInvalidTokenException : Exception
    {
        public ExpiredOrInvalidTokenException() : base("ExpiredToken")
        {
        }
    }
}
