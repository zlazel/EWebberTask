using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EWebberTask.DAL
{
    public class Student
    {
        public Student()
        {
            StudentBooks = new HashSet<StudentBook>();
        }
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        public virtual ICollection<StudentBook> StudentBooks { get; set; }
    }
}

