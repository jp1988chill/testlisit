using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    //Clase general para implementar conversiones específicas si son requeridas durante la prueba unitaria
    public class PruebaUnitaria
    {
        public PruebaUnitaria()
        {

        }
    }

    /*
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
    */
}