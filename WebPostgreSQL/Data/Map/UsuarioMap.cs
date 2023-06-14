using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebPostgreSQL.Models;

namespace WebPostgreSQL.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<RegistroOrdenha>
    {
        public void Configure(EntityTypeBuilder<RegistroOrdenha> builder)
        {
            throw new NotImplementedException();
        }
    }
}
