namespace EntityGames.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changebase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "GameMode", c => c.String());
            AddColumn("dbo.Games", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Count");
            DropColumn("dbo.Games", "GameMode");
        }
    }
}
