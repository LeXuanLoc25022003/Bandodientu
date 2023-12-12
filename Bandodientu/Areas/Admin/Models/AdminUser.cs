using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bandodientu.Areas.Admin.Models
{
    [Table("AdminUser")]
    public class AdminUser
    {
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? About {  get; set; }
        public string? Image { get; set; }
        public string? Job { get; set; }
        public string? Country { get; set; }
        public string? Company {  get; set; }
        public string? Address { get; set; }
        public string? Phone {  get; set; }
        public bool? IsActive { get; set; }
    }
}
