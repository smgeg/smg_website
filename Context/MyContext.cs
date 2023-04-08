using System.Data.Entity;
using Website.Models;

namespace Website.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobUsers> JobUserss { get; set; }
        public DbSet<AgricultureNetworkUser> AgricultureNetworkUsers { get; set; }
        public DbSet<CourseApproval> CourseApprovals { get; set; }
        public DbSet<CourseApprovalCollection> CourseApprovalCollections { get; set; }
        public DbSet<UserCourseCollection> UserCourseCollections { get; set; }
    }

}