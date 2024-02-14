using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventosController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var eventos= await _eventoService.GetAllEventosAsync(true);
            if (eventos ==null)
            {
                return NotFound("Nenhum evento encontrado.");
            }

            return Ok(eventos);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar recuperar eventos. Error{ex.Message}");
        }

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var evento = await _eventoService.GetEventoByIdAsync(id, true);
            if (evento ==null)
            {
                return NotFound("Nenhum evento encontrado.");
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar recuperar eventos. Error{ex.Message}");
        }
    }

    [HttpGet("{tema}/tema")]
    public async Task<IActionResult> GetByTema(string tema)
    {
        try
        {
            var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
            if (evento ==null)
            {
                return NotFound("Nenhum evento encontrado.");
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar recuperar eventos. Error{ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
        try
        {
            var evento = await _eventoService.AddEvento(model);
            if (evento == null)
            {
                return BadRequest("Erro ao tentar adicionar evento.");
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar adicionar eventos. Error{ex.Message}");
        }
    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
        try
        {
            var evento = await _eventoService.UpdateEvento(id, model);
            if (evento ==null)
            {
                return BadRequest("Erro ao tentar adicionar evento.");
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar atualizar eventos. Error{ex.Message}");
        }    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _eventoService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Não foi possível deletar evento");
        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao tentar recuperar eventos. Error{ex.Message}");
        }
    }
}

