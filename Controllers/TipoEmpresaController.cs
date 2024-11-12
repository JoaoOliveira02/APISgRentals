using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeEmpresas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoEmpresaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TipoEmpresaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtem uma lista de objetos Tipos Empresas
        /// </summary>
        /// <returns>Retorna uma lista de objetos Tipos Empresas</returns>
        // Consultar todos os tipos de empresas
        [HttpGet("GetAllTipoEmpresas")]
        public async Task<IActionResult> GetAllTipoEmpresas()
        {
            var tipoEmpresas = await _unitOfWork.TipoEmpresaRepository.GetAllAsync();

            if (tipoEmpresas == null)
            {
                return NotFound("Nenhum Dado encontrado");
            }

            return Ok(tipoEmpresas);
        }

        /// <summary>
        /// Obtem uma Tipo Empresa pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto Tipo Empresa</returns>
        // Consultar um tipo de empresa pelo ID
        [HttpGet("GetTipoEmpresa/{id}")]
        public async Task<IActionResult> GetTipoEmpresa(int id)
        {
            var tipoEmpresa = await _unitOfWork.TipoEmpresaRepository.GetAsync(id);

            if (tipoEmpresa == null)
            {
                return NotFound("O Tipo de Empresa não foi encontrado!");
            }

            return Ok(tipoEmpresa);
        }

        /// <summary>
        /// Inclui um novo tipo empresa
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/Empresa/AddTipoEmpresa
        ///     {
        ///        "nome": "EXEMPLO",
        ///     }
        /// </remarks>
        /// <param>objeto Tipo Empresa</param>
        /// <returns>O objeto Tipo Empresa incluida</returns>
        /// <remarks>Retorna um objeto Tipo Empresa incluído</remarks>
        // Adicionar um novo tipo de empresa
        [HttpPost("AddTipoEmpresa")]
        public async Task<IActionResult> AddTipoEmpresa(TipoEmpresa tipoEmpresa)
        {
            if (tipoEmpresa == null)
            {
                return BadRequest("Tipo de empresa não pode ser nulo.");
            }

            await _unitOfWork.TipoEmpresaRepository.CreateAsync(tipoEmpresa);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetTipoEmpresa), new { id = tipoEmpresa.Id }, tipoEmpresa);
            }

            return BadRequest("Erro ao salvar o tipo de empresa.");
        }

        /// <summary>
        /// Modificar um tipo empresa existente
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Put api/Empresa/UpdateTipoEmpresa/1
        ///     {
        ///        "id": 1,
        ///        "nome": "EXEMPLO"
        ///     }
        /// </remarks>
        /// <param>objeto Tipo Empresa</param>
        /// <returns>O objeto Tipo Empresa modificado</returns>
        /// <remarks>Retorna um objeto Tipo Empresa modificado</remarks>
        // Atualizar um tipo de empresa
        [HttpPut("UpdateTipoEmpresa/{id}")]
        public async Task<IActionResult> UpdateTipoEmpresa(int id, TipoEmpresa tipoEmpresa)
        {
            if (id != tipoEmpresa.Id)
            {
                return BadRequest("ID do tipo de empresa não corresponde.");
            }

            await _unitOfWork.TipoEmpresaRepository.UpdateAsync(tipoEmpresa);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return Ok(tipoEmpresa);
            }

            return BadRequest("Erro ao atualizar o tipo de empresa.");
        }

        /// <summary>
        /// Exclui um Tipo Empresa pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto Tipo Empresa Excluido</returns>
        // Excluir um tipo de empresa
        [HttpDelete("DeleteTipoEmpresa/{id}")]
        public async Task<IActionResult> DeleteTipoEmpresa(int id)
        {
            if (id <= 0)
            {
                return NotFound("Não Encontrado");
            }

            var tipoEmpresa = await _unitOfWork.TipoEmpresaRepository.GetAsync(id);
            if (tipoEmpresa == null)
            {
                return NotFound("Perfil Usuario não encontrada...");
            }
            var tipoEmpresaDeletado = await _unitOfWork.TipoEmpresaRepository.DeleteAsync(tipoEmpresa);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return Ok(tipoEmpresaDeletado);
            }

            return BadRequest("Erro ao excluir o tipo de empresa.");
        }
    }
}
