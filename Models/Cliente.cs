using System.ComponentModel.DataAnnotations;

namespace DesafioThomasGreg.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [MaxLength(50, ErrorMessage = "Este campo deve conter entre 3 e 50 caracteres.")]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres.")]
        public string LogoTipo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Logradouro Inválido.")]
        public int LogradouroId { get; set; }
        public Logradouro Logradouro {get; set;}


    }
}