using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyShop.Models.Req.Account
{
    public class MemberRegisterReq : IValidatableObject
    {
        [Required(ErrorMessage = "登入帳號必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "登入帳號長度需介於 2~20")]
        [RegularExpression(@"^[A-Za-z0-9._-]+$", ErrorMessage = "登入帳號僅允許英數字與 . _ -")]
        public string Username { get; set; }

        [Required(ErrorMessage = "密碼必填")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密碼至少 6 碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請再次輸入密碼")]
        [Compare(nameof(Password), ErrorMessage = "兩次密碼不一致")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email 格式不正確")]
        [StringLength(254)]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "手機格式不正確")]
        [StringLength(20)]
        public string? Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1) 至少要有 Email 或 Phone 其中之一
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Phone))
            {
                yield return new ValidationResult(
                    "Email 或手機至少需填一項。",
                    new[] { nameof(Email), nameof(Phone) }
                );
            }
        }
    }
}
