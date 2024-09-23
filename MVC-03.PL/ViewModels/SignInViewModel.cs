using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
	public class SignInViewModel
	{
		[Required]
		[EmailAddress]
        public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
    }
}
