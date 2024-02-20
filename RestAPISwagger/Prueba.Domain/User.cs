using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class User 
    {
        public User(Guid token, int idroluser, string name, string password, string tokenleasetime)
        {
            this.Token = token;
            this.Idroluser = idroluser;
            this.Name = name;
            this.Password = password;
            this.Tokenleasetime = tokenleasetime;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Iduser { get; set; } //PK: Autoinc
        
        public Guid Token { get; set; }     //Token de usuario generado con duración de N minutos

        public int Idroluser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Tokenleasetime { get; set; } //allows the token to last up to 10 minutes
    }

    public class UserResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<User> usersNuevoTokenAsignado { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class UserBody
    {
        public List<User> Users { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}