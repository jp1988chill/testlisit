using ClientWebAPI.Api.Core.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebAPI.Api.Core.AppBuilderExtensions
{
    public static class ErrorHandlingMiddlewareAppBuilderExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandling>();
        }
    }
}
