namespace WebApplication1.DataModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDataModel
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
