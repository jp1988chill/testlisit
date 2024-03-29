﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class Comuna
    {
        public Comuna()
        {

        }
        public Comuna(string nombre)
        {
            this.Nombre = nombre;

        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int IdComuna { get; set; } //PK: Autoinc
        public string Nombre { get; set; }
    }

    public class ComunaResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<Comuna> Comunas { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ComunaBody
    {
        public string Token { get; set; } //Token con ID único que se actualiza cada vez que se autoriza el ingreso a un usuario. Protege accesos a servicios tipo: Usuario/Administrador
        public List<Comuna> Comunas { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}