namespace WebPostgreSQL.Models
{
    public class RegistroOrdenhaModel : RegistroOrdenha
    {
        public List<Usuario>? ListaUsuarios { get; set; }

        // Adicionar uma lista de animais para seleção na View
        public List<Animal>? ListaAnimais { get; set; }

        // Propriedade para capturar os IDs dos animais selecionados
        public List<int>? AnimaisSelecionadosIds { get; set; }
    }
}