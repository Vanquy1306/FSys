using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_FPT0.Models;
using Training_FPT0.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Training_FPT0.Controllers
{
    public class TrainerTopicsController : Controller
    {
        private ApplicationDbContext _context;

        public TrainerTopicsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("TrainingStaff"))
            {
                var trainertopics = _context.TrainerTopics.Include(t => t.Topic).Include(t => t.Trainer).ToList();
                return View(trainertopics);
            }
            if (User.IsInRole("Trainer"))
            {
                var trainerId = User.Identity.GetUserId();
                var Res = _context.TrainerTopics.Where(e => e.TrainerId == trainerId).Include(t => t.Topic).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Create()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();

            var TrainerTopicVM = new TrainerTopicViewModel()
            {
                Topics = topics,
                Trainers = users,
                TrainerTopic = new TrainerTopic()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        public ActionResult Create(TrainerTopicViewModel model)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();


            if (ModelState.IsValid)
            {
                _context.TrainerTopics.Add(model.TrainerTopic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new TrainerTopicViewModel()
            {
                Topics = topics,
                Trainers = users,
                TrainerTopic = new TrainerTopic()
            };

            return View(TrainerTopicVM);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var appUser = _context.TrainerTopics.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(TrainerTopic trainerTopic)
        {
            var trainertopicInDb = _context.TrainerTopics.Find(trainerTopic.Id);

            if (trainertopicInDb == null)
            {
                return View(trainerTopic);
            }

            if (ModelState.IsValid)
            {
                trainertopicInDb.TrainerId = trainerTopic.TrainerId;
                trainertopicInDb.TopicId = trainerTopic.TopicId;

                _context.TrainerTopics.AddOrUpdate(trainerTopic);
                _context.SaveChanges();

                return RedirectToAction("Index", "TrainerTopics");
            }
            return View(trainerTopic);

        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(int id)
        {
            var trainertopicInDb = _context.TrainerTopics.SingleOrDefault(p => p.Id == id);

            if (trainertopicInDb == null)
            {
                return HttpNotFound();
            }
            _context.TrainerTopics.Remove(trainertopicInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "TrainerTopics");

        }
    }
}