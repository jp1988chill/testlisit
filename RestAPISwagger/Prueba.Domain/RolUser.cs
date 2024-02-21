using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class RolUser
    {
        public RolUser()
        {

        }
        public RolUser(string nombreroluser)
        {
            this.Nombreroluser = nombreroluser;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Idroluser { get; set; } //PK: Autoinc
        public string Nombreroluser { get; set; }
    }

    public class RolUserResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<RolUser> RolUsers { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class RolUserBody
    {
        public List<RolUser> RolUsers { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}