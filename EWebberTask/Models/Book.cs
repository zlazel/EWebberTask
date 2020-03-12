using System.Collections.Generic;

namespace EWebberTask.DAL
{
    public class Book
    {
        public Book()
        {
            StudentBooks = new HashSet<StudentBook>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public int Count { get; set; }
        public virtual Author Author { get; set; }
        public ICollection<StudentBook> StudentBooks { get; set; }

    }

}