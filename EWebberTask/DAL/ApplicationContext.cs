using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EWebberTask.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext():base("ApplicationContext") { }
        public DbSet<Author> Authors {get; set;}
        public DbSet<Book> Books {get; set;}
        public DbSet<Student> Students {get; set;}
        public DbSet<StudentBook> StudentBooks {get; set;}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                                       
            //modelBuilder.Entity<Student>()
            // .HasMany(c => c.Books).WithMany(i => i.Students)
            // .Map(t => t.MapLeftKey("StudentId")
            //     .MapRightKey("BookId")
            //     .ToTable("StudentBook"));
            modelBuilder.Entity<StudentBook>()
                .Property(a=>a.Date).HasColumnType("datetime");
        }
    }
}