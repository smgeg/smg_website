using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Job : BasicModel
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

        [Display(Name = "الوصف المختصرالعربى")]
        public string ShortDescriptionAr { get; set; }

        [Display(Name = "الوصف المختصر الانجليزى")]
        public string ShortDescriptionEn { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        public DateTime? ExpirationDate { get; set; }

        [NotMapped]
        [Display(Name = "تاريخ الانتهاء")]
        public string _ExpirationDate { get; set; }

        public bool IsActive { get; set; }


    }
}