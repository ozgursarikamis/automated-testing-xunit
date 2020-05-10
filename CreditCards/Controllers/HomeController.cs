﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace CreditCards.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(NoStore = true)]
        public IActionResult Index()
        {
            string[] randomGreetings = { "Hi", "Hey", "Yo" };

            var rndGreetingIndex = new Random().Next(0, randomGreetings.Length);

            ViewData["RandomGreeting"] = randomGreetings[rndGreetingIndex];

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Demo application for Pluralsight course.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            throw new NotImplementedException();
        }
    }
}
