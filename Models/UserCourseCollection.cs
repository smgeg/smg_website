using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Models
{
    public class UserCourseCollection
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
    }
}
