using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalAPI.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "Código do evento obrigatório")]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Quantidade obrigatória")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser positivo")]
        public long Quantity { get; set; }
    }
}
