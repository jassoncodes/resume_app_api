using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jasson.Codes.Api.Models.Entities;

[Table("projects")]
public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("project_title")]
    public string Title { get; set; }

    [Column("repo")]
    public string Repo { get; set; }

    [Column("live_link")]
    public string LiveLink { get; set; }

    [Column("project_description")]
    public string Description { get; set; }

    [Column("stack")]
    public string Stack { get; set; }
}