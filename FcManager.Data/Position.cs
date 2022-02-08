using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcManager.Data
{
	[Table("Position")]
	public class Position
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int PositionId { get; set; }
		[Required]
		public int Number { get; set; }
		[Required]
		[MaxLength(80)]
		public string Name { get; set; }
	}
}

