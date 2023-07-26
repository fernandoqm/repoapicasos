using TarjetasApi.Domain.Entities;

namespace TarjetasApi.Domain.Interface
{
    public interface IUsuarioSeguridadRepository
    {
        Task<bool> ValidaAcceso(usuario_seguridad usuarioSeguridad);
    }
}
