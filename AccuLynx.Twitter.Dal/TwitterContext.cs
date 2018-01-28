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
    }
}
