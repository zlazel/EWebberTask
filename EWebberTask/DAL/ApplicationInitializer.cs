using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EWebberTask.DAL
{
    public class ApplicationInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var Students = new List<Student>()
            {
                new Student
                {
                    Name = "Ahmed",
                    StudentBooks = new List<StudentBook>
                    {
                        new StudentBook{BookId = 1 , Date = DateTime.Now},
                        new StudentBook{BookId = 2, Date = DateTime.Now},
                    }
                },
                new Student { Name = "Omar" ,
                    StudentBooks = new List<StudentBook>
                    {
                        new StudentBook{BookId = 3, Date = DateTime.Now},
                        new StudentBook{BookId = 1, Date = DateTime.Now},
                    }},
                new Student { Name = "Ali" },
            };

            var Authors = new List<Author>()
            {
                new Author
                {
                    Name = "Author 1",
                    Books = new List<Book>
                    {
                        new Book { Name= "Book 1" , ISBN = "1234" , Count = 5},
                        new Book { Name= "Book 2" , ISBN = "1235" , Count = 3},
                    }
                },
                new Author
                {
                    Name = "Author 2",
                    Books = new List<Book>
                    {
                        new Book { Name= "Book 4" , ISBN = "4444" , Count = 4},
                        new Book { Name= "Book 5" , ISBN = "5555" , Count = 5},
                    }
                },

            };

            context.Students.AddRange(Students);
            context.Authors.AddRange(Authors);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}