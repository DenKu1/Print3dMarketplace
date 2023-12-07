using Print3dMarketplace.Common.Data;

namespace Print3dMarketplace.PrintersAPI.Entities;

public class TemplatePrinter : BaseEntity
{
	public string ModelName { get; set; }
	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }
}
