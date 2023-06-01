using Npgsql;
using System.Data;
using System.Text;
using TarjetasApi.Application.Context;
using TarjetasApi.Domain.Entities;
using TarjetasApi.Domain.Interface;
using TarjetasApi.Infrastructure.Conec;

namespace TarjetasApi.Infrastructure.Repositories
{
    public class TarjetaRepository : ITarjetaRepository
    {
        private string _Conn = "";


        public TarjetaRepository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json").Build();
            _Conn = builder.GetSection("ConnectionStrings:Connec").Value;


        }

        public async Task<bool> CreateTarjetaAsync(tarjeta tarjeta)
        {
            await using (var conn = new NpgsqlConnection(_Conn))
            {
                conn.Open();
                string query = "INSERT INTO tarjeta(id_tarjeta, caso, tipo_caso, id_proyecto, reportado_por, solucionado_por, fecha_inicio_atencion, " +
                               "fecha_fin_atencion, estado,cantidad_incidencias, branch_padre, notas, entregado_cliente, merge_padre )" +
                               "values (@id_tarjeta, @caso, @tipo_caso, @id_proyecto, @reportado_por, @solucionado_por, @fecha_inicio_atencion, @fecha_fin_atencion, @estado," +
                               "@cantidad_incidencias, @branch_padre, @notas, @entregado_cliente,@merge_padre);";
                await using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_tarjeta", tarjeta.id_tarjeta);
                    cmd.Parameters.AddWithValue("@caso", tarjeta.caso);
                    cmd.Parameters.AddWithValue("@tipo_caso", tarjeta.tipo_caso);
                    cmd.Parameters.AddWithValue("@id_proyecto", tarjeta.id_proyecto);
                    cmd.Parameters.AddWithValue("@reportado_por", tarjeta.reportado_por);
                    cmd.Parameters.AddWithValue("@solucionado_por", tarjeta.solucionado_por);
                    cmd.Parameters.AddWithValue("@fecha_inicio_atencion", tarjeta.fecha_inicio_atencion);
                    cmd.Parameters.AddWithValue("@fecha_fin_atencion", tarjeta.fecha_fin_atencion);
                    cmd.Parameters.AddWithValue("@estado", tarjeta.estado);
                    cmd.Parameters.AddWithValue("@cantidad_incidencias", tarjeta.cantidad_incidencias);
                    cmd.Parameters.AddWithValue("@branch_padre", tarjeta.branch_padre);
                    cmd.Parameters.AddWithValue("@notas", tarjeta.notas);
                    cmd.Parameters.AddWithValue("@entregado_cliente", tarjeta.entregado_cliente);
                    cmd.Parameters.AddWithValue("@merge_padre", tarjeta.merge_padre);

                    if (await cmd.ExecuteNonQueryAsync() != 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public Task<bool> DeleteTarjetaAsync(tarjeta tarjeta)
        {
            throw new NotImplementedException();
        }

        public tarjeta GetTarjetaById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<tarjeta>> GetTarjeta()
        {

            var lista = new List<tarjeta>();
            try
            {
                await using (var conn = new NpgsqlConnection(_Conn))
                {
                    conn.Open();
                    string sql = "";
                    sql =  "select id_tarjeta, caso, t.tipo_caso, tc.descripcion,t.id_proyecto,p.nombre, reportado_por, t.solucionado_por, ";
                    sql += "fecha_reporte, fecha_inicio_atencion, fecha_fin_atencion, case when fecha_aprobacion is null then 'N' else 'S' end esAprobado, ";
                    sql += "t.estado as tarjeta_estado, e.descripcion as estado, cantidad_incidencias, branch_padre, entregado_cliente, version, script, t.notas, t.merge_padre  ";
                    sql += "from tarjeta t  ";
                    sql += "  left outer join tipo_casos tc on t.tipo_caso = tc.tipo_caso  ";
                    sql += "  left outer join proyectos p on t.id_proyecto = p.id_proyecto  ";
                    sql += "  left outer join estados e on t.estado = e.id_estado  ";
                    sql += "order by id_tarjeta desc";

                    await using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        await using (var items = await cmd.ExecuteReaderAsync())
                        {
                            while (await items.ReadAsync())
                            {
                                var oTarjeta = new tarjeta();
                                oTarjeta.id_tarjeta = (decimal)items["id_tarjeta"];
                                oTarjeta.caso = (string)items["caso"];
                                oTarjeta.tipo_caso = (string)items["tipo_caso"];
                                oTarjeta.descripcion = (string)items["descripcion"];
                                oTarjeta.id_proyecto = (decimal)items["id_proyecto"];
                                oTarjeta.nombre = (string)items["nombre"];
                                oTarjeta.reportado_por = (string)items["tipo_caso"];
                                oTarjeta.reportado_por = (string)items["reportado_por"];
                                oTarjeta.solucionado_por = (string)items["solucionado_por"];
                                oTarjeta.fecha_inicio_atencion = (DateTime)items["fecha_inicio_atencion"];
                                oTarjeta.fecha_fin_atencion = (DateTime)items["fecha_fin_atencion"];
                                oTarjeta.estado = (string)items["estado"];
                                oTarjeta.cantidad_incidencias = (Int16)items["cantidad_incidencias"];
                                oTarjeta.branch_padre = (string)items["branch_padre"];
                                oTarjeta.notas = (string)items["notas"];
                                oTarjeta.entregado_cliente = (Int16)items["entregado_cliente"];
                                oTarjeta.merge_padre = (Int16)items["merge_padre"];
                                oTarjeta.esAprobado = (string)items["esAprobado"];
                                oTarjeta.version = (string)items["version"];
                                oTarjeta.script = (string)items["script"];

                                lista.Add(oTarjeta);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> UpdateTarjetaAsync(tarjeta tarjeta)
        {
            await using (var conn = new NpgsqlConnection(_Conn))
            {
                conn.Open();
                string query = "UPDATE tarjeta SET " +
                               "caso = @caso, tipo_caso = @tipo_caso, id_proyecto = @id_proyecto, reportado_por = @reportado_por, " +
                               "solucionado_por = @solucionado_por, fecha_inicio_atencion = @fecha_inicio_atencion, " +
                               "fecha_fin_atencion = @fecha_fin_atencion, estado = @estado, cantidad_incidencias = @cantidad_incidencias, " +
                               "branch_padre = @branch_padre, notas = @notas, entregado_cliente = @entregado_cliente, merge_padre = @merge_padre" +
                               "WHERE id_tarjeta = @id_tarjeta;";

                await using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_tarjeta", tarjeta.id_tarjeta);
                    cmd.Parameters.AddWithValue("@caso", tarjeta.caso);
                    cmd.Parameters.AddWithValue("@tipo_caso", tarjeta.tipo_caso);
                    cmd.Parameters.AddWithValue("@id_proyecto", tarjeta.id_proyecto);
                    cmd.Parameters.AddWithValue("@reportado_por", tarjeta.reportado_por);
                    cmd.Parameters.AddWithValue("@solucionado_por", tarjeta.solucionado_por);
                    cmd.Parameters.AddWithValue("@fecha_inicio_atencion", tarjeta.fecha_inicio_atencion);
                    cmd.Parameters.AddWithValue("@fecha_fin_atencion", tarjeta.fecha_fin_atencion);
                    cmd.Parameters.AddWithValue("@estado", tarjeta.estado);
                    cmd.Parameters.AddWithValue("@cantidad_incidencias", tarjeta.cantidad_incidencias);
                    cmd.Parameters.AddWithValue("@branch_padre", tarjeta.branch_padre);
                    cmd.Parameters.AddWithValue("@notas", tarjeta.notas);
                    cmd.Parameters.AddWithValue("@entregado_cliente", tarjeta.entregado_cliente);
                    cmd.Parameters.AddWithValue("@merge_padre", tarjeta.merge_padre);

                    if (await cmd.ExecuteNonQueryAsync() != 0)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}
