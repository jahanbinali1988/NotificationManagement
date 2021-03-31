using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common.AspNetCore.Model;
using Common.Core.Exceptions;
using Common.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
 
namespace Common.AspNetCore
{
    public class BaseApiController : ControllerBase, IActionFilter
    {
        private readonly ILogger _logger;

        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }

        protected int? TotalItemsCount { get; set; }

        protected int? RemainingItemsCount { get; set; }

        protected string NextUrl { get; set; }

        protected string VideosNextUrl { get; set; }

        protected HttpStatusCode? ResponseStatusCode { get; set; }

        /// <summary>
        /// for customize 200 response messages
        /// </summary>
        protected FilmnetStatusCode ResponseScratCode { get; set; }

        [NonAction]
        protected ActionResult FilmnetResponse(HttpStatusCode statusCode, FilmnetStatusCode code, object body = null, object metaBody = null)
        {
            ResponseStatusCode = statusCode;
            return StatusCode((int)statusCode, new ApiResponse()
            {
                Data = body,
                Meta = new MetaApiResponse()
                {
                    CustomDisplayMessage = code.DisplayMessage,
                    Message = code.Description,
                    OperationResult = code.Code,
                }
            });
        }


        [NonAction]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        [NonAction]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var statusCode = ResponseStatusCode ?? HttpStatusCode.OK;

            if (context.Exception != null)
            {
                if (context.Exception is BusinessException businessException)
                {
                    context.Result = StatusCode(500, new ApiResponse()
                    {
                        Meta = new MetaApiResponse()
                        {
                            Message = businessException.Message,
                            Exception = businessException,
                            BusinessCode = businessException.Code
                        }
                    });
                    context.ExceptionHandled = true;
                }
                else
                {
                    context.Exception.Data.Add("Request-Path", Request.Path);
                    _logger.LogCritical(context.Exception, context.Exception.Message);
                    context.Result = StatusCode(500, new ApiResponse()
                    {
                        Meta = new MetaApiResponse()
                        {
                            Message = context.Exception.Message,
#if DEBUG
                            Exception = context.Exception,
#endif
                        }
                    });
                    context.ExceptionHandled = true;
                }
            }

            switch (context.Result)
            {
                case ObjectResult objectResult:
                    if (objectResult.Value != null && objectResult.Value.GetType() == typeof(ApiResponse))
                        break;

                    context.Result = StatusCode(objectResult.StatusCode ?? (int)statusCode, new ApiResponse()
                    {
                        Data = objectResult.Value,
                        Meta = new MetaApiResponse()
                        {
                        }
                    });
                    break;
                case NotFoundResult notFoundResult:
                    context.Result = StatusCode(notFoundResult.StatusCode, null);
                    break;
                case NoContentResult noContentResult:
                    context.Result = StatusCode(noContentResult.StatusCode, null);
                    break;
                case StatusCodeResult statusCodeResult:
                    context.Result = StatusCode(statusCodeResult.StatusCode, null);
                    break;
                default:
                    throw new NotImplementedException();
            }

        }
    }
}