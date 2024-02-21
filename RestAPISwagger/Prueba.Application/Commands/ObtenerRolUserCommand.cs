using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Application.Commands
{
    public class ObtenerRolUserCommand : IRequest<RolUserResponse>
    {
        public string IdRolUser { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ObtenerRolUsersCommand : IRequest<RolUserResponse>
    {

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
