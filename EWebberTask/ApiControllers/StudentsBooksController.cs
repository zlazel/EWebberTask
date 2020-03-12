using EWebberTask.DAL;
using EWebberTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace EWebberTask.ApiControllers
{
    public class StudentsBooksController : System.Web.Http.ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        // GET api/StudentsBooks/GetBooksNames
        //[HttpGet]
        //[Route("GetBooksNames")]
        public JsonResult<List<BookVM>> GetBooksNames(string term = "")
        {
            var booksNames = db.Books
                .Where(x => x.Name.Contains(term.Trim()))
                .Select(x => new BookVM { Id = x.Id, Name = x.Name, ISBN = x.ISBN }).ToList();
            return Json(booksNames);
        }
        // GET api/StudentsBooks/RetriveBook/1
        [HttpPost]
        public JsonResult<Result> RetriveBook(int Id)
        {
            try
            {
                var studentbook = db.StudentBooks.Find(Id);
                if (studentbook != null)
                {
                    studentbook.Retrived = true;
                    int res = db.SaveChanges();
                    return Json(new Result { Success = true, Messages = "Book Retrived Successfully" });
                }
                else
                {
                    return Json(new Result { Success = false, Messages = "Not Found Record" });
                }
            }
            catch (Exception e)
            {
                return Json(new Result { Success = false, Messages = $"Error Occured {e.Message}" });
            }
        }
        public JsonResult<List<StudentBookVM>> LoadData()
        {
            var r = Request;
            var c = RequestContext;
            var studentbooks = db.StudentBooks
                .Select(x => new StudentBookVM
                {
                    BookName = x.Book.Name,
                    StudentName = x.Student.Name,
                    Date = x.Date.ToString(),
                    Status = x.Retrived ? "Retrived" : "Borrowed"
                }).ToList();

            return Json(studentbooks);
            // try
            // {
            // var draw = RequestContext.WebRequest.Form["draw"].FirstOrDefault();
            //    // Skiping number of Rows count
            //    var start = Request.Form["start"].FirstOrDefault();
            //    // Paging Length 10,20
            //    var length = Request.Form["length"].FirstOrDefault();
            //    // Sort Column Name
            //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            //    // Sort Column Direction ( asc ,desc)
            //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            //    // Search Value from (Search box)
            //    var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //    //Paging Size (10,20,50,100)
            //    int pageSize = length != null ? Convert.ToInt32(length) : 10;
            //    int skip = start != null ? Convert.ToInt32(start) : 0;
            //    int recordsTotal = 0;

            //    //var CurrentUser = _userManager.GetUserId(User);

            //    //int userID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //    //var user = db.AppUsers.Include(a => a.AppUserTypes).Where(a => a.Id == userID).FirstOrDefault();

            //    // List<int> schoolList = db.SchoolList.Where(a => a.ManagerId == userID).Select(a => a.Id).ToList();
            //    // Getting first page only 
            //    var myStages = (from x in db.StageList where x.SchoolId == SiteSettings.SchoolId select x);
            //    var result = myStages.Skip(skip).Take(pageSize).AsNoTracking();

            //    //Sorting
            //    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //    {
            //        result = result.OrderBy(sortColumn + " " + sortColumnDirection);
            //    }
            //    //Search
            //    if (!string.IsNullOrEmpty(searchValue))
            //    {
            //        result = (from x in myStages
            //                  where x.Name.Contains(searchValue) || x.School.Contains(searchValue) || x.Head.Contains(searchValue)
            //                  select x).Take(pageSize).AsNoTracking();
            //    }

            //    //total number of rows count 
            //    recordsTotal = myStages.Count();
            //    //Paging 
            //    var data = result.ToList();
            //    //Returning Json Data
            //    return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });

            //}
            //catch (Exception exp)
            //{
            //    _logger.LogError(exp.Message, "Load List Faild");
            //    return Json(data: false);
            //}
        }

        // GET api/StudentsBooks/GetStudentsNames
        //[HttpGet]
        //[Route("GetStudentsNames")]
        public JsonResult<List<StudentVM>> GetStudentsNames(string term = "")
        {
            var studentsNames = db.Students
                .Where(x => x.Name.Contains(term.Trim()))
                .Select(x => new StudentVM { Id = x.Id, Name = x.Name }).ToList();
            return Json(studentsNames);
        }

        // POST: StudentBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public JsonResult<Result> Save([Bind(Include = "StudentId,BookId,Retrived")] StudentBook studentBook)
        {
            try
            {
                // in case of retrive borrowed book must find record in database and change it's Retrived
                if (studentBook.Retrived)
                {
                    return StudentRetriveBook(studentBook);
                }
                else
                {
                    return StudentBorrowBook(studentBook);
                }
            }
            catch (Exception e)
            {
                return Json(new Result { Success = false, Messages = e.Message });
            }

        }

        private JsonResult<Result> StudentRetriveBook(StudentBook studentBook)
        {
            var oldstudentbook = db.StudentBooks
                  .Where(x => x.StudentId == studentBook.StudentId
                      && x.BookId == studentBook.BookId
                      && x.Retrived == false)
                  .FirstOrDefault();

            if (oldstudentbook != null)
            {
                oldstudentbook.Retrived = true;
                db.SaveChanges();
                return Json(new Result { Success = true, Messages = $"Student: {oldstudentbook.Student.Name} Retrived Book: {oldstudentbook.Book.Name} Successfully" });
            }
            else
            {
                return Json(new Result { Success = false, Messages = $"This student does not borrowed this book before Or borrowed but retrived" });
            }
        }

        private JsonResult<Result> StudentBorrowBook(StudentBook studentBook)
        {
            var oldstudentbook = db.StudentBooks
                  .Where(x => x.StudentId == studentBook.StudentId
                      && x.BookId == studentBook.BookId
                      && x.Retrived == false)
                  .FirstOrDefault();
            if (oldstudentbook == null)
            {
                var book = db.Books.Where(x => x.Id == studentBook.BookId).FirstOrDefault();
                if (book == null)
                {
                    return Json(new Result { Success = false, Messages = $"Book Not Found" });
                }

                // calculate available books 
                int borrowedBooksCount = db.StudentBooks
                    .Where(s => s.BookId == studentBook.BookId && !s.Retrived)
                    .Count();
                int availableBooksCount = book.Count - borrowedBooksCount;
                // if there available books
                if (availableBooksCount > 0)
                {
                    studentBook.Date =  DateTime.Now;
                    db.StudentBooks.Add(studentBook);
                    db.SaveChanges();
                    return Json(new Result { Success = true, Messages = $"Student Borrowed Book Successfully" });
                }
                else
                {
                    return Json(new Result { Success = false, Messages = $"Sorry This Book Not Available Now" });
                }
            }
            else
            {
                return Json(new Result { Success = false, Messages = $"This Student Borrowed this Book before and Must Retrive it first" });
            }
        }
    }
}
