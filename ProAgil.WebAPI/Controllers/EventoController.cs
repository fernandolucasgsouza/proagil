

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventoController : ControllerBase
  {
    private readonly IProAgilRepository _repo;

    public EventoController(IProAgilRepository repo)
    {
      _repo = repo;
    }

    // GET api/evento
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var results = await _repo.GetAllEventosAsync(true);
        return Ok(results);
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco De Dados Falhou");
      }
    }

    // GET api/evento/id
    [HttpGet("{EventoId}")]
    public async Task<IActionResult> Get(int EventoId)
    {
      try
      {
        var results = await _repo.GetAllEventosAsyncById(EventoId, true);
        return Ok(results);
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco De Dados Falhou");
      }
    }

    // GET api/evento/getByTema/nome
    [HttpGet("getByTema/{Tema}")]
    public async Task<IActionResult> Get(string Tema)
    {
      try
      {
        var results = await _repo.GetAllEventosAsyncByTema(Tema, true);
        return Ok(results);
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco De Dados Falhou");
      }
    }

    // POST api/evento
    [HttpPost]
    public async Task<IActionResult> Post(Evento model)
    {
      try
      {
        _repo.Add(model);

        if (await _repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.Id}", model);
        }
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco De Dados Falhou");
      }
      return BadRequest();
    }

    // PUT api/evento
    [HttpPut]
    public async Task<IActionResult> Put(int EventoId, Evento model)
    {
      try
      {
        var evento = _repo.GetAllEventosAsyncById(EventoId, false);

        _repo.Update(model);

        if (evento == null) return NotFound();

        if (await _repo.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.Id}", model);
        }
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco De Dados Falhou");
      }
      return BadRequest();
    }

    // Delet api/evento
    [HttpDelete]
    public async Task<IActionResult> Delete(int EventoId)
    {
      try
      {
        var evento = _repo.GetAllEventosAsyncById(EventoId, false);

        _repo.Delete(evento);

        if (evento == null) return NotFound();

        if (await _repo.SaveChangesAsync())
        {
          return Ok();
        }
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco De Dados Falhou");
      }
      return BadRequest();
    }

  }

}
