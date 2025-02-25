namespace UserLoginFunctionality.Application.ExceptionMiddleWares;

public class ExceptionModel
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object Errors { get; set; }
}
