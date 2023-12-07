using Print3dMarketplace.Common.Data;

namespace Print3dMarketplace.MaterialsAPI.Entities;

public class Material : BaseEntity
{
	public Guid ApplicationUserId { get; set; }

	public Color Color { get; set; }
	public Guid ColorId { get; set; }

	public TemplateMaterial TemplateMaterial { get; set; }
	public Guid TemplateMaterialId { get; set; }

	public string Name { get; set; }
	public bool IsActive { get; set; }
}
