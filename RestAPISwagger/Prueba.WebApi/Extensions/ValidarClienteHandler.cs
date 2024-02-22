using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prueba.Domain;
using Prueba.Repository;
using Prueba.WebApi.Controllers;
using Prueba.WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private IRepositoryEntityFrameworkCQRS<RolUser> rolUserRepository = null;
        private readonly ILogger<ValidarClienteHandler> _logger;
        public ValidarClienteHandler(IHttpContextAccessor httpContextAccessor, IServiceScopeFactory scopeFactory, ILogger<ValidarClienteHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            this._scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidarClienteRequirement requirement)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<PruebaContext>();
                userRepository = new RepositoryEntityFrameworkCQRS<User>(ctx);
                rolUserRepository = new RepositoryEntityFrameworkCQRS<RolUser>(ctx);

                //Se obtiene token con vigencia de 10 minutos. Permite al usuario autenticarse mediante rolUser, tipo Administrador, sólo durante la sesión actual y nunca otra.
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                string token = httpContext.Request.Headers["Token"];
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogInformation("Log: El request no tiene Token. Agregue uno.");
                    httpContext = CustomMessage(httpContext, "El request no tiene Token. Agregue uno.");
                    context.Fail();
                    return Task.CompletedTask;
                }

                //Validamos que el lease time no sea mayor a 10 minutos
                try
                {
                    User user = userRepository.GetAll().Where(id => id.Token == new Guid(token)).FirstOrDefault();
                    RolUser rolUser = rolUserRepository.GetAll().Where(id => id.Idroluser == user.Idroluser).FirstOrDefault();
                    if ((rolUser != null) && (rolUser.Nombreroluser == "Administrador"))
                    {
                        _logger.LogInformation("Log: ValidarCliente(): Sesión Administrador encontrada. Verificando si está expirada su sesión...");
                        DateTime userDT = DateTime.ParseExact(user.Tokenleasetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        int result = DateTime.Compare(DateTime.Now, userDT);
                        if (result < 0)
                        {
                            _logger.LogInformation("Log: ValidarCliente(): Sesión Administrador sesion válida!");
                            context.Succeed(requirement);
                        }
                        else
                        {
                            _logger.LogInformation("Log: ValidarCliente(): " + "El Token del usuario se encuentra expirado (" + userDT.ToShortTimeString() + "). Asegúrese de iniciar sesión nuevamente.");
                            httpContext = CustomMessage(httpContext, "El Token del usuario se encuentra expirado (" + userDT.ToShortTimeString() + "). Asegúrese de iniciar sesión nuevamente.");
                            context.Fail();
                        }
                    }
                    else {
                        _logger.LogInformation("Log: ValidarCliente():" + "El usuario (" + token + " ) no tiene acceso a los servicios.");
                        httpContext = CustomMessage(httpContext, "El usuario (" + token + " ) no tiene acceso a los servicios: .");
                        context.Fail();
                    }
                }
                catch (Exception ex) {
                    _logger.LogInformation("Log: ValidarCliente():" + ex.Message);
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
