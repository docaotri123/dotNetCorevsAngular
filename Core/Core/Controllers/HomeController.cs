using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Core.ViewModels;
using Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        //private readonly CoreContext _context;
        private readonly ICoreRepository _repository;

        public HomeController(IMailService mailService,ICoreRepository repository)
        {
            this._mailService = mailService;
            //this._context = context;
            this._repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost("about")]
        public IActionResult About(object model)
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //send the email
                _mailService.SendMessage("docaotri211997@gmail.com",model.Subject,$"From:{model.Email},Message:{model.Message}");
                ViewBag.UserMessage = "Mail send";
                ModelState.Clear();
            }
        
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            return View(results);
        }

      
    }
}
