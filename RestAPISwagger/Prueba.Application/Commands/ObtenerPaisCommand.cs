using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Application.Commands
{
    public class ObtenerPaisCommand : IRequest<PaisResponse>
    {
        public string IdPais { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ObtenerPaisesCommand : IRequest<PaisResponse>
    {

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
