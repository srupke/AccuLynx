namespace AccuLynx.Twitter.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AccuLynx.Twitter.Web.Models.TwitterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AccuLynx.Twitter.Web.Models.TwitterContext";
        }

        protected override void Seed(AccuLynx.Twitter.Web.Models.TwitterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Analysis.AddOrUpdate(new Models.TwitterAnalysis() { WordOrPhrase1 = "Goonies", WordOrPhrase2 = "It's our time" });
        }
    }
}
