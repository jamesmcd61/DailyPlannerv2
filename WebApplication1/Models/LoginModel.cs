namespace WebApplication1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
