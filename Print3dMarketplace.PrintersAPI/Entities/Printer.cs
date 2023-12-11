using Print3dMarketplace.Common.Data;

namespace Print3dMarketplace.PrintersAPI.Entities;

public class Printer : BaseEntity
{
	public Guid ApplicationUserId { get; set; }

	public TemplatePrinter TemplatePrinter { get; set; }
	public Guid TemplatePrinterId { get; set; }

	public Nozzle Nozzle { get; set; }
	public Guid NozzleId { get; set; }

	public string ModelName { get; set; }

	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }

	public bool IsActive { get; set; }
}
