using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class AgricultureNetworkUser : BasicModel
    {
        public int Id { get; set; }
        [Display(Name = "الاسم العربى")]
        public string NameAr { get; set; }
        [Display(Name = "الاسم الانجليزى")]
        public string NameEn { get; set; }
        [Display(Name = "الموبايل")]
        public string MobileNo { get; set; }
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }
        [Display(Name = "الصورة")]
        public string Image { get; set; }
        [Display(Name = "اسم المستخدم")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "كلمة المرور")]
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "نوع المستخدم")]
        public string UserType { get; set; }

        [NotMapped]
        public List<string> userCourses { get; set; } = new List<string>();
    }
}