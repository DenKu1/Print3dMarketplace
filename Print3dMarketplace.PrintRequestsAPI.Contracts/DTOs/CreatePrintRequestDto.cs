namespace Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;

public class CreatePrintRequestDto
{
	public Guid TemplateMaterialId { get; set; }
	public Guid ColorId { get; set; }
	public Guid ModelId { get; set; }

	public double LayerHeight { get; set; }
	public int Infill { get; set; }

	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }

	public string? Comment { get; set; }
	public bool? UseSupports { get; set; }
	public double? WallThickness { get; set; }
}
