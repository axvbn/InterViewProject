﻿using InterViewProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterViewProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Index_Query()
        {
            M_Index MI = new M_Index();
            string strJson = MI.Query();
           

            return Json(strJson);
        }

        public string Index_Insert(string jData)
        {
            string result = string.Empty;
            M_Index MI = new M_Index();
            Categories myCategories = JsonConvert.DeserializeObject<Categories>(jData);
            result = MI.Insert(myCategories);

            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}