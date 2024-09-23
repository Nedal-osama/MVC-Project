using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required]
		public string FName { get; set; }
		[Required]
		public string LName { get; set; }
		[Required]
		[EmailAddress]
        public  string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		public string ConfirmPassenger { get; set; }
		[Required]
		public bool IsAgree { get; set; }
		
    }
}
