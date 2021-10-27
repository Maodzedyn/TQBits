using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TQBits.Context;

namespace TQBits.Controllers
{
    public class CandidateController : Controller
    {
        BitsDBEntities dbObj = new BitsDBEntities();
        public ActionResult Candidate(Candidate obj)
        {
            //  if (obj != null)
            return View(obj);
            //  else
            //      return View();
        }

        [HttpPost]
        public ActionResult AddCandidate(Candidate model)
        {
            if (ModelState.IsValid)
            {
                Candidate obj = new Candidate();
                obj.Id = model.Id;
                obj.Name = model.Name;
                obj.DateOfBirth = model.DateOfBirth;
                obj.Married = model.Married;
                obj.Phone = model.Phone;
                obj.Salary = model.Salary;

                if (model.Id == 0)
                {
                    dbObj.Candidates.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                }
            }
            ModelState.Clear();
            return View("Candidate");
        }

        public ActionResult CandidateList()
        {
            var res = dbObj.Candidates.ToList();
            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = dbObj.Candidates.Where(x => x.Id == id).First();
            dbObj.Candidates.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Candidates.ToList();

            return View("CandidateList", list);
        }
    }
}