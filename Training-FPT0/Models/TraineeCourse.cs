using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Training_FPT0.Models;

namespace Training_FPT0.Models
{
    public class TraineeCourse
    {
        [Key]
        public int Id { get; set; }
        public string TraineeId { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Trainee { get; set; }
        public Course Course { get; set; }
    }
}