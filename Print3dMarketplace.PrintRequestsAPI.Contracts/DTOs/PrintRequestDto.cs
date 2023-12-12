namespace Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;

public class PrintRequestDto
{
	public Guid Id { get; set; }

	public Guid ApplicationUserId { get; set; }
	public Guid PrintRequestStatusId { get; set; }
	public Guid TemplateMaterialId { get; set; }
	public Guid NozzleId { get; set; }
	public Guid ColorId { get; set; }
	public Guid ModelId { get; set; }

	public int Infill { get; set; }

	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }

	public double BorderWidth { get; set; }
	public double LayerHeight { get; set; }

	public string Comment { get; set; }

	public bool UseSupports { get; set; }
	public bool IsActive { get; set; }
}
