using ClientWebAPI.Api.Core.Enveloped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebAPI.Api.Core.HandlingObjectGeneric
{
    public interface IComposeObject
    {
        Task<EnvelopedObject.Enveloped> GetObject<T>(T Obj, string iniDate);
    }
}
