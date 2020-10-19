namespace Training_FPT0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPD : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "CourseCategoryId", "dbo.CourseCategories");
            DropForeignKey("dbo.Enrollments", "TraineeId", "dbo.Trainees");
            DropIndex("dbo.Enrollments", new[] { "TraineeId" });
            DropIndex("dbo.Enrollments", new[] { "CourseCategoryId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropTable("dbo.Enrollments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.Int(nullable: false),
                        CourseCategoryId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Enrollments", "CourseId");
            CreateIndex("dbo.Enrollments", "CourseCategoryId");
            CreateIndex("dbo.Enrollments", "TraineeId");
            AddForeignKey("dbo.Enrollments", "TraineeId", "dbo.Trainees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Enrollments", "CourseCategoryId", "dbo.CourseCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
