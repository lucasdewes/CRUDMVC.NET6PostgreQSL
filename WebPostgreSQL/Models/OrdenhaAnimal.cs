namespace WebPostgreSQL.Models
{
    public class OrdenhaAnimal
    {
        public int OrdenhaId { get; set; }
        public RegistroOrdenha Ordenha { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}