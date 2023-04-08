using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Website.Models
{
    public class CourseApproval
    {
        public int Id { get; set; }
        [Display(Name = "الاسم العربى")]
        [Required]
        public string NameAr { get; set; }
        [Display(Name = "الاسم الانجليزى")]
        public string NameEn { get; set; }

        [Display(Name = "من تاريخ")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "الى تاريخ")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "من تاريخ")]
        [NotMapped]
        public string _startDate { get; set; }

        [Display(Name = "الى تاريخ")]
        [NotMapped]
        public string _endDate { get; set; }

        [Display(Name = "صورة الاعتماد")]
        public string Image { get; set; }

    }
}
