using ProjetoFinalAPI.Core.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoFinalAPI.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Título obrigatório")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Data e Hora obrigatórios")]
        [CustomDateRange(ErrorMessage = "Digite uma data atual ou futura")]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Local obrigatório")]
        public string Local { get; set; }

        public string? Address { get; set; }

        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Status obrigatório")]
        public bool Status { get; set; }
    }
}
