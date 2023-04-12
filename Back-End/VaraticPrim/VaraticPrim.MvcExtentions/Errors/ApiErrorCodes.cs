namespace VaraticPrim.MvcExtentions.Errors;

public static class ApiErrorCodes
{
    public const string Forbidden                  = "forbidden";
    public const string Unauthorized               = "unauthorized";
    public const string IdentityNotFound           = "user_not_found";
    public const string InternalServerError        = "internal_server_error";
    public const string InvalidData                = "invalid_data";
    public const string NewPasswordMustBeDifferent = "new_password_must_be_different";

    public static class Register
    {
        public const string UserAlreadyExists = "user_already_exists";
        public const string UserDisabled      = "user_disabled";
    }

    public static class Authentication
    {
        public const string InvalidEmailOrPassword            = "invalid_email_or_password";
        public const string InvalidFirstNameOrLastNameOrPhone = "invalid_firstname_or_lastname_or_phone";
        public const string InvalidRefreshToken               = "invalid_refresh_token";
    }
}