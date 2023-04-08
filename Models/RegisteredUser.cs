using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class RegisteredUser : BasicModel
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? TrainingId { get; set; }
        [Required]
        [Display(Name ="اسم الشخص")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "البلد")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "رقم الموبايل")]
        public string Mobile{ get; set; }
        [Display(Name = "الوظيفة")]
        public string Position { get; set; }
        [Display(Name = "الواتساب")]
        public string WhatasppMobile { get; set; }

        [NotMapped]
        [Display(Name = "النوع")]
        public string Type { get; set; }
        [NotMapped]
        [Display(Name = "المراد الاشتراك بها")]
        public string CourseName { get; set; }
    }
}