using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class Region
    {
        public Region(string _Nombre, List<int> _Comunas)
        {
            this.Nombre = _Nombre;
            this.IdComuna = _Comunas;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int IdRegion { get; set; } //PK: Autoinc
        public string Nombre { get; set; }
        public List<int> IdComuna { get; set; } // 1 Region : N Comunas
    }

    public class RegionResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public Region region { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class RegionBody
    {
        public Region region { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}