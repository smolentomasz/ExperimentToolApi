using System.Linq;
using ExperimentToolApi.Models;
using Microsoft.EntityFrameworkCore;


namespace ExperimentToolApi
{
    public class ExperimentToolDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set default precision to decimal property
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18, 10)");
            }
        }
        public ExperimentToolDbContext(DbContextOptions<ExperimentToolDbContext> options) : base(options)
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<TextureAnalysis> TextureAnalyses { get; set; }
        public DbSet<TensileTest> TensileTests { get; set; }
        public DbSet<TensileResult> TensileResults { get; set; }
        public DbSet<CompressionTest> CompressionTests { get; set; }
        public DbSet<CompressionResult> CompressionResults { get; set; }
        public DbSet<AdditionalFile> AdditionalFiles { get; set; }
    }
}