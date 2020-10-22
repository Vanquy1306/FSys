using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Training_FPT0.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}