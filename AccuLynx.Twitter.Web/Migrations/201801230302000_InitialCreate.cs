namespace AccuLynx.Twitter.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterAnalysis",
                c => new
                    {
                        AnalysisId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AnalysisId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TwitterAnalysis");
        }
    }
}
