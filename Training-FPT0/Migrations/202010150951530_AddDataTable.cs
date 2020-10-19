namespace Training_FPT0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.Int(nullable: false),
                        CourseCategoryId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.CourseCategories", t => t.CourseCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Trainees", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseCategoryId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Email = c.String(),
                        FullName = c.String(),
                        Phone = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        WorkingPlace = c.String(),
                        PhoneNumber = c.String(),
                        Topic_Id = c.Int(),
                        topId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.Topic_Id)
                .ForeignKey("dbo.Topics", t => t.topId_Id)
                .Index(t => t.Topic_Id)
                .Index(t => t.topId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "topId_Id", "dbo.Topics");
            DropForeignKey("dbo.Trainers", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.Enrollments", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "CourseCategoryId", "dbo.CourseCategories");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Trainers", new[] { "topId_Id" });
            DropIndex("dbo.Trainers", new[] { "Topic_Id" });
            DropIndex("dbo.Trainees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Enrollments", new[] { "CourseCategoryId" });
            DropIndex("dbo.Enrollments", new[] { "TraineeId" });
            DropIndex("dbo.Courses", new[] { "TopicId" });
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            DropTable("dbo.Trainers");
            DropTable("dbo.Trainees");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Topics");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseCategories");
            DropTable("dbo.Categories");
        }
    }
}
