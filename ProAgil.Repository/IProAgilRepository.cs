using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public interface IProAgilRepository
  {
    //GERAL
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;

    Task<bool> SaveChangesAsync();

    //EVENTOS
    Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
    Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
    Task<Evento> GetAllEventosAsyncById(int eventoId, bool includePalestrantes);

    //PALESTRANTES
    Task<Palestrante[]> GetPalestranteAsync(int palestrantesId, bool includeEventos);
    Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos);
  }
}