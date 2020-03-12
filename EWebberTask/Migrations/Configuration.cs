namespace EWebberTask.Migrations
{
    using EWebberTask.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EWebberTask.DAL.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EWebberTask.DAL.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.
            var Students = new List<Student>()
            {
                new Student
                {
                    Name = "Ahmed",
                    StudentBooks = new List<StudentBook>
                    {
                        new StudentBook{BookId = 1},
                        new StudentBook{BookId = 2},
                    }
                },
                new Student { Name = "Omar" ,
                    StudentBooks = new List<StudentBook>
                    {
                        new StudentBook{BookId = 3},
                        new StudentBook{BookId = 1},
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



            try
            {
                Students.ForEach(s => context.Students.AddOrUpdate(s));
                Authors.ForEach(a => context.Authors.AddOrUpdate(a));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
