using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EmpresaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Obtem uma lista de objetos Empresas
    /// </summary>
    /// <returns>Retorna uma lista de objetos Empresas</returns>
    // Consultar todas as empresas
    [HttpGet("GetAllEmpresas")]
    public async Task<IActionResult> GetAllEmpresas()
    {
        try
        {
            var empresas = await _unitOfWork.EmpresaRepository.GetAllEmpresasAsync();

            if (empresas == null || !empresas.Any())
            {
                return NotFound("Nenhuma empresa encontrada.");
            }

            return Ok(empresas);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao buscar as empresas.");
        }
    }

    /// <summary>
    /// Obtem uma Empresa pelo seu Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Um objeto Empresa</returns>
    // Consultar uma empresa pelo ID
    [HttpGet("GetEmpresa/{id}")]
    public async Task<IActionResult> GetEmpresa(int id)
    {
        if (id <= 0)
        {
            return BadRequest("ID inválido.");
        }

        try
        {
            var empresa = await _unitOfWork.EmpresaRepository.GetEmpresaAsync(id);

            if (empresa == null)
            {
                return NotFound("Empresa não encontrada.");
            }

            return Ok(empresa);
        }
        catch (Exception)
        {
            // Logar a exceção se necessário
            return StatusCode(500, "Ocorreu um erro interno. Tente novamente mais tarde.");
        }
    }

    /// <summary>
    /// Obtem uma Empresa pelo Tipo de empresa com o id tipoEmpresaId
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Um objeto Empresa por tipo de empresa</returns>
    // Consultar uma empresa pelo ID
    // Consultar empresas por tipo
    [HttpGet("GetEmpresasByTipo/{tipoEmpresaId}")]
    public async Task<IActionResult> GetEmpresasByTipo(int tipoEmpresaId)
    {
        if (tipoEmpresaId <= 0)
        {
            return BadRequest("ID inválido.");
        }

        try
        {
            var empresas = await _unitOfWork.EmpresaRepository.GetPorTipoEmpresaAsync(tipoEmpresaId);

            if (empresas == null || !empresas.Any())
            {
                return NotFound("Nenhuma empresa encontrada para o tipo selecionado.");
            }

            return Ok(empresas);
        }
        catch (Exception)
        {
            // Logar a exceção se necessário
            return StatusCode(500, "Ocorreu um erro interno. Tente novamente mais tarde.");
        }
    }


    /// <summary>
    /// Inclui uma nova empresa
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     POST api/Empresa/AddEmpresa
    ///     {
    ///        "nomeFantasia": "EXEMPLO",
    ///        "razaoSocial": "EXEMPLO",
    ///        "cnpj": "stringstringst",
    ///        "endereco": "EXEMPLO",
    ///        "tipoEmpresaId": 1
    ///     }
    /// </remarks>
    /// <param>objeto Empresa</param>
    /// <returns>O objeto Empresa incluida</returns>
    /// <remarks>Retorna um objeto Empresa incluído</remarks>
    // Adicionar uma nova empresa
    [HttpPost("AddEmpresa")]
    public async Task<IActionResult> AddEmpresa(Empresa empresa)
    {
        if (empresa == null)
        {
            return BadRequest("Dados da empresa não podem ser nulos.");
        }

        // Verificar se o ModelState é válido
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna os erros de validação
        }

        try
        {

            var tipoEmpresa = await _unitOfWork.TipoEmpresaRepository.GetAsync(empresa.TipoEmpresaId);
            if (tipoEmpresa == null)
            {
                return BadRequest("Tipo de empresa inválido.");
            }


            // Obter todos os tipos de empresas do banco
            var listaTiposEmpresa = await _unitOfWork.TipoEmpresaRepository.GetAllAsync();

            // Verificar se o TipoEmpresaId da empresa existe na lista de tipos de empresa
            var tipoEmpresaExiste = listaTiposEmpresa.Any(t => t.Id == empresa.TipoEmpresaId);

            if (!tipoEmpresaExiste)
            {
                return BadRequest("Tipo de empresa inválido.");
            }


            // Criar a empresa
            await _unitOfWork.EmpresaRepository.CreateAsync(empresa);

            // Salvar as mudanças
            if (await _unitOfWork.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
            }

            return BadRequest("Erro ao salvar a empresa.");

        }
        catch (Exception ex)
        {
            // Logar o erro para depuração
            Console.WriteLine($"Erro: {ex.Message}");
            return StatusCode(500, "Erro interno ao adicionar a empresa.");
        }
    }
    


    /// <summary>
    /// Modificar uma empresa existente
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     Put api/Empresa/UpdateEmpresa/1
    ///     {
    ///        "id": 1,
    ///        "nomeFantasia": "EXEMPLO",
    ///        "razaoSocial": "EXEMPLO",
    ///        "cnpj": "stringstringst",
    ///        "endereco": "EXEMPLO",
    ///        "tipoEmpresaId": 1
    ///     }
    /// </remarks>
    /// <param>objeto Empresa</param>
    /// <returns>O objeto Empresa modificado</returns>
    /// <remarks>Retorna um objeto Empresa modificado</remarks>
    // Atualizar uma empresa
    [HttpPut("UpdateEmpresa/{id}")]
    public async Task<IActionResult> UpdateEmpresa(int id, Empresa empresa)
    {
        if (id != empresa.Id)
        {
            return BadRequest("Dados inválidos para atualização.");
        }

        if (empresa == null)
        {
            return NotFound("Empresa não encontrada.");
        }

        await _unitOfWork.EmpresaRepository.UpdateAsync(empresa);

        if (await _unitOfWork.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
        }

        return BadRequest("Erro ao atualizar a empresa.");
    }

    /// <summary>
    /// Exclui uma Empresa pelo seu Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Um objeto Empresa Excluido</returns>
    // Excluir uma empresa
    [HttpDelete("DeleteEmpresa/{id}")]
    public async Task<IActionResult> DeleteEmpresa(int id)
    {
        if (id <= 0)
        {
            return NotFound("Empresa não encontrada.");
        }
        var empresa = await _unitOfWork.EmpresaRepository.GetEmpresaAsync(id);
        if (empresa == null)
        {
            return NotFound("Empresa não encontrada...");
        }

        var empresaDeletada = _unitOfWork.EmpresaRepository.DeleteAsync(empresa);

        if (await _unitOfWork.SaveChangesAsync())
        {
            return Ok(empresaDeletada);
        }

        return BadRequest("Erro ao excluir a empresa.");
    }


    /// <summary>
    /// Obtem uma lista de objetos Tipo Empresas
    /// </summary>
    /// <returns>Retorna uma lista de objetos Tipo Empresas</returns>
    [HttpGet("ListaTipoEmpresas")]
    public async Task<IActionResult> ListaTipoEmpresas()
    {
        try
        {
            var empresas = await _unitOfWork.TipoEmpresaRepository.GetAllAsync();

            var empresasSelectList = empresas
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    Text = p.Nome,
                    Value = p.Id.ToString()
                })
                .ToList();

            return Ok(new { empresas = empresasSelectList });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter tipos de empresas: {ex.Message}");
        }
    }
}
