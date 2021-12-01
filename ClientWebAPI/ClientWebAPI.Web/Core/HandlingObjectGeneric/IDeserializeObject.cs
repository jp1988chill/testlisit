using ClientWebAPI.Api.Core.Enveloped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebAPI.Api.Core.HandlingObjectGeneric
{
    public interface IDeserializeObject
    {
        T Deserialize<T>(EnvelopedObject.Enveloped ObjDeserialize);
    }
}
