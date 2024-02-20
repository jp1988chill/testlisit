using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class ServicioSocial
    {
        public ServicioSocial(int _IdComuna, int _IdUser, string _NombreServicioSocial)
        {
            this.IdComuna = _IdComuna;
            this.IdUser = _IdUser;
            this.NombreServicioSocial = _NombreServicioSocial;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int IdServicioSocial { get; set; } //PK: Autoinc
        public int IdComuna { get; set; }
        public int IdUser { get; set; }

        public string NombreServicioSocial { get; set; }
    }

    public class ServicioSocialResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public ServicioSocial servicioSocial { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ServicioSocialBody
    {
        public ServicioSocial servicioSocial { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}