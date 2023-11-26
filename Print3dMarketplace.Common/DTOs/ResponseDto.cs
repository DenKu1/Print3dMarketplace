namespace Print3dMarketplace.Common.DTOs;

public class ResponseDto
{
	public object? Result { get; set; }
	public bool IsSuccess { get; set; }
	public string Message { get; set; }

	public static ResponseDto SuccessResponse(object? result = null) => new()
	{
		IsSuccess = true,
		Message = string.Empty,
		Result = result
	};

	public static ResponseDto ErrorResponse(string message = "") => new()
	{
		IsSuccess = false,
		Message = message,
		Result = null
	};
}
