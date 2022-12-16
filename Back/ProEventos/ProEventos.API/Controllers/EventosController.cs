using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _service;

        public EventosController(IEventosService eventosService)
        {
            _service = eventosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _service.GetAllEventosAsync(true);

                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.
                    StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                    );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _service.GetEventoByIdAsync(id, true);

                if (evento == null) return NotFound("Nenhum evento encontrado com o id escolhido.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.
                    StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                    );
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _service.GetAllEventosByTemaAsync(tema, true);

                if (evento == null) return NotFound("Nenhum evento encontrado com o tema escolhido.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.
                    StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                    );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _service.AddEventos(model);
                if (evento == null) return BadRequest("Erro ao cadastrar novo evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar novo evento, Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _service.Update(id, model);
                if (evento == null) return BadRequest("Erro ao tentar atualizar o evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar o evento, Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _service.DeleteEvento(id))
                    return Ok("Evento removido com sucesso.");
                else
                    return BadRequest("Erro ao tentar remover o evento.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir o evento, Erro: {ex.Message}");
            }
        }
    }
}
