using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Can Not Be null")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]

        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(dataType: DataType.Password)]

        public string Password { get; set; }
        [DataType(dataType: DataType.Password)]
        [Compare(otherProperty: nameof(Password))]

        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
