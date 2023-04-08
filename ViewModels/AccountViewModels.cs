using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
  

    public class LoginViewModel
    {
        [Required(ErrorMessage = "حقل مطلوب")]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "حقل مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Display(Name = "تذكرنى ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "حقل مطلوب")]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "حقل مطلوب")]
        //[Display(Name = "البريد الالكترونى")]
        //[EmailAddress(ErrorMessage = "البريد الالكترونى غير صحيح")]
        //public string Email { get; set; }

        [Required(ErrorMessage = "حقل مطلوب")]
        [StringLength(100, ErrorMessage = "طول كلمة المرور يجب ان يكون 6 او اعلى", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "اعد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمتى المرور غير متطابقة")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "حقل مطلوب")]
        //[Display(Name = "فعال")]
        //public bool IsActive { get; set; }

        
        //[Display(Name = "الصلاحية")]
        //public int? Role { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "حقل مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الحالية")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "حقل مطلوب")]
        [StringLength(100, ErrorMessage = "طول كلمة المرور يجب ان يكون 6 او اعلى")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "اعد كلمة المرور الجديدة")]
        [Compare("NewPassword", ErrorMessage = "كلمتى المرور غير متطابقة")]
        public string ConfirmPassword { get; set; }
    }


}
