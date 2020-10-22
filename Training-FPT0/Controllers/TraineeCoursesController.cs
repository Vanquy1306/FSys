using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_FPT0.Models;
using System.Data.Entity;
using Training_FPT0.ViewModels;

namespace Training_FPT0.Controllers
{
    public class TraineeCoursesController : Controller
    {

		private ApplicationDbContext _context;
		public TraineeCoursesController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		// GET: TraineeToCourses
		public ActionResult Index(string searchTrainee)
		{
			var traineecourses = _context.TraineeCourses
								   .Include(tr => tr.Course)
								   .Include(tr => tr.Trainee);

			if (!String.IsNullOrEmpty(searchTrainee))
			{
				traineecourses = traineecourses.Where(
						s => s.Trainee.UserName.Contains(searchTrainee) ||
						s.Trainee.Email.Contains(searchTrainee));
			}

			return View(traineecourses);
		}

		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new TraineeCourseViewModel
			{
				Courses = _context.Courses.ToList(),
				Trainees = _context.Users.ToList()
			};

			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Create(TraineeCourse traineeCourse)
		{
			var newTraineeCourse = new TraineeCourse
			{
				TraineeId = traineeCourse.TraineeId,
				CourseId = traineeCourse.CourseId
			};

			_context.TraineeCourses.Add(newTraineeCourse);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var traineeInDb = _context.TraineeCourses.SingleOrDefault(traineeindb => traineeindb.Id == id);
			if (traineeInDb == null)
			{
				return HttpNotFound();
			}

			_context.TraineeCourses.Remove(traineeInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var traineeInDb = _context.TraineeCourses.SingleOrDefault(traineeindb => traineeindb.Id == id);

			if (traineeInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TraineeCourseViewModel
			{
				TraineeCourse = traineeInDb,
				Courses = _context.Courses.ToList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(TraineeCourse traineeCourse)
		{
			var traineeInDb = _context.TraineeCourses.SingleOrDefault(traineeindb => traineeindb.Id == traineeCourse.Id);

			if (traineeInDb == null)
			{
				return HttpNotFound();
			}

			traineeInDb.CourseId = traineeCourse.CourseId;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}