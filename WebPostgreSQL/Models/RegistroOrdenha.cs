using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPostgreSQL.Models
{
    [Table("RegistroOrdenha")]
    public class RegistroOrdenha
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("Descricao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [Column("DataOrdenha")]
        [Display(Name = "Data Ordenha")]
        public DateTime DataOrdenha { get; set; }

        [Column("VolumeLeite")]
        [Display(Name = "Volume Leite")]
        public decimal VolumeLeite { get; set; }
    }
}
