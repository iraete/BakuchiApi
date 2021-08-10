using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using BakuchiApi.Models.Configuration;

public class BakuchiContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Pool> Pools { get; set; }
    public DbSet<Wager> Wagers { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<Outcome> Outcomes { get; set; }
    public DbSet<Result> Results { get; set; }

    public BakuchiContext(DbContextOptions<BakuchiContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        new UserEntityTypeConfiguration()
            .Configure(modelBuilder.Entity<User>());
        new EventEntityTypeConfiguration()
            .Configure(modelBuilder.Entity<Event>());
        new PoolEntityTypeConfiguration()
            .Configure(modelBuilder.Entity<Pool>());
        new WagerEntityTypeConfiguration()
            .Configure(modelBuilder.Entity<Wager>());
        new OutcomeEntityTypeConfiguration()
            .Configure(modelBuilder.Entity<Outcome>());
        new ResultEntityTypeConfiguration()
            .Configure(modelBuilder.Entity<Result>());
    }
}