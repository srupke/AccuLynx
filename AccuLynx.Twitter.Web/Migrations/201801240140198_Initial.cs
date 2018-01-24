namespace AccuLynx.Twitter.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterAnalysis",
                c => new
                    {
                        AnalysisId = c.Int(nullable: false, identity: true),
                        WordOrPhrase1 = c.String(nullable: false),
                        WordOrPhrase2 = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AnalysisId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TwitterAnalysis");
        }
    }
}
