using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Tema {
        public Tema() { }

        public Tema(SGJD_ACT_TAB_TEMAS TemaBD) {
            Id = TemaBD.LLP_Id;
            IdSeccion = TemaBD.LLF_IdSeccion;
            IdOrdenDia = TemaBD.LLF_IdOrdenDia;
            IdEstado = TemaBD.LLF_IdEstado;
            Titulo = TemaBD.Titulo;
            Resumen = TemaBD.Resumen;
            Observacion = TemaBD.Observacion;
            NombreObjeto = TemaBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public Tema(SGJD_ACT_TAB_TEMAS TemaBD, SGJD_ADM_TAB_SECCIONES SeccionBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ACT_TAB_ORDENES_DIA OrdenDiaBD) {
            Id = TemaBD.LLP_Id;
            IdSeccion = TemaBD.LLF_IdSeccion;
            IdOrdenDia = TemaBD.LLF_IdOrdenDia;
            IdEstado = TemaBD.LLF_IdEstado;
            Titulo = TemaBD.Titulo;
            Resumen = TemaBD.Resumen;
            Observacion = TemaBD.Observacion;
            NombreObjeto = TemaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Seccion = new Seccion(SeccionBD);
            Estado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
            OrdenDia = new OrdenDia(OrdenDiaBD, OrdenDiaBD.SGJD_ACT_TAB_SESIONES, OrdenDiaBD.SGJD_ADM_TAB_ESTADOS);
        }

        // Propiedades
        [Required]
        public int Id { get; set; }

        public int IdSeccion { get; set; }

        public int IdOrdenDia { get; set; }

        public int IdEstado { get; set; }

        public string Titulo { get; set; }

        public string Resumen { get; set; }

        public string Observacion { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padres
        public Seccion Seccion { get; set; }

        public Estado Estado { get; set; }

        public OrdenDia OrdenDia { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + "," +
                "idSeccion:" + IdSeccion + "," +
                "idOrdenDia:" + IdOrdenDia + "," +
                "idEstado:" + IdEstado + "," +
                "Titulo:" + Titulo + "," +
                "Resumen:" + Resumen + "," +
                "Observacion:" + Observacion;
        }

        public SGJD_ACT_TAB_TEMAS BD() {
            return new SGJD_ACT_TAB_TEMAS() {
                LLP_Id = Id,
                LLF_IdSeccion = IdSeccion,
                LLF_IdOrdenDia = IdOrdenDia,
                LLF_IdEstado = IdEstado,
                Titulo = Titulo,
                Resumen = Resumen,
                Observacion = Observacion
            };
        }
    }
}