using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogMVC.Models
{
    public enum role
    {
        admin,
        customer
    }
    public class User
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        public string? Password { get; set; }
        [NotMapped]
        [Compare("Password",ErrorMessage ="Confirm password doesn't match, please try again!")]
        [DisplayName("Confirm Password")]
        [Required(ErrorMessage ="*")]
        public string? confirmPassword { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Name { get; set; }
        public role Role{ get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; }
    }
}
