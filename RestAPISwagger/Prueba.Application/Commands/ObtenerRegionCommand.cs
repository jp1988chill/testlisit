using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Application.Commands
{
    public class ObtenerRegionCommand : IRequest<RegionResponse>
    {
        public string Idregion { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ObtenerRegionesCommand : IRequest<RegionResponse>
    {

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
