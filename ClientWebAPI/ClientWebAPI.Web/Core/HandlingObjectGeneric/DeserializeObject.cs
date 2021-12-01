using ClientWebAPI.Api.Core.Enveloped;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebAPI.Api.Core.HandlingObjectGeneric
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
