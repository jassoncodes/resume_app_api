using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jasson.Codes.Api.Models.Entities;

[Table("contact")]
public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("github")]
    public string Github { get; set; }

    [Column("linkedin")]
    public string Linkedin { get; set; }
}
