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

		[HttpGet]
		// GET: TraineeToCourses
		public ActionResult Index(string searchTrainer)
		{
			var trainertopics = _context.TrainerTopics
								   .Include(tr => tr.Topic)
								   .Include(tr => tr.Trainer);

			if (!String.IsNullOrEmpty(searchTrainer))
			{
				trainertopics = trainertopics.Where(
						s => s.Trainer.UserName.Contains(searchTrainer) ||
						s.Trainer.Email.Contains(searchTrainer));
			}

			return View(trainertopics);
		}

		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new TrainerTopicViewModel
			{
				Topics = _context.Topics.ToList(),
				Trainers = _context.Users.ToList()
			};

			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Create(TrainerTopic trainerTopic)
		{
			var newTrainerTopic = new TrainerTopic
			{
				TrainerId = trainerTopic.TrainerId,
				TopicId = trainerTopic.TopicId
			};

			_context.TrainerTopics.Add(newTrainerTopic);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var trainerInDb = _context.TrainerTopics.SingleOrDefault(trainerinDb => trainerinDb.Id == id);
			if (trainerInDb == null)
			{
				return HttpNotFound();
			}

			_context.TrainerTopics.Remove(trainerInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var trainerInDb = _context.TrainerTopics.SingleOrDefault(trainerinDb => trainerinDb.Id == id);

			if (trainerInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TrainerTopicViewModel
			{
				TrainerTopic = trainerInDb,
				Topics = _context.Topics.ToList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(TrainerTopic trainerTopic)
		{
			var trainerInDb = _context.TrainerTopics.SingleOrDefault(trainerinDb => trainerinDb.Id == trainerTopic.Id);

			if (trainerInDb == null)
			{
				return HttpNotFound();
			}

			trainerInDb.TopicId = trainerTopic.TopicId;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}