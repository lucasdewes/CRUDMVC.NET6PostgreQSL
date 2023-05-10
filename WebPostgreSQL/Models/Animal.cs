using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPostgreSQL.Models
{
    [Table("Animal")]
    public class Animal
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("SISBOV")]
        [Display(Name = "SISBOV")]
        public string SISBOV { get; set; }

        [Column("Nascimento")]
        [Display(Name = "Nascimento")]
        public DateTime Nascimento { get; set; }
    }
}