namespace ToDoMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullpriority : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Todoes", "Priority", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Todoes", "Priority", c => c.Int(nullable: false));
        }
    }
}
