using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebPostgreSQL.Models
{
    public class ApontamentoOrdenha
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("CodAnimal")]
        [ForeignKey("CodAnimal")]
        [Display(Name = "Código Animal")]
        public int CodAnimal { get; set; }
    }
}
