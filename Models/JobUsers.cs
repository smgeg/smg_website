using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class JobUsers : BasicModel
    {
        public int Id { get; set; }
        public string CV { get; set; }
        [Display(Name = "اسم الشخص")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "البريد الالكترونى")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "رقم الموبايل")]
        [Required]
        public string Mobile { get; set; }
        public int JobId { get; set; }

        [NotMapped]
        public string JobName { get; set; }

        //[NotMapped]
        //[Required]
        //public string AppliedName { get; set; }

        //[NotMapped]
        //[Required]
        //public string AppliedEmail { get; set; }

        //[NotMapped]
        //[Required]
        //public string AppliedMobile { get; set; }

        [NotMapped]
        public Job Job { get; set; }
    }
}