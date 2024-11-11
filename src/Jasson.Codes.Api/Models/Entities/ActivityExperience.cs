using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jasson.Codes.Api.Models.Entities;

[Table("activity_experience")]
public class ActivityExperience
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("activity_title")]
    public string ActivityTitle { get; set; }

    [Column("activity_description")]
    public string ActivityDescription { get; set; }

    [Column("experience_id")]
    public int ExperienceId { get; set; }

    [NotMapped]
    public Experience Experience { get; set; }

}