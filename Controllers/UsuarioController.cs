using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.Repositories.Interfaces;
using GerenciadorDeEmpresas.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GerenciadorDeEmpresas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtem uma lista de objetos Usuario
        /// </summary>
        /// <returns>Retorna uma lista de objetos Usuarios</returns>
        // Consultar todos os usuários
        [HttpGet("GetAllUsuarios")]
        public async Task<IActionResult> GetAllUsuarios()
        {
            try
            {
                var usuarios = await _unitOfWork.UsuarioRepository.GetAllUsuariosAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("Nenhum usuário encontrado.");
                }

                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao buscar os usuários.");
            }
        }

        /// <summary>
        /// Obtem uma usuario pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto usuario</returns>
        // Consultar um usuário pelo ID
        [HttpGet("GetUsuario/{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetUsuarioAsync(id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception)
            {
                // Logar exceção
                return StatusCode(500, "Ocorreu um erro ao buscar o usuário.");
            }
        }

        /// <summary>
        /// Obtem usuarios pelo Id da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto usuario da empresa</returns>
        // Consultar usuários por empresa
        [HttpGet("GetUsuariosByEmpresa/{empresaId}")]
        public async Task<IActionResult> GetUsuariosByEmpresa(int empresaId)
        {
            if (empresaId <= 0)
            {
                return BadRequest("ID de empresa inválido.");
            }

            try
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetUsuariosPorEmpresa(empresaId);

                if (usuario == null || !usuario.Any())
                {
                    return NotFound("Nenhum usuário encontrado para a empresa especificada.");
                }

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar usuários.");
            }
        }

        /// <summary>
        /// Obtem usuarios pelo Id do tipo de perfil
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto usuario do tipo de perfil</returns>
        // Consultar usuários por perfil
        [HttpGet("GetUsuariosByPerfil/{perfilUsuarioId}")]
        public async Task<IActionResult> GetUsuariosByPerfil(int perfilUsuarioId)
        {
            if (perfilUsuarioId <= 0)
            {
                return BadRequest("ID de perfil inválido.");
            }

            try
            {
                var usuarios = await _unitOfWork.UsuarioRepository.GetUsuariosPorPerfil(perfilUsuarioId);

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("Nenhum usuário encontrado para o perfil especificado.");
                }

                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar usuários.");
            }
        }

        [HttpGet("CPFExists")]
        public async Task<ActionResult<bool>> CPFExistsAsync(string cpf, int empresaId)
        {
            // Verifica se o CPF já está registrado para a empresa
            bool cpfExists = await _unitOfWork.UsuarioRepository.CPFExistsAsync(cpf, empresaId);

            if (cpfExists)
            {
                // Retorna HTTP 409 (Conflict) se o CPF já existir
                return Conflict(true);
            }

            // Retorna HTTP 200 (OK) se o CPF não existir
            return Ok(false);
        }


        /// <summary>
        /// Inclui um novo Usuario
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/Empresa/AddUsuario
        ///     {   
        ///        "nome": "string",
        ///        "cpf": "string",
        ///        "perfilUsuarioId": 1,
        ///        "empresaId": 1
        ///     }
        /// </remarks>
        /// <param>objeto Usuario</param>
        /// <returns>O objeto Usuario incluida</returns>
        /// <remarks>Retorna um objeto Usuario incluído</remarks>
        // Adicionar um usuário
        [HttpPost("AddUsuario")]
        public async Task<IActionResult> AddUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuário não pode ser nulo.");
            }

            try
            {

                // Cria o novo usuário
                await _unitOfWork.UsuarioRepository.CreateAsync(usuario);

                // Tenta salvar as mudanças no banco
                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
                }

                return BadRequest("Erro ao salvar o usuário.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao adicionar o usuário.");
            }
        }


        /// <summary>
        /// Modificar um Usuario existente
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     Put api/Empresa/UpdateEmpresa/1
        ///     {
        ///        "id": 1,
        ///        "nome": "string",
        ///        "cpf": "string",
        ///        "perfilUsuarioId": 1,
        ///        "empresaId": 1
        ///     }
        /// </remarks>
        /// <param>objeto Usuario</param>
        /// <returns>O objeto Usuario modificado</returns>
        /// <remarks>Retorna um objeto Usuario modificado</remarks>
        // Atualizar um usuário
        [HttpPut("UpdateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("Dados inválidos para atualização.");
            }
            if (usuario == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {

                await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
                }

                return BadRequest("Erro ao atualizar o usuário.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar.");
            }
        }

        /// <summary>
        /// Exclui um Usuario pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto usuario Excluido</returns>
        // Excluir um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            try
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetUsuarioAsync(id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                var usuarioDeletado = await _unitOfWork.UsuarioRepository.DeleteAsync(usuario);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return Ok(usuarioDeletado);
                }

                return BadRequest("Erro ao excluir o usuário.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir o usuário.");
            }
        }


        /// <summary>
        /// Obtem uma lista de objetos Perfis e Empresas
        /// </summary>
        /// <returns>Retorna uma lista de objetos Perfis e Empresas</returns>
        [HttpGet("ObterPerfisEEmpresas")]
        public async Task<IActionResult> ListaPerfisEEmpresas()
        {
            try
            {
                var perfis = await _unitOfWork.PerfilUsuarioRepository.GetAllAsync();
                var empresas = await _unitOfWork.EmpresaRepository.GetAllAsync();

                // Montando as listas para o front-end (select)
                var perfisSelectList = perfis
                    .OrderBy(p => p.Id)
                    .Select(p => new
                    {
                        Text = p.Nome,
                        Value = p.Id.ToString()
                    })
                    .ToList();
                var empresasSelectList = empresas
                    .OrderBy(p => p.Id)
                    .Select(p => new
                    {
                        Text = p.NomeFantasia,
                        Value = p.Id.ToString()
                    })
                    .ToList();

                return Ok(new { perfis = perfisSelectList, empresas = empresasSelectList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter perfis e empresas: {ex.Message}");
            }
        }
    }
}
