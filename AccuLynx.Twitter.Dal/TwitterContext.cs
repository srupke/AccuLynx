using AccuLynx.Twitter.Models;
using System.Data.Entity;

namespace AccuLynx.Twitter.Dal
{
    public class TwitterContext : DbContext
    {
        public TwitterContext() : base("name=MyDbConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TwitterContext>());

            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<TwitterAnalysisModel> Analysis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Some database configuration
            modelBuilder.Entity<TwitterAnalysisPhraseModel>()
                  .Ignore(i => i.TotalTweets);
            modelBuilder.Entity<TwitterAnalysisPhraseModel>()
                 .Ignore(i => i.TotalRetweetCount);
            modelBuilder.Entity<TwitterAnalysisPhraseModel>()
                 .Ignore(i => i.TotalFavoriteCount);
        }
    }
}
