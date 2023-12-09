namespace Print3dMarketplace.MaterialsAPI.Contracts.DTOs;

public class MaterialDto
{
	public Guid ColorId { get; set; }
	public Guid TemplateMaterialId { get; set; }

	public string Name { get; set; }
	public bool IsActive { get; set; }
}
