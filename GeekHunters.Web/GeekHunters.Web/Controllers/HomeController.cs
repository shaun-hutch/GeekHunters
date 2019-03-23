using GeekHunters.Web.Code;
using GeekHunters.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web;

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
            DB db = DB.Instance(Global.dbPath);
            List<Candidate> candidates = db.GetCandidates();

            if (!string.IsNullOrEmpty(candidates.FirstOrDefault().Error))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, candidates.FirstOrDefault().Error);
            }

            return Json(candidates, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Skills()
        {
            DB db = DB.Instance(Global.dbPath);
            List<Skill> skills = db.GetSkills();

            if (!string.IsNullOrEmpty(skills.FirstOrDefault().Error))
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, skills.FirstOrDefault().Error);
            }

            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public HttpStatusCodeResult AddCandidate(string firstName, string lastName, string skillList)
        {
            try
            {
                DB db = DB.Instance(Global.dbPath);
                int newCandidateId = db.AddCandidate(firstName, lastName, skillList);

                if (newCandidateId > 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK, newCandidateId.ToString());
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Candidate unable to be inserted into DB.");
                }

            }
            catch (SQLiteException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}