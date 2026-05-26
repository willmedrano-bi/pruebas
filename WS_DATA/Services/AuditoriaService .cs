
using Library.Models.Unificada;

namespace WS_DATA.Services
    
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly UnificadaSpkeyContext _context;
        public AuditoriaService(UnificadaSpkeyContext context)
        {
            _context = context;
        }
        public async  Task RegistrarAsync(LogsApiExterna log)
        {
            _context.LogsApiExterna.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
