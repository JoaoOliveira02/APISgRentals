using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeEmpresas.Models;

public class PerfilUsuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; }
}
