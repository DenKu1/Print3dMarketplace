namespace Print3dMarketplace.PrintersAPI.Contracts.DTOs;

public class TemplatePrinterDto
{
	public string ModelName { get; set; }
	public double PrintAreaLength { get; set; }
	public double PrintAreaWidth { get; set; }
	public double PrintAreaHeight { get; set; }
}
