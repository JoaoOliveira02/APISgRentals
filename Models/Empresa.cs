﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GerenciadorDeEmpresas.Models;

public class Empresa
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome fantasia é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome fantasia deve ter entre 3 e 100 caracteres.")]
    public string NomeFantasia { get; set; }

    [Required(ErrorMessage = "A razão social é obrigatória.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "A razão social deve ter entre 3 e 150 caracteres.")]
    public string RazaoSocial { get; set; }

    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [StringLength(18, MinimumLength = 18, ErrorMessage = "O CNPJ deve ter 18 caracteres.")]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "O Endereco é obrigatório.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "O Endereco deve ter entre 10 e 200 caracteres.")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "O Tipo de Empresa é obrigatório.")]
    public int TipoEmpresaId { get; set; }

    [JsonIgnore]
    public TipoEmpresa? TipoEmpresa { get; set; }

    [JsonIgnore]
    public ICollection<Usuario>? Usuarios { get; set; }
}
