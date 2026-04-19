using System.Text.Json.Serialization;

namespace ECommerce.Common
{
    public class GeneralResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, List<Errors>> Errors { get; set; } = null!;

        
        public static GeneralResult Success(string message = "Operation completed successfully.")
            => new() { IsSuccess = true, Message = message };
        public static GeneralResult Failure(string message = "Operation failed.")
            => new() { IsSuccess = false, Message = message };
        public static GeneralResult NotFound(string message = "The requested resource was not found.")
            => new() { IsSuccess = false, Message = message };
        public static GeneralResult ValidationFailure(Dictionary<string, List<Errors>> errors, string message = "Validation failed.")
            => new() { IsSuccess = false, Message = message, Errors = errors };

    }

    public class GeneralResult<T> : GeneralResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public static GeneralResult<T> Success(T data, string message = "Operation completed successfully.")
            => new() { IsSuccess = true, Message = message, Data = data };

            public new static GeneralResult<T> Failure(string message = "Operation failed.")
            => new() { IsSuccess = false, Message = message };

           public new static GeneralResult<T> NotFound(string message = "The requested resource was not found.")
            => new() { IsSuccess = false, Message = message };



    }
}
