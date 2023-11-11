using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var tarefa = _context.Tarefas.Find(id);

                return tarefa == null ? NotFound() : Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            try
            {
                var tarefas = _context.Tarefas.ToList();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }

        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            try
            {
                var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            try
            {
                var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            try
            {
                var tarefa = _context.Tarefas.Where(x => x.Status == status);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            try
            {
                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
                return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }

        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            try
            {
                var tarefaBanco = _context.Tarefas.Find(id);

                if (tarefaBanco == null)
                    return NotFound();

                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
                tarefaBanco.Titulo = tarefa.Titulo;
                tarefaBanco.Descricao = tarefa.Descricao;
                tarefaBanco.Data = tarefa.Data;
                tarefaBanco.Status = tarefa.Status;
                // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var tarefaBanco = _context.Tarefas.Find(id);

                if (tarefaBanco == null)
                    return NotFound();

                // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
                _context.Tarefas.Remove(tarefaBanco);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = $"Erro interno do servidor: {ex.Message}" });
            }

        }
    }
}
