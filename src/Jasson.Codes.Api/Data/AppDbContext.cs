using Microsoft.EntityFrameworkCore;
using Jasson.Codes.Api.Models.Entities;

namespace Jasson.Codes.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Experience>()
            .HasMany(e => e.Activities)
            .WithOne(e => e.Experience)
            .HasForeignKey(e => e.ExperienceId)
            .HasPrincipalKey(e => e.ExperienceId);
    }

    public DbSet<Contact> ContactInfo { get; set; } = default!;
    public DbSet<Study> Studies { get; set; } = default!;


    public DbSet<Experience> Experiences { get; set; } = default!;
    public DbSet<ActivityExperience> ExperienceActivies { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;

    public DbSet<About> AboutInfo { get; init; } = default!;
}
