using Print3dMarketplace.SchemeStorageAPI.Contracts.DTOs;

namespace Print3dMarketplace.SchemeStorageAPI.Helpers.Interfaces;

/// <summary>
/// Interface that describe base guard methods
/// </summary>
public interface IGuard
{
	/// <summary>
	/// Method verify if the model is suitable for preservation
	/// </summary>
	/// <param name="stlScheme">Request model which contains data</param>
	/// <exception cref="ValidationException">Throw when one of parameters null or empty</exception>
	void ValidateStlModel(StlSchemeRequestDTO stlScheme);

	/// <summary>
	/// Method verify if the model is suitable for preservation
	/// </summary>
	/// <param name="stlScheme">Request model which contains key to data</param>
	/// <exception cref="ValidationException">Throw when file name have inccorect data pattern</exception>
	void ValidateStlModelWithoudData(StlSchemeRequestDTO stlScheme);
}
