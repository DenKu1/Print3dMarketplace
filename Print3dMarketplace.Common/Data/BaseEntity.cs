using System.ComponentModel.DataAnnotations;

namespace Print3dMarketplace.Common.Data;

public abstract class BaseEntity
{
	[Key]
	public Guid Id { get; set; }
}
