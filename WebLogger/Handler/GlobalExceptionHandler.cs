using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebLogger.Handler
{
    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception.InnerException ?? context.Exception;

            context.Result = new JsonResult(exception.Message);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        }
    }
}
