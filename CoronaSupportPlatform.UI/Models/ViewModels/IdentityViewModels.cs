using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoronaSupportPlatform.UI.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool Remember { get; set; }
    }

    public class ForgotViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel : BaseViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }

    public class RegisterViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [Display(Name = "Ad")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [Display(Name = "Soyad")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Görev zorunludur.")]
        [Display(Name = "Görev")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Hastane seçimi zorunludur.")]
        [Display(Name = "Hastane")] 
        public int? OrganizationId { get; set; }

        [Required(ErrorMessage = "E-Posta alanı zorunludur.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cep telefonu alanı zorunludur.")]
        [Phone]
        [Display(Name = "Cep Telefonu")] 
        public string Mobile { get; set; }

        [Display(Name = "Disploma Numarası")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "En az 6 karakterden oluşan bir şifre girmelisiniz.")]
        [MinLength(6, ErrorMessage = "En az 6 karakterden oluşan bir şifre girmelisiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }        

        public List<Organization> Organizations { get; set; } = new List<Organization>();

    }

    public class ResetPasswordViewModel : BaseViewModel
    {
        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "En az 6 karakterden oluşan, en az 1 büyük harf, 1 küçük harf ve 1 sayı içeren bir şifre girmelisiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "En az 6 karakterden oluşan, en az 1 büyük harf, 1 küçük harf ve 1 sayı içeren bir şifre girmelisiniz.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }

        public string SecurityStamp { get; set; }
    }

    public class ForgotPasswordViewModel : BaseViewModel
    {
        [Required(ErrorMessage = ("Lütfen e-postanızı giriniz"))]
        [EmailAddress(ErrorMessage = ("E-postanızı doğru girdiğinizden emin olunuz"))]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}