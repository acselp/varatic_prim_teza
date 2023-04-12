namespace VaraticPrim.MvcExtentions.Errors;

public class ApiErrorModel
{
    public string                Code    { get; set; }
    public string                Message { get; set; }
    public IEnumerable<ApiError> Errors  { get; set; }

    public class ApiError
    {
        /// <summary>
        /// The name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The property value that caused the failure.
        /// </summary>
        public object AttemptedValue { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string ErrorCode { get; set; }
    }
}