using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jasson.Codes.Api.Models.Entities;

[Table("experience")]
public class Experience
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ExperienceId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("company")]
    public string Company { get; set; }

    [Column("start_date")]
    public string StartDate { get; set; }

    [Column("end_date")]
    public string EndDate { get; set; }

    [NotMapped]
    public ICollection<ActivityExperience> Activities { get; set; } = new List<ActivityExperience>();

}