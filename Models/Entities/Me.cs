using System.ComponentModel.DataAnnotations.Schema;

namespace Jasson.Codes.Api.Models.Entities;

[NotMapped]
public class Me
{
    public About About { get; }

    public Contact ContactInfo { get; }

    public Experience Experience { get; }

    public Project Project { get; }

    public Study Study { get; }
}