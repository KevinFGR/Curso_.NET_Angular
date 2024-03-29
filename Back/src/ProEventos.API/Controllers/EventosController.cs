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
                return NoContent();
            }

            return Ok(eventos);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Error trying to get events. Error{ex.Message}");
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
                return NoContent();
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Error trying to get this event.  Error{ex.Message}");
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
                return NoContent();
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Error trying to get events. Error{ex.Message}");
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
                return NoContent();
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Something went wrong whent trying to add this event. Error{ex.Message}");
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
                return NoContent();
            }
            return Ok(evento);

        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Something went wront while updating this event. Error{ex.Message}");
        }    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var evento = await _eventoService.GetEventoByIdAsync(id);
            if(evento == null){
                return NoContent();
            }
            return await _eventoService.DeleteEvento(id) ? Ok(new{message = "Deleted"}) :
                BadRequest("It wasn't possible to delete this event.");
        }
        catch (Exception ex)
        {
            
            return this.StatusCode(StatusCodes.Status500InternalServerError,
            $"Something went wrong trying to delete this event. Error{ex.Message}");
        }
    }
}

