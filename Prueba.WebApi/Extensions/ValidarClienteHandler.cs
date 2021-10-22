using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Prueba.Domain;
using Prueba.Repository;
using Prueba.WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.WebApi.Extensions
{
    public class ValidarClienteHandler : AuthorizationHandler<ValidarClienteRequirement>
    {
        private readonly IServiceScopeFactory _scopeFactory; //no podemos acceder al objeto PruebaContext desde acá aún, porque no se ha generado (arroja error transiente), por lo tanto, accedemos de esta manera.
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        private IRepositoryEntityFrameworkCQRS<User> userRepository = null;
        public ValidarClienteHandler(IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            this._scopeFactory = scopeFactory;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidarClienteRequirement requirement)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<PruebaContext>();
                userRepository = new RepositoryEntityFrameworkCQRS<User>(ctx);

                //Se obtiene el token
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                string token = httpContext.Request.Headers["Token"];
                if (string.IsNullOrEmpty(token))
                {
                    httpContext = CustomMessage(httpContext, "El request no tiene Token. Agregue uno.");
                    context.Fail();
                    return Task.CompletedTask;
                }

                //Validamos que el lease time no sea mayor a 10 minutos
                try
                {
                    User user = userRepository.GetByID(new Guid(token));
                    DateTime userDT = DateTime.Parse(user.Tokenleasetime);
                    int result = DateTime.Compare(DateTime.Now, userDT);
                    if (result < 0)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        httpContext = CustomMessage(httpContext, "El Token del usuario se encuentra expirado ("+ userDT.ToShortTimeString()+"). Genere un nuevo Token");
                        context.Fail();
                    }
                }
                catch (Exception ex) {
                    httpContext = CustomMessage(httpContext, ex.Message);
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }

        private HttpContext CustomMessage(HttpContext httpContext, string userFriendly)
        {
            var bytes = Encoding.UTF8.GetBytes(new ErrorDetails
            {
                HttpCode = (int)HttpStatusCode.Unauthorized,
                HttpMessage = "Unauthorized",
                MoreInformation = "401 – Invalid cliend id or secret",
                UserFriendlyError = userFriendly,
                Internal_id = httpContext.Request.Headers["x-transaction_id"] + "-" + httpContext.Request.Headers["X-Global-Transaction-ID"]
            }.ToString());
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            return httpContext;
        }
    }
}
