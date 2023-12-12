﻿using Print3dMarketplace.Common.Data;

namespace Print3dMarketplace.PrintRequestsAPI.Entities;

public class PrintRequest : BaseEntity
{
	public Guid ApplicationUserId { get; set; }

	public PrintRequestStatus PrintRequestStatus { get; set; }
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