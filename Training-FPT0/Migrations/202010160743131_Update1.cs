namespace Training_FPT0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainers", "topId_Id", "dbo.Topics");
            DropForeignKey("dbo.Trainers", "Topic_Id", "dbo.Topics");
            DropIndex("dbo.Trainers", new[] { "Topic_Id" });
            DropIndex("dbo.Trainers", new[] { "topId_Id" });
            RenameColumn(table: "dbo.Trainers", name: "Topic_Id", newName: "TopicId");
            AlterColumn("dbo.Trainers", "TopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trainers", "TopicId");
            AddForeignKey("dbo.Trainers", "TopicId", "dbo.Topics", "Id", cascadeDelete: true);
            DropColumn("dbo.Trainers", "topId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainers", "topId_Id", c => c.Int());
            DropForeignKey("dbo.Trainers", "TopicId", "dbo.Topics");
            DropIndex("dbo.Trainers", new[] { "TopicId" });
            AlterColumn("dbo.Trainers", "TopicId", c => c.Int());
            RenameColumn(table: "dbo.Trainers", name: "TopicId", newName: "Topic_Id");
            CreateIndex("dbo.Trainers", "topId_Id");
            CreateIndex("dbo.Trainers", "Topic_Id");
            AddForeignKey("dbo.Trainers", "Topic_Id", "dbo.Topics", "Id");
            AddForeignKey("dbo.Trainers", "topId_Id", "dbo.Topics", "Id");
        }
    }
}
