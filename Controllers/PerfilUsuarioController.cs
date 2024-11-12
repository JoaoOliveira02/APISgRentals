using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeEmpresas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilUsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PerfilUsuarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtem uma lista de objetos Tipos Perfil
        /// </summary>
        /// <returns>Retorna uma lista de objetos Tios Perfil</returns>
        // Consultar todos os tipos de Perfil
        [HttpGet("GetAllTipoPerfilUsuario")]
        public async Task<IActionResult> GetAllTipoPerfilUsuario()
        {
            var perfilUsuario = await _unitOfWork.PerfilUsuarioRepository.GetAllAsync();

            if (perfilUsuario == null)
            {
                return NotFound("Nenhum Dado encontrado");
            }

            return Ok(perfilUsuario);
        }

        /// <summary>
        /// Obtem uma Tipo Perfil pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto Tipo Perfil</returns>
        // Consultar um tipo de perfil pelo ID
        [HttpGet("GetPerfilUsuario/{id}")]
        public async Task<IActionResult> GetPerfilUsuario(int id)
        {
            var perfilUsuario = await _unitOfWork.PerfilUsuarioRepository.GetAsync(id);

            if (perfilUsuario == null)
            {
                return NotFound("O Perfil Usuario não foi encontrado!");
            }

            return Ok(perfilUsuario);
        }

        /// <summary>
        /// Inclui um novo tipo Perfil
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/Empresa/AddPerfilUsuario
        ///     {
        ///        "nome": "EXEMPLO",
        ///     }
        /// </remarks>
        /// <param>objeto Tipo Usuario</param>
        /// <returns>O objeto Tipo Usuario incluida</returns>
        /// <remarks>Retorna um objeto Tipo Usuario incluído</remarks>
        // Adicionar um novo tipo de perfil
        [HttpPost("AddPerfilUsuario")]
        public async Task<IActionResult> AddPerfilUsuario(PerfilUsuario perfilUsuario)
        {
            if (perfilUsuario == null)
            {
                return BadRequest("Perfil Usuario não pode ser nulo.");
            }

            await _unitOfWork.PerfilUsuarioRepository.CreateAsync(perfilUsuario);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetPerfilUsuario), new { id = perfilUsuario.Id }, perfilUsuario);
            }

            return BadRequest("Erro ao salvar o Perfil Usuario.");
        }

        /// <summary>
        /// Modificar um tipo perfil existente
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Put api/Empresa/UpdatePerfilUsuario/1
        ///     {
        ///        "id": 1,
        ///        "nome": "EXEMPLO"
        ///     }
        /// </remarks>
        /// <param>objeto Tipo Usuario</param>
        /// <returns>O objeto Tipo Usuario modificado</returns>
        /// <remarks>Retorna um objeto Tipo Usuario modificado</remarks>
        // Atualizar um tipo de usuario
        [HttpPut("UpdatePerfilUsuario/{id}")]
        public async Task<IActionResult> UpdatePerfilUsuario(int id, PerfilUsuario perfilUsuario)
        {
            if (id != perfilUsuario.Id)
            {
                return BadRequest("ID do Perfil Usuario não corresponde.");
            }

            await _unitOfWork.PerfilUsuarioRepository.UpdateAsync(perfilUsuario);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Erro ao atualizar o Perfil Usuario.");
        }

        /// <summary>
        /// Exclui um Tipo Usuario pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto Tipo Usuario Excluido</returns>
        // Excluir um tipo de usuario
        [HttpDelete("DeletePerfilUsuario/{id}")]
        public async Task<IActionResult> DeletePerfilUsuario(int id)
        {
            if (id <= 0)
            {
                return NotFound("Não Encontrado");
            }

            var perfilUsuario = await _unitOfWork.PerfilUsuarioRepository.GetAsync(id);
            if (perfilUsuario == null)
            {
                return NotFound("Perfil Usuario não encontrada...");
            }
            var perfilUsuarioDeletado = await _unitOfWork.PerfilUsuarioRepository.DeleteAsync(perfilUsuario);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return Ok(perfilUsuarioDeletado);
            }

            return BadRequest("Erro ao excluir o Perfil Usuario.");
        }
    }
}
