using NpgsqlTypes;
using System.ComponentModel;
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

        [Column("DataOrdenha")]
        [Display(Name = "Data Ordenha")]
        public DateTime DataOrdenha { get; set; }

        [Column("VolumeLeite")]
        [Display(Name = "Volume Leite")]
        public decimal VolumeLeite { get; set; }

        [Display(Name = "Usuário responsável")]
        public int? UsuarioId { get; set; }

        [Display(Name = "Responsável")]
        public Usuario usuario { get; set; }

    }
}
