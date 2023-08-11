using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class OrdenDia {
        public OrdenDia() { }

        // Constructor para llenar solo las propiedades del objeto
        public OrdenDia(SGJD_ACT_TAB_ORDENES_DIA OrdenDiaBD) {
            Id = OrdenDiaBD.LLP_Id;
            IdSesion = OrdenDiaBD.LLF_IdSesion;
            IdEstado = OrdenDiaBD.LLF_IdEstado;
            Encabezado = OrdenDiaBD.Encabezado;
            NombreObjeto = OrdenDiaBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para llenar las propiedades del objeto y las propiedades de navegación padre
        public OrdenDia(SGJD_ACT_TAB_ORDENES_DIA OrdenDiaBD, SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ADM_TAB_ESTADOS EstadoBD) {
            Id = OrdenDiaBD.LLP_Id;
            IdSesion = OrdenDiaBD.LLF_IdSesion;
            IdEstado = OrdenDiaBD.LLF_IdEstado;
            Encabezado = OrdenDiaBD.Encabezado;
            NombreObjeto = OrdenDiaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Sesion = new Sesion(SesionBD, SesionBD.SGJD_ADM_TAB_TIPOS_SESION, SesionBD.SGJD_ADM_TAB_USUARIOS, SesionBD.SGJD_ADM_TAB_ESTADOS);
            Estado = new Estado(EstadoBD);
        }

        // Constructor para llenar las prodiendades del objeto y las propiedades de navegaación hijo
        public OrdenDia(SGJD_ACT_TAB_ORDENES_DIA OrdenDiaBD, IEnumerable<SGJD_ADM_TAB_SECCIONES> SeccionesBD) {
            Id = OrdenDiaBD.LLP_Id;
            IdSesion = OrdenDiaBD.LLF_IdSesion;
            IdEstado = OrdenDiaBD.LLF_IdEstado;
            Encabezado = OrdenDiaBD.Encabezado;
            NombreObjeto = OrdenDiaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Secciones = SeccionesBD.Select(SeccionBD => new Seccion(SeccionBD)).ToList();
        }

        // Constructor para llenar las propiedades del objeto y las propiedades de navegación padre e hijo
        public OrdenDia(SGJD_ACT_TAB_ORDENES_DIA OrdenDiaBD, SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ADM_TAB_ESTADOS EstadoBD, IEnumerable<SGJD_ADM_TAB_SECCIONES> SeccionesBD) {
            Id = OrdenDiaBD.LLP_Id;
            IdSesion = OrdenDiaBD.LLF_IdSesion;
            IdEstado = OrdenDiaBD.LLF_IdEstado;
            Encabezado = OrdenDiaBD.Encabezado;
            NombreObjeto = OrdenDiaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Sesion = new Sesion(SesionBD, SesionBD.SGJD_ADM_TAB_TIPOS_SESION, SesionBD.SGJD_ADM_TAB_USUARIOS, SesionBD.SGJD_ADM_TAB_ESTADOS);
            Estado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
            Secciones = SeccionesBD.Select(SeccionBD => new Seccion(SeccionBD, SeccionBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdOrdenDia == OrdenDiaBD.LLP_Id).ToList())).ToList();
        }

        // Constructor para crear un Orden del Día a partir de una Sesión
        public OrdenDia(int IdSesion, int IdEstado) {
            this.IdSesion = IdSesion;
            this.IdEstado = IdEstado;
            this.Encabezado = string.Empty;
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "La sesión es requerida")]
        public int IdSesion { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }

        public string Encabezado { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padre
        public Sesion Sesion { get; set; }

        public Estado Estado { get; set; }

        // Hijos
        public IEnumerable<Seccion> Secciones { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idSesion:" + IdSesion + ", " +
                "idEstado:" + IdEstado + ", " +
                "encabezado:" + Encabezado;
        }

        public SGJD_ACT_TAB_ORDENES_DIA BD() {
            return new SGJD_ACT_TAB_ORDENES_DIA() {
                LLP_Id = Id,
                LLF_IdEstado = IdEstado,
                LLF_IdSesion = IdSesion,
                Encabezado = Encabezado
            };
        }
    }
}