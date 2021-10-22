using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Application.Commands
{
    public class DescontarSaldoTarjetaCommand : IRequest<CardResponse>
    {
        public decimal saldoPorDescontar { get; set; }
        //Cards
        [FromBody]
        public CardBody objBodyObjectRequest { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
