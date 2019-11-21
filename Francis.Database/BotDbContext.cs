using Francis.Database.Converters;
using Francis.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Francis.Database
{
    public class BotDbContext : DbContext
    {
        private static bool _migrated = false;


        public DbSet<BotUser> Users { get; set; }

        public DbSet<Progression> Progressions { get; set; }

        public DbSet<RequestProgression> RequestProgressions { get; set; }

        public DbSet<WatchedItem> WatchedItems { get; set; }

        public DbSet<OptionValue> Options { get; set; }


        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            if (!_migrated)
            {
                Database.Migrate();
                _migrated = true;
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RequestProgression>(entity =>
            {
                entity.Property(x => x.ExcludedIds).HasJsonConversion();
            });
        }
    }
}
