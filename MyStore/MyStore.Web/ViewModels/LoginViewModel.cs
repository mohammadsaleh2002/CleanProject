using System.ComponentModel.DataAnnotations; 

namespace MyStore.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ایمیل اجباری است")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}