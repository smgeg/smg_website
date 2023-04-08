using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Course : BasicModel
    {
        public int Id { get; set; }

        [Display(Name = "نوع الكورس  ")]
        public int CourseTypeId { get; set; }
        [NotMapped]
        [Display(Name = "نوع الكورس  ")]
        public string CourseTypeNameAr { get; set; }

        [Display(Name = "كود الكورس  ")]
        public string Code { get; set; }

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

        [Display(Name = "السعر")]
        [Required]
        public int Price { get; set; }

        [Required]
        [Display(Name = "عدد الساعات")]
        public int HourlyDuration { get; set; }

        [Required]
        [Display(Name = "عدد المحاضرات")]
        public int LecturesCount { get; set; }

        [Required]
        [Display(Name = "المكان")]
        public string Location { get; set; }

        //[Required]
        [Display(Name = "تفاصيل ومحاور الكورس  العربى")]
        public string DetailsAr { get; set; }

        //[Required]
        [Display(Name = "تفاصيل ومحاور الكورس  الانجليزى")]
        public string DetailsEn { get; set; }

        //[Required]
        [Display(Name = "اهداف الكورس  العربى")]
        public string TargetAr { get; set; }

        //[Required]
        [Display(Name = "اهداف الكورس  الانجليزى")]
        public string TargetEn { get; set; }

        //[Required]
        [Display(Name = "المحاور التي سيتم دراستها العربى")]
        public string ContentDetailsAr { get; set; }

        //[Required]
        [Display(Name = "المحاور التي سيتم دراستها الانجليزى")]
        public string ContentDetailsEn { get; set; }

        //[Required]
        [Display(Name = "لمن هذه الكورس  العربى")]
        public string CoursePersonsAr { get; set; }

        //[Required]
        [Display(Name = "لمن هذه الكورس   الانجليزى")]
        public string CoursePersonsEn { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "التاريخ")]
        public DateTime? CourseDate { get; set; }

        [NotMapped]
        [Display(Name = "التاريخ")]
        public string _CourseDate { get; set; }

        [Display(Name = "الصورة")]
        public string Image { get; set; }

        [Display(Name = "مفعل")]
        public bool IsEnable { get; set; }

        [NotMapped]
        public List<string> courseApprovals { get; set; } = new List<string>();

        [NotMapped]
        public List<CourseApproval> courseApprovalsWithImage { get; set; } = new List<CourseApproval>();

    }
    
}