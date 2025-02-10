using AlunosRegistro.Models;
using AlunosRegistro.Service;
using Microsoft.AspNetCore.Mvc;

namespace AlunosRegistro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly AlunoService _alunoService;

        public AlunoController(AlunoService alunoService)
        {
            _alunoService = alunoService;
        }
        [HttpGet]
        public async Task<List<Aluno>> Get() => await _alunoService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Aluno>> Get(string id)
        {
            var Aluno = await _alunoService.GetAsync(id);
            if (Aluno == null)
            {
                return NotFound();
            }
            return Ok(Aluno);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAluno(Aluno aluno)
        {
            await _alunoService.CreateAluno(aluno);
            return NoContent();
        }
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> UpdateAluno(string id, Aluno alunoAtualizado)
        {
            var aluno = await _alunoService.GetAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            alunoAtualizado.Id = aluno.Id;

            await _alunoService.UpdateAluno(id, alunoAtualizado);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Aluno = await _alunoService.GetAsync(id);

            if (Aluno is null)
            {
                return NotFound();
            }

            await _alunoService.RemoveAluno(id);

            return NoContent();
        }
    }
}
