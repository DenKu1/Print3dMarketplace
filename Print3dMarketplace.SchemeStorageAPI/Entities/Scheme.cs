using Print3dMarketplace.AuthAPI.Entities;
using Print3dMarketplace.Common.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Print3dMarketplace.SchemeStorageAPI.Models;

public class Scheme : BaseEntity
{
	[ForeignKey(nameof(ApplicationUser))]
	public Guid UserId { get; set; }

	public Guid TemplateMaterialId { get; set; }

	public string FileName { get; set; }
}
