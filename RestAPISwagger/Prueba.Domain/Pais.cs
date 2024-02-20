using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace Prueba.Domain
{
    public class Pais
    {
        public Pais()
        {
            
        }
        public Pais(string nombre, List<int> idregion)
        {
            this.Nombre = nombre;
            this.Idregion = idregion;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Idpais { get; set; } //PK: Autoinc
        public string Nombre { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public List<int> Idregion { get; set; } // 1 País : N Regiones
}

    public class PaisResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<Pais> Paises { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class PaisBody
    {
        public List<Pais> Paises { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}