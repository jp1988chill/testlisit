using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class Pais
    {
        public Pais(string _Nombre, List<int> _IdRegion)
        {
            this.Nombre = _Nombre;
            this.IdRegion = _IdRegion;

        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int IdPais { get; set; } //PK: Autoinc
        public string Nombre { get; set; }
        public List<int> IdRegion { get; set; } // 1 País : N Regiones
    }

    public class PaisResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public Pais pais { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class PaisBody
    {
        public Pais pais { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}