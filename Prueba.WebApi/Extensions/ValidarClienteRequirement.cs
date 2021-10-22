using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.WebApi.Extensions
{
    public class ValidarClienteRequirement : IAuthorizationRequirement
    {
        public ValidarClienteRequirement(bool equal)
        {
            Equal = equal;
        }
        public bool Equal { get; set; }
    }
}
