using Prueba.Domain.Interfaces.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Repository
{
    public class MiscHelpers : IMiscHelpers
    {
        public MiscHelpers()
        {
        }
        public async Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(HttpClient httpClient, string requestUri, TValue value)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(value),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri, UriKind.Relative)
            };
            return await httpClient.SendAsync(request);
        }
    }
}
