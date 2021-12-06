using Prueba.UnitTests.Core.Enveloped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.UnitTests.Core.HandlingObjectGeneric
{
    public interface IDeserializeObject
    {
        T Deserialize<T>(EnvelopedObject.Enveloped ObjDeserialize);
    }
}
