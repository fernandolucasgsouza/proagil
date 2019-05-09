namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }
        public Palestrante Palastrante { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}