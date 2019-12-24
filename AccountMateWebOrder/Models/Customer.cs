using System.ComponentModel.DataAnnotations;

namespace AccountMateWebOrder.Models
{
    public class Customer
    {
        
        [Display(Name = "Customer Code")]
        [Required(ErrorMessage = "Username is required")]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}