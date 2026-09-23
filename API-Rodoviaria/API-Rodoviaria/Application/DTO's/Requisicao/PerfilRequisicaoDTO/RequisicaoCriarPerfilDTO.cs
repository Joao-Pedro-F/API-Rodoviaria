namespace API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil;
using System.ComponentModel.DataAnnotations;

    public class RequisicaoCriarPerfilDTO
    {
        
        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Cargo deve ter no máximo 100 caracteres.")]
        public string Cargo { get; set; }

    }

