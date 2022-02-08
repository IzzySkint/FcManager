using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FcManager.Data
{
	[Table("Team")]
	public class Team
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TeamId { get; set; }
		[Required]
		[MaxLength(80)]
		public string Name { get; set; }
		[Required]
		[MaxLength(100)]
		public string ManagerName { get; set; }
		[Required]
		[MaxLength(100)]
		public string CoachName { get; set; }
		[MaxLength(100)]
		public string AssistantCoachName {get; set;}
		public int StadiumId { get; set; }
		public Stadium Stadium { get; set; }
    }
}

