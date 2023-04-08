using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Models
{
    public class Gallery : BasicModel
    {
        public int Id { get; set; }
        [Display(Name ="النوع")]
        public string Type { get; set; }
        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; }
        [Display(Name = "التاريخ")]
        [NotMapped]
        public string _date { get; set; }
        [Display(Name = "الاسم العربى")]
        public string NameAr { get; set; }
        [Display(Name = "الاسم الانجليزى")]
        public string NameEn { get; set; }
        [Display(Name = "الوصف العربى")]
        public string DescriptionAr { get; set; }
        [Display(Name = "الوصف الانجليزى")]
        public string DescriptionEn { get; set; }
        [Display(Name = "الموقع العربى")]
        public string LocationAr { get; set; }
        [Display(Name = "الموقع الانجليزى")]
        public string LocationEn { get; set; }

        [NotMapped]
        public string FirstImage { get; set; }

        [NotMapped]
        public List<string> Images { get; set; }

    }
}
