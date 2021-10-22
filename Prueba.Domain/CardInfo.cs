using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prueba.Domain
{
    public class CardInfoResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<Card> cardInfoResponse { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}