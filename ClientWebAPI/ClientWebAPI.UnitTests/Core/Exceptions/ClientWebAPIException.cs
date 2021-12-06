using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.UnitTests.Core.Exceptions
{
    public class MantenedorMVCEntityException : Exception
    {
        public MantenedorMVCEntityException()
        { }

        public MantenedorMVCEntityException(string message)
        : base(message)
        { }

        public MantenedorMVCEntityException(string message, Exception innerException)
        : base(message, innerException)
        { }
    }

    public class MantenedorMVCEntityNotFoundException : Exception
    {
        public MantenedorMVCEntityNotFoundException()
        { }

        public MantenedorMVCEntityNotFoundException(string message)
        : base(message)
        { }

        public MantenedorMVCEntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
        { }
    }
}

