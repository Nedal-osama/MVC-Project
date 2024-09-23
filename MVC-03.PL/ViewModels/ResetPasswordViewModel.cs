using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		public string confirmPassworded { get; set; }
	}
}
