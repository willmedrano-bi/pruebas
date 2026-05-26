using Library.Models.Unificada;

namespace WS_DATA.Services
{
    public interface IAuditoriaService
    {
        Task RegistrarAsync(LogsApiExterna log);
    }
}
