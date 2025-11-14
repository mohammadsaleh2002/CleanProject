using System.ComponentModel.DataAnnotations;

namespace MyStore.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "ایمیل اجباری است")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور اجباری است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }
    }
}