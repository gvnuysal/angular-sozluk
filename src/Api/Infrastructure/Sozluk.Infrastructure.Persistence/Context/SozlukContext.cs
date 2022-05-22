using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.Context;

public class SozlukContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public SozlukContext()
    {
        
    }
    public SozlukContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryVote> EntryVotes { get; set; }
    public DbSet<EntryFavorite> EntryFavorites { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=sozlukdb;User=sa;Password=Tekno-900*!;", opt =>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//IEntityTypeConfiguration dan türemiş classları tarar
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Added).Select(i => (BaseEntity)i.Entity);
        PreparedAddedEntities(addedEntities);
    }

    private void PreparedAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreateDate == DateTime.MinValue)
                entity.CreateDate = DateTime.Now;
        }
    }
}