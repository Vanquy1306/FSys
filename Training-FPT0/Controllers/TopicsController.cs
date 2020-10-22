using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Training_FPT0.Models;
using Training_FPT0.ViewModels;

namespace Training_FPT0.Controllers
{
    public class TopicsController : Controller
    {
		private ApplicationDbContext _context;
		public TopicsController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Course
		[HttpGet]
		public ActionResult Index(string searchString)
		{
			var topics = _context.Topics.Include(p => p.Course);
			if (!String.IsNullOrEmpty(searchString))
			{
				topics = topics.Where(
					s => s.Name.Contains(searchString) ||
					s.Course.Name.Contains(searchString));

			}
			return View(topics.ToList());
		}

		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new TopicCourseViewModel
			{
				Courses = _context.Courses.ToList(),
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(Topic topic)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (_context.Topics.Any(p => p.Name.Contains(topic.Name)))
			{
				ModelState.AddModelError("Name", "Topic Name Already Exists.");
				return View();
			}

			var newTopic = new Topic
			{
				Name = topic.Name,
				Description = topic.Description,
				CourseId = topic.CourseId,


			};

			_context.Topics.Add(newTopic);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(p => p.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			_context.Topics.Remove(topicInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(p => p.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}
			var viewModel = new TopicCourseViewModel
			{
				Topic = topicInDb,
				Courses = _context.Courses.ToList(),
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(Topic topic)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var topicInDb = _context.Topics.SingleOrDefault(p => p.Id == topic.Id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			topicInDb.Name = topicInDb.Name;
			topicInDb.Description = topicInDb.Description;
			topicInDb.CourseId = topicInDb.CourseId;


			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		// GET: Topics/Details/5
		public ActionResult Details(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(p => p.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			return View(topicInDb);
		}
	
	}
}