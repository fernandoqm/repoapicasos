using TarjetasApi.Domain.Entities;

namespace TarjetasApi.Domain.Interface
{
    public interface ITarjetaRepository
    {
        Task<bool> CreateTarjetaAsync(tarjeta tarjeta);
        Task<bool> DeleteTarjetaAsync(tarjeta tarjeta);
        tarjeta GetTarjetaById(int id);
        Task <List<tarjeta>> GetTarjeta();
        Task<bool> UpdateTarjetaAsync(tarjeta tarjeta);
    }
}
