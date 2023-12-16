namespace Print3dMarketplace.Common.DTOs;

public class ResponseDto
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }

	public static ResponseDto SuccessResponse(string message = "") => new()
	{
		IsSuccess = true,
		Message = message
	};

	public static ResponseDto ErrorResponse(string message = "") => new()
	{
		IsSuccess = false,
		Message = message
	};
}

public class ResponseDto<T> : ResponseDto where T : class
{
	public T Result { get; set; }

	public static ResponseDto<T> SuccessResponse(T result, string message = "") => new()
	{
		IsSuccess = true,
		Message = message,
		Result = result
	};
}
