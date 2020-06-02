using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sloth.Common.Exceptions;

namespace Sloth.Api.Middlewares
{
    public class HttpCustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpCustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SlothEntityNotFoundException ex)
            {
                await CreateResponse(HttpStatusCode.NotFound, context, ex);
            }
            catch (SlothException ex)
            {
                await CreateResponse(HttpStatusCode.BadRequest, context, ex);
            }
            catch (Exception ex)
            {
                await CreateResponse(HttpStatusCode.InternalServerError, context, ex);
            }
        }

        private static async Task CreateResponse(HttpStatusCode statusCode, HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted)
            {
                throw ex;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errors = new
            {
                Errors = new[] {
                    new {
                        ex.Message,
                        ex.Data
                    }
                }
            };

            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errors, Formatting.Indented,
                serializerSettings));
        }
    }
}
