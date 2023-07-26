using Npgsql;
using System.Text;
using TarjetasApi.Domain.Entities;
using TarjetasApi.Domain.Interface;

namespace TarjetasApi.Infrastructure.Repositories
{
    public class UsuarioSeguridadRepository : IUsuarioSeguridadRepository
    {
        private string _Conn = "";
        public UsuarioSeguridadRepository()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appSettings.json").Build();
            _Conn = builder.GetSection("ConnectionStrings:Connec").Value;
        }


        public async Task<bool> ValidaAcceso(usuario_seguridad usuarioSeguridad)
        {
            try
            {
                await using (var conn = new NpgsqlConnection(_Conn))
                {
                    conn.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select * from usuario_seguridad ");
                    query.AppendLine("where usuario = @usuario and clave = @clave ");                    

                    await using (NpgsqlCommand cmd = new NpgsqlCommand(query.ToString(), conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuarioSeguridad.usuario);
                        cmd.Parameters.AddWithValue("@clave", usuarioSeguridad.clave);

                        await using (var items = await cmd.ExecuteReaderAsync())
                        {
                            if (items == null) 
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }

                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
    }
}
