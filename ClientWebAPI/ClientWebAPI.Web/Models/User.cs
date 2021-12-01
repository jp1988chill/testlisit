using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClientWebAPI.Web.Models
{
    public partial class User
    {
        public User(string name, string password, Guid token, string tokenleasetime)
        {
            this.Name = name;
            this.Password = password;
            this.Token = token;
            this.Tokenleasetime = tokenleasetime;
        }
        public Guid Token { get; set; }     //Token de usuario generado con duración de N minutos
        public string Name { get; set; }    //Nombre de tarjetahabiente que contiene 1:1 class Card
        public string Password { get; set; }
        public string Tokenleasetime { get; set; } //allows the token to last up to 10 minutes
    }

    public partial class UserResponse
    {
        public string Token { get; set; }     //Token de usuario generado con duración de N minutos
        public string Name { get; set; }    //Nombre de tarjetahabiente que contiene 1:1 class Card
        public string Password { get; set; }
        public string Tokenleasetime { get; set; } //allows the token to last up to 10 minutes

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
