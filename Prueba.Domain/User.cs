using System;
using System.Collections.Generic;
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

        public string Name { get; private set; }
        public string Password { get; private set; }
        public Guid Token { get; private set; }
    }
}