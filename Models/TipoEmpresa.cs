﻿using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeEmpresas.Models;

public class TipoEmpresa
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O Nome do Tipo da Empresa é obrigatório", AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome do Tipo da Empresa deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; }
}
