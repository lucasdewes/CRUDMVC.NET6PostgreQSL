using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WebPostgreSQL.Models
{
    [Table("VMLogin")]
    public class VMLogin
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        
        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set;}

        [Column("PassWord")]
        [Display(Name = "PassWord")]
        public string PassWord { get; set; }
       
        [Column("KeepLoggedIn")]
        [Display(Name = "KeepLoggedIn")]
        public bool KeepLoggedIn { get; set; }
    }
}