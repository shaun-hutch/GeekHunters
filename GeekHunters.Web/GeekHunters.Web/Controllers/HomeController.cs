using GeekHunters.Web.Code;
using GeekHunters.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GeekHunters.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult Candidates()
        {
            DB db = DB.Instance;
            List<Candidate> candidates = db.GetCandidates();

            if (!string.IsNullOrEmpty(candidates.FirstOrDefault().Error))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, candidates.FirstOrDefault().Error);
            }

            return Json(candidates, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Skills()
        {
            DB db = DB.Instance;
            List<Skill> skills = db.GetSkills();

            if (!string.IsNullOrEmpty(skills.FirstOrDefault().Error))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, skills.FirstOrDefault().Error);
            }

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}