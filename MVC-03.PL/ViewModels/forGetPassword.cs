using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
	public class forGetPassword
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
