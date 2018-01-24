using AccuLynx.Twitter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuLynx.Twitter.Dal
{
    public class TwitterContext : DbContext
    {
        public TwitterContext() : base("name=MyDbConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<TwitterContext>());
        }

        public DbSet<TwitterAnalysisModel> Analysis { get; set; }
    }
}
