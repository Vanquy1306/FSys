using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training_FPT0.Models;

namespace Training_FPT0.ViewModels
{
    public class TopicTrainerViewModel
    {
        public Trainer Trainer { get; set; }
        public IEnumerable<Topic> topics { get; set; }
        public IEnumerable<Course> courses { get; set; }

    }
}