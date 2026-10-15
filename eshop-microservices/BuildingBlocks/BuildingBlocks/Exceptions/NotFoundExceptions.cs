using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class NotFoundExceptions : Exception
    {
        public NotFoundExceptions(string message): base(message) 
        { 
        }
        public NotFoundExceptions(string name, object key) : base($"Entity \"{name}\"({key}) was not found.") 
        { 
        }
    }   
}
