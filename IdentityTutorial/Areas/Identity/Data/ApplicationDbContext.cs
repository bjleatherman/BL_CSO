using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityTutorial.Models.ViewModels;

namespace IdentityTutorial.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<IdentityTutorial.Models.UserGame>? UserGames { get; set; }
    public DbSet<IdentityTutorial.Models.Game>? Games { get; set; }
    public DbSet<IdentityTutorial.Models.Player>? Players { get; set; }
    public DbSet<IdentityTutorial.Models.PlayerFriend>? PlayerFriends { get; set; }
    public DbSet<IdentityTutorial.Models.PlayerGameSave>? PlayerGameSaves { get; set; }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //builder.Property(u => u.UserName).HasMaxLength(255);
        //builder.Property(u => u.FirstName).HasMaxLength(255);
        //builder.Property(u => u.LastName).HasMaxLength(255);
    }

    public void Configuration(EntityTypeBuilder<UserGame> builder)
    {
        builder.Property(u => u.Id);
        builder.Property(u => u.User1Id);
        builder.Property(u => u.User2Id);
        builder.Property(u => u.GameId);
    }

    public void Configuration(EntityTypeBuilder<Game> builder)
    {
        builder.Property(u => u.GameId);
        builder.Property(u => u.StartDate);
        builder.Property(u => u.LastMoveDate);
        builder.Property(u => u.MapLength);
        builder.Property(u => u.MapWidth);
        builder.Property(u => u.IslandCoordinates);
        builder.Property(u => u.MaxHealth);
        builder.Property(u => u.MaxMines);
        builder.Property(u => u.MaxDrones);
        builder.Property(u => u.MaxSneak);
        builder.Property(u => u.MaxTorpedo);
        builder.Property(u => u.MaxSonar);
    }
    public void Configuration(EntityTypeBuilder<Player> builder)
    {
        builder.Property(u => u.Id);
        builder.Property(u => u.PlayerName);
        builder.Property(u => u.LastActiveTime);
    }

    public void Configuration(EntityTypeBuilder<PlayerFriend> builder)
    {
        builder.Property(u => u.Id);
        builder.Property(u => u.PlayerId);
        builder.Property(u => u.FriendId);
        builder.Property(u => u.Status);
    }

    public void Configuration(EntityTypeBuilder<PlayerGameSave> builder)
    {
        builder.Property(u => u.Id);
        builder.Property(u => u.GameId);
        builder.Property(u => u.PlayerId);
        builder.Property(u => u.IsChallenger);
        builder.Property(u => u.IsTurn);
        builder.Property(u => u.TotalDirectionHistory);
        builder.Property(u => u.CurrentDirectionHistory);
        builder.Property(u => u.TotalCoordinateHistory);
        builder.Property(u => u.CurrentCoordinateHistory);
        builder.Property(u => u.Health);
        builder.Property(u => u.Mines);
        builder.Property(u => u.Drones);
        builder.Property(u => u.Sneak);
        builder.Property(u => u.Torpedo);
        builder.Property(u => u.Sonar);
        builder.Property(u => u.SystemsAttack);
        builder.Property(u => u.SystemsDetect);
        builder.Property(u => u.SystemsEvade);
        builder.Property(u => u.SystemsReactor);
    }
}
