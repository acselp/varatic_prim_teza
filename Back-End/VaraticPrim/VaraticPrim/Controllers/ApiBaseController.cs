using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using VaraticPrim.MvcExtentions.Errors;
using VaraticPrim.Service.Exceptions;

namespace VaraticPrim.Controllers;

public class ApiBaseController : ControllerBase
{
    protected IActionResult ValidationError(ValidationException exception)
    {
        var error = new ApiErrorModel()
        {
            Errors = exception.Errors.Select(it => new ApiErrorModel.ApiError()
            {
                AttemptedValue = it.AttemptedValue,
                ErrorCode = it.ErrorCode,
                ErrorMessage = it.ErrorMessage,
                PropertyName = it.PropertyName
            })
        };
 
        return BadRequest(error);
    }

    protected IActionResult BadRequest(ApiErrorModel model)
    {
        return RestResponse(HttpStatusCode.BadRequest, model);
    }
 
    protected IActionResult Forbidden(ApiErrorModel model)
    {
        return RestResponse(HttpStatusCode.Forbidden, model);
    }
 
    protected IActionResult ForbiddenError()
    {
        var error = CreateError(ApiErrorCodes.Forbidden)
            .SetMessage("This operation is forbidden.")
            .Build();
 
        return RestResponse(HttpStatusCode.Forbidden, error);
    }
 
    protected IActionResult UnAuthorizedActionError()
    {
        var error = CreateError(ApiErrorCodes.Forbidden)
            .SetMessage(
                "It appears that you don't have permission to access this page. Please  make sure you're authorized to view this content and/or contact your administrator.")
            .Build();
 
        return RestResponse(HttpStatusCode.Forbidden, error);
    }
 
    protected IActionResult Unauthorized(ApiErrorModel model)
    {
        return RestResponse(HttpStatusCode.Unauthorized, model);
    }
 
    protected IActionResult NotFound(ApiErrorModel model)
    {
        return RestResponse(HttpStatusCode.NotFound, model);
    }
 
    protected IActionResult RestResponse(HttpStatusCode code, object body = null)
    {
        var restResponse = new JsonResult(body) {StatusCode = (int) code};
        return restResponse;
    }
 
    protected ApiErrorBuilder CreateError(string code)
    {
        var descriptor = ControllerContext
            .ActionDescriptor;
        
        return ApiErrorBuilder.New()
            .SetCode(code);
    }
 
    protected IActionResult BadRequest(string errorCode, string errorMessage)
    {
        var error = CreateError(errorCode)
            .SetMessage(errorMessage)
            .Build();
        return BadRequest(error);
    }
}