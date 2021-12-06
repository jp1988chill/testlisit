using Prueba.UnitTests.Core.Enveloped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.UnitTests.Core.HandlingObjectGeneric
{
    public interface IComposeObject
    {
        Task<EnvelopedObject.Enveloped> GetObject<T>(T Obj, string iniDate);
    }
}
