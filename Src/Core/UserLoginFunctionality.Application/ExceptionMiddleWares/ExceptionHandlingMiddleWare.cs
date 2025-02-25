using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.ExceptionMiddleWares;

public class ExceptionHandlingMiddleWare : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			context.Response.ContentType="application/json";
			ExceptionModel exceptionModel;
			switch (ex)
			{
				
                case BaseException baseException:
                    context.Response.StatusCode = baseException.StatusCode;
					exceptionModel = new()
					{
						Message = ex.Message,
						StatusCode = context.Response.StatusCode,
					};
                    break;
                default:
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					exceptionModel = new()
					{
						Message = "An unexpected error occurred.",
						StatusCode = context.Response.StatusCode,
					};
					break;
			}
			string json =JsonSerializer.Serialize(exceptionModel);
			await context.Response.WriteAsync(json);	
		}
    }
}
