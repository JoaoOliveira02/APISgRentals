using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GerenciadorDeEmpresas.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(14, ErrorMessage = "O CPF deve ter 14 caracteres.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O Perfil Usuario é obrigatório.")]
    public int PerfilUsuarioId { get; set; }
    [JsonIgnore]
    public PerfilUsuario? PerfilUsuario { get; set; }

    [Required(ErrorMessage = "A Empresa é obrigatório.")]
    public int EmpresaId { get; set; }
    [JsonIgnore]
    public Empresa? Empresa { get; set; }
}
