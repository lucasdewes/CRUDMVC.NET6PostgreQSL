using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPostgreSQL.Models
{
    public class Usuario
    {

        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Column("Email")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Column("PassWord")]
        [Display(Name = "PassWord")]
        public string PassWord { get; set; }
    }
}
