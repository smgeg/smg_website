using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class CourseType : BasicModel
    {
        public int Id { get; set; }
        [Display(Name = "الاسم العربى")]
        [Required]
        public string NameAr { get; set; }
        [Display(Name = "الاسم الانجليزى")]
        [Required]
        public string NameEn { get; set; }
        [Display(Name = "الوصف العربى")]
        public string DescriptionAr { get; set; }
        [Display(Name = "الوصف الانجليزى")]
        public string DescriptionEn { get; set; }
        [Display(Name = "مفعل")]
        public bool IsEnable { get; set; }
    }
}