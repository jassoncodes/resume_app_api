using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jasson.Codes.Api.Models.Entities;

[Table("studies")]
public class Study
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("institution")]
    public string Institution { get; set; }

    [Column("start_date")]
    public string StartDate { get; set; }

    [Column("end_date")]
    public string EndDate { get; set; }

}