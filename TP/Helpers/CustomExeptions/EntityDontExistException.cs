using System;

namespace Helpers.CustomExeptions
{
    public class EntityDontExistException : Exception
    {
        public EntityDontExistException() : base("Entity dont exist")
        {
        }
    }
}
