using System.Collections.Generic;

namespace EWebberTask.DAL
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books{ get; set; }
    }
}