using Microsoft.AspNetCore.Builder;
using Prueba.UnitTests.Core.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.UnitTests.Core.AppBuilderExtensions
{
    public static class ErrorHandlingMiddlewareAppBuilderExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandling>();
        }
    }
}
