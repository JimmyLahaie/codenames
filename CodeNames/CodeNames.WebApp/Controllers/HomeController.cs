﻿using System.Web.Mvc;

namespace CodeNames.WebApp.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}