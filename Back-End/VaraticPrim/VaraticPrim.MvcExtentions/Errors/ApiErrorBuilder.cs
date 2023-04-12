namespace VaraticPrim.MvcExtentions.Errors;

public class ApiErrorBuilder
{
    private string _code;
    private string _message;
 
    private ApiErrorBuilder()
    {
    }
 
    public static ApiErrorBuilder New()
    {
        var builder = new ApiErrorBuilder();
 
        return builder;
    }
 
    public ApiErrorBuilder SetCode(string code)
    {
        _code = code;
 
        return this;
    }
 
    public ApiErrorBuilder SetMessage(string message)
    {
        _message = message;
 
        return this;
    }
 
    public ApiErrorModel Build()
    {
        return new ApiErrorModel
        {
            Code = _code,
            Message = _message,
        };
    }
}
