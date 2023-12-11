namespace Print3dMarketplace.PrintersAPI.Entities;

public class PrinterDto
{
	public Guid ApplicationUserId { get; set; }

	public Guid TemplatePrinterId { get; set; }
	public Guid NozzleId { get; set; }

	public string ModelName { get; set; }

	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }

	public bool IsActive { get; set; }
}
