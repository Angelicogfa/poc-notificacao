using System.ComponentModel.DataAnnotations;

namespace NotificacaoAPI.Requests
{
    public class AutenticationRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "O usuário deve ter pelo menos 5 caracteres")]
        public string UserName { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "O senha deve ter pelo menos 8 caracteres")]
        public string Password { get; set; }
    }
}
