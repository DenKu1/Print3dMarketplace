namespace Print3dMarketplace.PrintRequestsAPI.Contracts.DTOs;

public class PrintRequestDto
{
	public Guid Id { get; set; }
	public Guid ApplicationUserId { get; set; }

	public string PrintRequestStatusName { get; set; }

	public IEnumerable<SubmittedCreatorDto> SubmittedCreators { get; set; }
	public Guid CustomerSubmittedCreatorId { get; set; }

	public Guid TemplateMaterialId { get; set; }
	public Guid ColorId { get; set; }

	public double LayerHeight { get; set; }
	public int Infill { get; set; }

	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }

	public string? Comment { get; set; }
	public double? WallThickness { get; set; }

	public bool? UseSupports { get; set; }
	public bool IsActive { get; set; }
}
