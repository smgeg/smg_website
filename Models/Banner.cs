using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Banner : BasicModel
    {
        public int Id { get; set; }

        [Display(Name = "العنوان العربى")]
        [Required]
        public string NameAr { get; set; }

        [Display(Name = "العنوان الانجليزى")]
        [Required]
        public string NameEn { get; set; }

        [Display(Name = "العنوان الفرعى العربى")]
        public string DescriptionAr { get; set; }

        [Display(Name = "العنوان الفرعى الانجليزى")]
        public string DescriptionEn { get; set; }

        [Display(Name = "الصورة")]
        public string Image { get; set; }

    }
}
