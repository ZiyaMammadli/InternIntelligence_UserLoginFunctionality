using Microsoft.AspNetCore.Builder;

namespace UserLoginFunctionality.Application.ExceptionMiddleWares;

public static class ExceptionMiddleWare
{
    public static void ExceptionHandlingMiddleWare(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleWare>();
    }
}
