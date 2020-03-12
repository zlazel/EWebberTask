using System;

namespace EWebberTask.DAL
{
    public class StudentBook
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public bool Retrived { get; set; }
        public DateTime Date { get; set; }
        public virtual Student Student { get; set; }
        public virtual Book Book { get; set; }
    }

}