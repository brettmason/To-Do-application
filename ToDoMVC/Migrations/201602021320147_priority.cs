namespace ToDoMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Todoes", "Priority");
        }
    }
}
