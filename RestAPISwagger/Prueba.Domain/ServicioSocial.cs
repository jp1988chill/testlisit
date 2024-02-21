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
        public ServicioSocial()
        {

        }
        public ServicioSocial(int idcomuna, int iduser, string nombreserviciosocial)
        {
            this.Idcomuna = idcomuna;
            this.Iduser = iduser;
            this.Nombreserviciosocial = nombreserviciosocial;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Idserviciosocial { get; set; } //PK: Autoinc
        public int Idcomuna { get; set; }
        public int Iduser { get; set; }

        public string Nombreserviciosocial { get; set; }
    }

    public class ServicioSocialResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<ServicioSocial> ServiciosSociales { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ServicioSocialBody
    {
        public List<ServicioSocial> ServiciosSociales { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}