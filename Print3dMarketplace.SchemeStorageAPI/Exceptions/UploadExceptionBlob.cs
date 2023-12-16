namespace Print3dMarketplace.SchemeStorageAPI.Exceptions;

[Serializable]
public class UploadExceptionBlob : Exception
{
	public UploadExceptionBlob(string message) 
		: base(message) { }
}
