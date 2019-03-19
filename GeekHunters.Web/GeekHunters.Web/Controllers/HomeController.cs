using GeekHunters.Web.Code;
using GeekHunters.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekHunters.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DB db = DB.Instance;
            List<Candidate> candidates = db.GetCandidates();



            return View("Home");
        }
    }
}