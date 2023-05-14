namespace VaraticPrim.Framework.Errors;

public static class FrontEndErrors
{
    public static Error UserNotFound { get; } = new Error()
    {
        ErrorCode = "user_not_found",
        ErrorMessage = "User not found"
    };
    
    public static Error ServiceNotFound { get; } = new Error()
    {
        ErrorCode = "service_not_found",
        ErrorMessage = "Service not found"
    };
    
    public static Error LocationNotFound { get; } = new Error()
    {
        ErrorCode = "location_not_found",
        ErrorMessage = "Location not found"
    };
    
    public static Error CounterNotFound { get; } = new Error()
    {
        ErrorCode = "counter_not_found",
        ErrorMessage = "Counter not found"
    };
    
    public static Error EmailAlreadyExists { get; } = new Error()
    {
        ErrorCode = "email_already_exists",
        ErrorMessage = "Email already exists"
    };
    
    public static Error UserAlreadyExists { get; } = new Error()
    {
        ErrorCode = "user_already_exists",
        ErrorMessage = "User already exists"
    };

    public static Error EmailOrPasswordNotFound { get; } = new Error()
    {
        ErrorCode = "email_password_not_found",
        ErrorMessage = "Email or password incorrect"
    };

    public static Error InvalidToken { get; } = new Error()
    {
        ErrorCode = "invalid_token",
        ErrorMessage = "Invalid token"
    };
    
    public static Error InvalidCounterValue { get; } = new Error()
    {
        ErrorCode = "invalid_counter_value",
        ErrorMessage = "Invalid counter value"
    };
}