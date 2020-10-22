namespace Training_FPT0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCode : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TopicId", "dbo.Topics");
            DropIndex("dbo.Courses", new[] { "TopicId" });
            AddColumn("dbo.Topics", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "CourseId");
            AddForeignKey("dbo.Topics", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            DropColumn("dbo.Courses", "TopicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "TopicId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Topics", "CourseId", "dbo.Courses");
            DropIndex("dbo.Topics", new[] { "CourseId" });
            DropColumn("dbo.Topics", "CourseId");
            CreateIndex("dbo.Courses", "TopicId");
            AddForeignKey("dbo.Courses", "TopicId", "dbo.Topics", "Id", cascadeDelete: true);
        }
    }
}
