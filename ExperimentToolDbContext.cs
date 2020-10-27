using ExperimentToolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExperimentToolApi
{
    public class ExperimentToolDbContext : DbContext
    {
        public ExperimentToolDbContext(DbContextOptions<ExperimentToolDbContext> options): base(options){

        } 
        public DbSet<User> Users {get; set;}
        public DbSet<Material> Materials {get;set;}
        public DbSet<TextureAnalysis> TextureAnalyses {get;set;}
        public DbSet<TensileTest> TensileTests {get; set;}
        public DbSet<TensileResult> TensileResults {get; set;}
        public DbSet<CompressionTest> CompressionTests {get; set;}
        public DbSet<CompressionResult> CompressionResults {get; set;}
        public DbSet<AdditionalFile> AdditionalFiles {get;set;}
    }
}