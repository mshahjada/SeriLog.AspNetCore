using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLogger.Exceptions
{
    public class ObjectNotFoundException<T> : Exception where T : class
    {
        public ObjectNotFoundException() : base($"{typeof(T).Name} info doesn't found!") { }
        public ObjectNotFoundException(string message) : base(message) { }
        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }


}
