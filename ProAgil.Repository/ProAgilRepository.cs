using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public class ProAgilRepository : IProAgilRepository
  {

    private ProAgilContext _context { get; }

    public ProAgilRepository(ProAgilContext context)
    {
      _context = context;
    }

    //GERAIS
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }


    //EVENTOS
    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos
       .Include(c => c.Lotes)
       .Include(c => c.RedeSociais);

      if (includePalestrantes)
      {
        query = query
        .Include(pe => pe.PalestranteEventos)
        .ThenInclude(p => p.Palastrante);
      }

      query = query.OrderByDescending(c => c.Data);

      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetAllEventosAsyncById(int eventoId, bool includePalestrantes)
    {
      IQueryable<Evento> query = _context.Eventos
        .Include(c => c.Lotes)
        .Include(c => c.RedeSociais);

      if (includePalestrantes)
      {
        query = query
        .Include(pe => pe.PalestranteEventos)
        .ThenInclude(p => p.Palastrante);
      }

      query = query.OrderByDescending(c => c.Data)
      .Where(c => c.Id == eventoId);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
    {
      IQueryable<Evento> query = _context.Eventos
       .Include(c => c.Lotes)
       .Include(c => c.RedeSociais);

      if (includePalestrantes)
      {
        query = query
        .Include(pe => pe.PalestranteEventos)
        .ThenInclude(p => p.Palastrante);
      }

      query = query.OrderByDescending(c => c.Data)
      .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

      return await query.ToArrayAsync();
    }

    //PALESTRANTE
    public async Task<Palestrante> GetPalestranteAsync(int palestrantesId, bool includeEventos = false)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
        .Include(c => c.RedeSociais);

      if (includeEventos)
      {
        query = query
        .Include(pe => pe.PalestranteEventos)
        .ThenInclude(e => e.Evento);
      }

      query = query.OrderBy(p => p.Nome)
      .Where(p => p.Id == palestrantesId);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
        .Include(c => c.RedeSociais);

      if (includeEventos)
      {
        query = query
        .Include(pe => pe.PalestranteEventos)
        .ThenInclude(e => e.Evento);
      }

      query = query.OrderBy(p => p.Nome.ToLower().Contains(nome.ToLower()));

      return await query.ToArrayAsync();
    }

  }
}