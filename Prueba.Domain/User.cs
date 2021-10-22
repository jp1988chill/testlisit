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
        public Guid Token { get; private set; }     //Token de usuario generado con duración de N minutos
        public string Name { get; private set; }    //Nombre de tarjetahabiente que contiene 1:1 class Card
        public string Password { get; private set; }
    }
}