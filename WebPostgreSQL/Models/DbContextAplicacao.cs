using Microsoft.EntityFrameworkCore;

namespace WebPostgreSQL.Models
{
    public class DbContextAplicacao : DbContext
    {
        public DbContextAplicacao(DbContextOptions<DbContextAplicacao> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RegistroOrdenha> RegistroOrdenhas { get; set; }
        public DbSet<Animal> Animais { get; set; }
        public DbSet<OrdenhaAnimal> OrdenhaAnimais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela de junção
            modelBuilder.Entity<OrdenhaAnimal>()
                .HasKey(oa => new { oa.OrdenhaId, oa.AnimalId });  // Chave composta

            modelBuilder.Entity<OrdenhaAnimal>()
                .HasOne(oa => oa.Ordenha)
                .WithMany(o => o.OrdenhaAnimais)
                .HasForeignKey(oa => oa.OrdenhaId);

            modelBuilder.Entity<OrdenhaAnimal>()
                .HasOne(oa => oa.Animal)
                .WithMany(a => a.OrdenhaAnimais)
                .HasForeignKey(oa => oa.AnimalId);
        }
    }
}