using System.Collections.Generic;

namespace EWebberTask.DAL
{
    public class Student
    {
        public Student()
        {
            StudentBooks = new HashSet<StudentBook>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<StudentBook> StudentBooks { get; set; }
    }
}

