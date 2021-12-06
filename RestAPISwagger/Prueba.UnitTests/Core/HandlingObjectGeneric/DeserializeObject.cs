using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prueba.UnitTests.Core.Enveloped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.UnitTests.Core.HandlingObjectGeneric
{
    public class DeserializeObject : IDeserializeObject
    {
        public T Deserialize<T>(EnvelopedObject.Enveloped ObjDeserialize)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(ObjDeserialize.body, settings));
        }
    }
}
