using System.ComponentModel.DataAnnotations;

namespace TarjetasApi.Domain.Entities
{
    public class tarjeta
    {
        [Key]
        public decimal id_tarjeta { get; set; }
        public string caso { get; set; }
        public string tipo_caso { get; set; }
        public string descripcion { get; set; }
        public decimal id_proyecto { get; set; }
        public string nombre { get; set; }
        public string reportado_por { get; set; }
        public string solucionado_por { get; set; }
        public DateTime fecha_inicio_atencion { get; set; }
        public DateTime fecha_fin_atencion { get; set; }
        public string estado { get; set; }
        public string desc_estado { get; set; }
        public Int16 cantidad_incidencias { get; set; }
        public string branch_padre { get; set; }
        public string notas { get; set; }
        public Int16 entregado_cliente { get; set; }
        public Int16 merge_padre { get; set; }
        public string esAprobado { get; set; }
        public string version { get; set; }
        public string script { get; set; }

    }
}
