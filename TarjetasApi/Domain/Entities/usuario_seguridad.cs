namespace TarjetasApi.Domain.Entities
{
    public class usuario_seguridad
    {
        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public DateTime fecha_ultimo_cambio { get; set; }
        public int dias_vigencia { get; set; }


    }
}
