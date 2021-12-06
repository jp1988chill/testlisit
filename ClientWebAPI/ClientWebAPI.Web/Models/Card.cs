using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClientWebAPI.Web.Models
{
    public partial class Card
    {
        public System.Guid Id { get; set; } //ID objeto interno .NET
        public string Name { get; set; }    //nombre tarjetahabiente
        public string Pan { get; set; } //número de tarjeta(PAN) formato: 1234-1234-1234-1234.
        public string Pin { get; set; } //clave de 4 dígitos para usuario.
        public string Estado { get; set; }  //“vigente” o “no vigente”
        public decimal Amount { get; set; } //cantidad
    }

    public partial class CardResponse
    {
        public string Id { get; set; } //ID objeto interno .NET
        public string Name { get; set; }    //nombre tarjetahabiente
        public string Pan { get; set; } //número de tarjeta(PAN) formato: 1234-1234-1234-1234.
        public string Pin { get; set; } //clave de 4 dígitos para usuario.
        public string Estado { get; set; }  //“vigente” o “no vigente”
        public string Amount { get; set; } //cantidad
        public List<Card> Card { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public partial class CardBody
    {
        public List<Card> Cards { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
