using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcManager.Data
{
	[Table("Player")]
	public class Player
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PlayerId { get; set; }
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[MaxLength(50)]
		public string MiddleName { get; set; }
		[Required]
		[MaxLength(80)]
		public string LastName { get; set; }
		[MaxLength(50)]
		public string NickName { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public double Height { get; set; }
		[Required]
		public double Weight { get; set; }
		[Required]
		public int PositionId { get; set; }
		public int TeamId { get; set; }
		public Team Team { get; set; }
		public Position Position { get; set; }
	}
}

