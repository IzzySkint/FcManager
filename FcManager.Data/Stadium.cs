using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcManager.Data
{
	[Table("Stadium")]
	public class Stadium
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StadiumId { get; set; }
		[Required]
		[MaxLength(80)]
		public string Name { get; set; }
		[Required]
		public int Capacity { get; set; }
		[Required]
		[MaxLength(100)]
		public string AddressLine1 { get; set; }
		[MaxLength(100)]
		public string AddressLine2 { get; set; }
		[Required]
		[MaxLength(80)]
		public string Suburb { get; set; }
		[Required]
		[MaxLength(80)]
		public string City { get; set; }
		public int ProvinceId { get; set; }
		public Province Province { get; set; }
	}
}

