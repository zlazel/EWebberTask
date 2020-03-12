using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using EWebberTask.DAL;
using EWebberTask.ViewModels;

namespace EWebberTask.Controllers
{
    public class StudentBooksController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: StudentBooks
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoadData()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data    
                var studentsBooks = db.StudentBooks
                        .Select(x => new StudentBookVM
                        {
                            Id = x.Id,
                            BookName = x.Book.Name,
                            StudentName = x.Student.Name,
                            Date = x.Date.ToString(),
                            Status = x.Retrived ? "Retrived" : "Borrowed"
                        }).AsQueryable().AsNoTracking();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    studentsBooks = studentsBooks.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    studentsBooks = studentsBooks.Where(m => m.BookName.Contains(searchValue) || m.StudentName.Contains(searchValue));
                }

                //total number of rows count     
                recordsTotal = studentsBooks.Count();
                //Paging     
                var data = studentsBooks.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        //    var studentbooks = db.StudentBooks
        //        .Select(x=> new StudentBookVM 
        //        {
        //            BookName = x.Book.Name,
        //            StudentName = x.Student.Name,
        //            Date = x.Date.ToString(),
        //            Status = x.Retrived? "Retrived" : "Borrowed"
        //        }).ToList();

        //   // return Json(studentbooks);
        //    try
        //    {
        //        var draw = /*RequestContext.Web*/Request.Form["draw"];//.FirstOrDefault();
        //        // Skiping number of Rows count
        //        var start = Request.Form["start"];//.FirstOrDefault();
        //        // Paging Length 10,20
        //        var length = Request.Form["length"];//.FirstOrDefault();
        //        // Sort Column Name
        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"]/*.FirstOrDefault()*/ + "][name]"];//.FirstOrDefault();
        //        // Sort Column Direction ( asc ,desc)
        //        var sortColumnDirection = Request.Form["order[0][dir]"];//.FirstOrDefault();
        //        // Search Value from (Search box)
        //        var searchValue = Request.Form["search[value]"];//.FirstOrDefault();

        //        //Paging Size (10,20,50,100)
        //        int pageSize = length != null ? Convert.ToInt32(length) : 10;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;

        //        //var CurrentUser = _userManager.GetUserId(User);

        //        //int userID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        //var user = db.AppUsers.Include(a => a.AppUserTypes).Where(a => a.Id == userID).FirstOrDefault();

        //        // List<int> schoolList = db.SchoolList.Where(a => a.ManagerId == userID).Select(a => a.Id).ToList();
        //        // Getting first page only 

        //        var result = db.StudentBooks
        //        .Select(x=> new StudentBookVM 
        //        {
        //            BookName = x.Book.Name,
        //            StudentName = x.Student.Name,
        //            Date = x.Date.ToString(),
        //            Status = x.Retrived? "Retrived" : "Borrowed"
        //        })
        //        .Skip(skip).Take(pageSize).AsNoTracking();

        //        //Sorting
        //        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //        {
        //            result = result.OrderBy(sortColumn + " " + sortColumnDirection);
        //        }
        //        //Search
        //        if (!string.IsNullOrEmpty(searchValue))
        //        {
        //            result = (from x in myStages
        //                      where x.Name.Contains(searchValue) || x.School.Contains(searchValue) || x.Head.Contains(searchValue)
        //                      select x).Take(pageSize).AsNoTracking();
        //        }

        //        //total number of rows count 
        //        recordsTotal = myStages.Count();
        //        //Paging 
        //        var data = result.ToList();
        //        //Returning Json Data
        //        return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });

        //    }
        //    catch (Exception exp)
        //    {
        //        return Json(data: false);
        //    }
        //}
        //// GET: StudentBooks/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentBook studentBook = db.StudentBooks.Find(id);
        //    if (studentBook == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studentBook);
        //}

        // GET: StudentBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: StudentBooks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentBook studentBook = db.StudentBooks.Find(id);
        //    if (studentBook == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BookId = new SelectList(db.Books, "Id", "Name", studentBook.BookId);
        //    ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", studentBook.StudentId);
        //    return View(studentBook);
        //}

        //// POST: StudentBooks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,StudentId,BookId,Retrived")] StudentBook studentBook)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(studentBook).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BookId = new SelectList(db.Books, "Id", "Name", studentBook.BookId);
        //    ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", studentBook.StudentId);
        //    return View(studentBook);
        //}

        //// GET: StudentBooks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StudentBook studentBook = db.StudentBooks.Find(id);
        //    if (studentBook == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(studentBook);
        //}

        //// POST: StudentBooks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    StudentBook studentBook = db.StudentBooks.Find(id);
        //    db.StudentBooks.Remove(studentBook);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
