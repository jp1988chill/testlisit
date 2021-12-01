using ClientWebAPI.Api.Core.Enveloped;
using ClientWebAPI.Api.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebAPI.Api.Core.Middleware
{
    public class ErrorHandling
    {
        private readonly RequestDelegate next;
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly ILogger _log;
        private static string DateInternalServerError;

        public ErrorHandling(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _log = loggerFactory.CreateLogger<ErrorHandling>();

            _jsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                DateInternalServerError = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _jsonSettings);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, JsonSerializerSettings Js)
        {
            Stream originalBody = context.Response.Body;

            var bodyContent = string.Empty;
            context.Response.Body = originalBody;

            HttpStatusCode code = HttpStatusCode.InternalServerError;

            if (ex is NotImplementedException) code = HttpStatusCode.NotImplemented;
            else if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (ex is MantenedorMVCEntityNotFoundException) code = HttpStatusCode.NotFound;
            else if (ex is MantenedorMVCEntityException) code = HttpStatusCode.BadRequest;


            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;
                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;

                var env = EnvelopedError<Exception>(ex, context, code);

                bodyContent = JsonConvert.SerializeObject(env, Js);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;

                context.Response.WriteAsync(bodyContent, Encoding.UTF8);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                return memStream.CopyToAsync(originalBody);
            }
        }

        public static EnvelopedObject.Enveloped EnvelopedError<T>(Exception ex, HttpContext context, HttpStatusCode code)
        {
            List<string> levelErrorList = new List<string>() { "Technical", "Timeout ", "Functional" };
            string levelError = string.Empty;
            string typeError = string.Empty;
            string backendError = string.Empty;
            string codeError = string.Empty;
            string descriptionError = string.Empty;
            string inDate = string.Empty;

            if (ex.Message.IndexOf('$') > -1)
                inDate = ex.Message.Split('$')[1].Trim();

            if (ex.Message.IndexOf("ORA") > -1)
            {
                levelError = "Fatal";
                typeError = levelErrorList[2];
                backendError = ex.Message.Split(':')[1].Trim();//ex.Message.Split(':')[3].Trim().Replace("\n", "%").Split('%')[0];
                codeError = ex.Message.Split(':')[0];
                descriptionError = ex.Message;//ex.Message.Split(':')[0] + ": " + ex.Message.Split(':')[1];
            }
            else if (ex.Message.IndexOf("TimeOut") > -1 || ex.Message.IndexOf("timed out") > -1)
            {
                levelError = "Fatal";
                typeError = levelErrorList[1];
                backendError = ex.Message;
                codeError = Convert.ToString((int)code + " - " + code.ToString());
                descriptionError = ex.Message;
                inDate = DateInternalServerError;
            }
            else
            {
                levelError = "Fatal";
                typeError = levelErrorList[0];
                backendError = ex.GetType().Name;
                codeError = Convert.ToString((int)code + " - " + code.ToString());
                descriptionError = ex.Message;
                inDate = DateInternalServerError;
            }

            var ss = ex.GetType().Name;

            var env = new EnvelopedObject.Enveloped
            {
                header = new EnvelopedObject.Header
                {
                    transactionData = new EnvelopedObject.Transactiondata
                    {
                        idTransaction = "WEB2019043000000100",
                        startDate = inDate,
                        endDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                    }
                },
                body = new EnvelopedObject.Body
                {
                    error = new EnvelopedObject.Error
                    {
                        type = levelErrorList[2],
                        description = ex.Message.Split('$')[0].Trim(),
                        detail = new List<EnvelopedObject.Detail>
                        {
                            new EnvelopedObject.Detail
                            {
                                level = levelError,
                                type = typeError,
                                backend = backendError,
                                code = codeError,
                                description = descriptionError
                            }
                        }
                    }
                }
            };

            return env;
        }
    }
}
