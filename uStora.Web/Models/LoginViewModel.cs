using System.ComponentModel.DataAnnotations;

namespace uStora.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Bạn cần nhập tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Ghi nhớ")]
        public bool RememberMe { get; set; }
    }
}