namespace Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;

/// <summary>
/// Mapping paramaters from body for saving stl file
/// </summary>
public class StlSchemeRequestDTO
{
	/// <summary>
	/// Bynary file data of scheme 
	/// </summary>
	public byte[] Data { get; set; }

	/// <summary>
	/// File Name of saving file
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	/// Guid User ID
	/// </summary>
	public string UserId { get; set; }
}
