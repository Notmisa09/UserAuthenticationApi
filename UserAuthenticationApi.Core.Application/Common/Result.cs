namespace UserAuthenticationApi.Core.Application.Common
{
    public class Result<T>
    {
        public T Value { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        public Result(T value ,string message, bool issuccess)
        {
            Value = value;
            ErrorMessage = message;
            IsSuccess = issuccess;
        }

        public static Result<T> Success(T value, string message) => new(value, message, false);

        public static Result<T> Failure(string message) => new(default!, message, true);
    }
}
