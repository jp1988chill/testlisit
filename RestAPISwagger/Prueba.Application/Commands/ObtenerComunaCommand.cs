using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Application.Commands
{
    public class ObtenerComunaCommand : IRequest<ComunaResponse>
    {
        public string Idcomuna { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ObtenerComunasCommand : IRequest<ComunaResponse>
    {

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
