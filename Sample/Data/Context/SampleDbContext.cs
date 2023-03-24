using Microsoft.EntityFrameworkCore;

namespace Sample.Data
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Block> Blocks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Block>(entity =>
            {
                entity.HasKey(e => e.blockID);
                entity.Property(e => e.hash).HasMaxLength(66);
                entity.Property(e => e.parentHash).HasMaxLength(66);
                entity.Property(e => e.miner).HasMaxLength(42);
                entity.Property(e => e.blockReward).HasPrecision(50,0);
                entity.Property(e => e.gasLimit).HasPrecision(50, 0);
                entity.Property(e => e.gasUsed).HasPrecision(50, 0);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.transactionID);
                entity.Property(e => e.hash).HasMaxLength(66);
                entity.Property(e => e.from).HasMaxLength(42);
                entity.Property(e => e.to).HasMaxLength(42);
                entity.Property(e => e.value).HasPrecision(50, 0);
                entity.Property(e => e.gas).HasPrecision(50, 0);
                entity.Property(e => e.gasPrice).HasPrecision(50, 0);
                entity.HasOne(e => e.block).WithMany(e => e.Transactions);
            });
        }
    }
}
