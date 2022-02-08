using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcManager.Data
{
	[Table("Province")]
	public class Province
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ProvinceId { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
	}
}

