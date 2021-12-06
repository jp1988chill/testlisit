using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prueba.UnitTests.Core.Enveloped
{
    public class EnvelopedObject
    {
        public class Enveloped
        {
            public Header header { get; set; }
            public dynamic body { get; set; }
        }

        public class Header
        {
            public Consumerdata consumerData { get; set; }
            public Transactiondata transactionData { get; set; }
        }

        public class Consumerdata
        {
            [Required(ErrorMessage = "Debe ingresar IP.")]
            public string ip { get; set; }
            [Required(ErrorMessage = "Debe ingresar Canal.")]
            public string channel { get; set; }
            [Required(ErrorMessage = "Debe ingresar Modulo.")]
            public string module { get; set; }
            [Required(ErrorMessage = "Debe ingresar Funcionalidad.")]
            public string functionality { get; set; }
            [Required(ErrorMessage = "Debe ingresar Servicio.")]
            public string service { get; set; }
            [Required(ErrorMessage = "Debe ingresar Tipo de Servicio.")]
            public string serviceType { get; set; }
            [Required(ErrorMessage = "Debe ingresar ID Usuario.")]
            public string userId { get; set; }
        }

        public class Transactiondata
        {
            public string idTransaction { get; set; }           /* ID Transacción actual */
            public string idTransaction_Rel { get; set; }       /* ID Transacción Padre */
            public string userType { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string date { get; set; }
        }

        public class Body
        {
            public dynamic responseBody { get; set; }
            //public ResponseBody response { get; set; }
            public Error error { get; set; }
        }

        public class ResponseBody
        {
            public dynamic responseBody { get; set; }
        }

        public class Error
        {
            public string type { get; set; }
            public string description { get; set; }
            public List<Detail> detail { get; set; }
        }

        public class Detail
        {
            public string level { get; set; }
            public string type { get; set; }
            public string backend { get; set; }
            public string code { get; set; }
            public string description { get; set; }
        }
    }
}
