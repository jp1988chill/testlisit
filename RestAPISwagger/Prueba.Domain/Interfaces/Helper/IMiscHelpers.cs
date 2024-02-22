using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prueba.Domain.Interfaces.Helper
{
    public interface IMiscHelpers
    {
        Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(HttpClient httpClient, string requestUri, TValue value);
    }
}
