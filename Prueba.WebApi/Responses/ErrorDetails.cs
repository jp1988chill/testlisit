using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Prueba.WebApi.Responses
{
    public class ErrorDetails
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string UserFriendlyError { get; set; }
        public string Internal_id { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
