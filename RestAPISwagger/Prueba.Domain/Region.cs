﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class Region_
    {
        public Region_()
        {

        }
        public Region_(string nombre, List<int> comunas)
        {
            this.Nombre = nombre;
            this.Idcomuna = comunas;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Idregion { get; set; } //PK: Autoinc
        public string Nombre { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public List<int> Idcomuna { get; set; } // 1 Region : N Comunas
    }

    public class RegionResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<Region_> Regiones { get; set; } //return same object if operation success, or NULL if operation failed

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class RegionBody
    {
        public string Token { get; set; } //Token con ID único que se actualiza cada vez que se autoriza el ingreso a un usuario. Protege accesos a servicios tipo: Usuario/Administrador
        public List<Region_> Regiones { get; set; } //JSON format object

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}