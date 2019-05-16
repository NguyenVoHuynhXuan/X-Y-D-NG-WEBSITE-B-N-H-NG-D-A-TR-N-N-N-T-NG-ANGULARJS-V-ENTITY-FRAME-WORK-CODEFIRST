using System;
using System.ComponentModel.DataAnnotations;

namespace uStora.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        [MaxLength(50, ErrorMessage = "Tên không quá 50 ký tự")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email chưa ký tự không hợp lệ")]
        public string Email { get; set; }
        
        [MaxLength(100, ErrorMessage ="Chỉ nhập 100 ký tự")]
        public string Website { get; set; }

        [MaxLength(20, ErrorMessage = "Chỉ nhập 20 ký tự")]
        public string Phone { get; set; }

        [MaxLength(150, ErrorMessage = "Chỉ nhập 150 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập nội dung tin nhắn")]
        public string Message { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage="Bạn chưa chọn trạng thái gửi phản hồi")]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }

    }
}