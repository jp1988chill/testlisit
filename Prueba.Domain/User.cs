using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prueba.Domain
{
    public class User
    {
        public User(string name, string password, Guid token)
        {
            this.Name = name;
            this.Password = password;
            this.Token = token;
        }
        [Key]
        public Guid Token { get; set; }     //Token de usuario generado con duración de N minutos
        public string Name { get; set; }    //Nombre de tarjetahabiente que contiene 1:1 class Card
        public string Password { get; set; }
    }

    public class UserResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }

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