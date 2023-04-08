
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Training : BasicModel
    {
        public int Id { get; set; }

        [Display(Name = "نوع قسم دورة التدريب")]
        public int TrainingTypeId { get; set; }
        [NotMapped]
        [Display(Name = "نوع قسم دورة التدريب ")]
        public string TrainingTypeNameAr { get; set; }

        [Display(Name = "كود الدورة التدريبية ")]
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
        [Display(Name = "تفاصيل ومحاور الدورة التدريبية العربى")]
        public string DetailsAr { get; set; }

        //[Required]
        [Display(Name = "تفاصيل ومحاور الدورة التدريبية الانجليزى")]
        public string DetailsEn { get; set; }

        //[Required]
        [Display(Name = "اهداف الدورة التدريبية العربى")]
        public string TargetAr { get; set; }

        //[Required]
        [Display(Name = "اهداف الدورة التدريبية الانجليزى")]
        public string TargetEn { get; set; }

        //[Required]
        [Display(Name = "المحاور التي سيتم دراستها العربى")]
        public string ContentDetailsAr { get; set; }

        //[Required]
        [Display(Name = "المحاور التي سيتم دراستها الانجليزى")]
        public string ContentDetailsEn { get; set; }

        //[Required]
        [Display(Name = "لمن هذا الدورة التدريبية العربى")]
        public string CoursePersonsAr { get; set; }

        //[Required]
        [Display(Name = "لمن هذا الدورة التدريبية  الانجليزى")]
        public string CoursePersonsEn { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "التاريخ")]
        public DateTime? TrainingDate { get; set; }

        [NotMapped]
        [Display(Name = "التاريخ")]
        public string _TrainingDate { get; set; }

        [Display(Name = "الصورة")]
        public string Image { get; set; }

        
    }
    
}