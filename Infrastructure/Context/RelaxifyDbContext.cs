using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Context
{
    public class RelaxifyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public RelaxifyDbContext()
        {
        }
        public RelaxifyDbContext(DbContextOptions<RelaxifyDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            //options.UseNpgSql(Configuration.GetConnectionString("RelaxifyDb"));
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<MeditationContent> MeditationContents { get; set; }
        public virtual DbSet<BreathingContent> BreathingContents { get; set; }
        public virtual DbSet<MusicContent> MusicContents { get; set; }
        public virtual DbSet<GameContent> GameContents { get; set; }
        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
        public virtual DbSet<UserContentLog> UserContentLogs { get; set; }
        public virtual DbSet<StressQuestion> StressQuestions { get; set; }
        public virtual DbSet<StressAnswer> StressAnswers{ get; set; }
        public virtual DbSet<StressTestResult> StressTestResults { get; set; }
        public virtual DbSet<UserGoal> UserGoals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>()
                        .Property(c => c.Category)
                        .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connectionString;

                if (_configuration != null)
                {
                    connectionString = _configuration.GetConnectionString("db_relaxify");
                }
                else
                {
                    // EF CLI çağırdığında burası devreye girer
                    connectionString = "Server=2.59.119.231;Port=5432;Database=db_relaxify;User Id=postgres;Password=mysecretpassword;";
                }

                optionsBuilder.UseNpgsql(connectionString);
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning) // veya Error, Critical
                              .EnableSensitiveDataLogging(false);
            }

            base.OnConfiguring(optionsBuilder);
        }


    }
}
