using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Sesion {
        // Constructores
        public Sesion() { }

        // Constructor para crear una nueva sesión
        public Sesion(int TipoSesion, DateTime Fecha, string Hora, string IdUsuario) {
            IdTipoSesion = TipoSesion;
            FechaHora = Funciones.JoinStringToDateTime(Fecha, Hora);
            this.IdUsuario = IdUsuario;
        }

        // Constructor para atributos
        public Sesion(SGJD_ACT_TAB_SESIONES SesionBD) {
            if (SesionBD is null) throw new ArgumentNullException(paramName: nameof(SesionBD), message: Resources.ModeloNulo);

            Id = SesionBD.LLP_Id;
            IdTipoSesion = SesionBD.LLF_IdTipoSesion;
            IdUsuario = SesionBD.LLF_IdUsuario;
            IdEstado = SesionBD.LLF_IdEstado;
            Numero = SesionBD.Numero;
            FechaHora = SesionBD.FechaHora;
            NombreObjeto = SesionBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para atributos y propiedades de navegación padre
        public Sesion(SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ADM_TAB_TIPOS_SESION TipoSesionBD, SGJD_ADM_TAB_USUARIOS UsuarioBD, SGJD_ADM_TAB_ESTADOS EstadoBD) {
            if (SesionBD is null) throw new ArgumentNullException(paramName: nameof(SesionBD), message: Resources.ModeloNulo);

            Id = SesionBD.LLP_Id;
            IdTipoSesion = SesionBD.LLF_IdTipoSesion;
            IdUsuario = SesionBD.LLF_IdUsuario;
            IdEstado = SesionBD.LLF_IdEstado;
            Numero = SesionBD.Numero;
            FechaHora = SesionBD.FechaHora;
            NombreObjeto = SesionBD.GetType().UnderlyingSystemType.BaseType.Name;

            TipoSesion = new TipoSesion(TipoSesionBD);
            Usuario = new ApplicationUser(UsuarioBD);
            Estado = new Estado(EstadoBD);
        }

        // Constructor para atributos y propiedades de navegación padre e hijos
        public Sesion(SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ADM_TAB_TIPOS_SESION TipoSesionBD, SGJD_ADM_TAB_USUARIOS UsuarioBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ACT_TAB_ORDENES_DIA OrdenDiaBD, SGJD_ACT_TAB_ACTAS ActaBD) {
            if (SesionBD is null) throw new ArgumentNullException(paramName: nameof(SesionBD), message: Resources.ModeloNulo);

            Id = SesionBD.LLP_Id;
            IdTipoSesion = SesionBD.LLF_IdTipoSesion;
            IdUsuario = SesionBD.LLF_IdUsuario;
            IdEstado = SesionBD.LLF_IdEstado;
            Numero = SesionBD.Numero;
            FechaHora = SesionBD.FechaHora;
            NombreObjeto = SesionBD.GetType().UnderlyingSystemType.BaseType.Name;

            TipoSesion = new TipoSesion(TipoSesionBD);
            Usuario = new ApplicationUser(UsuarioBD);
            Estado = new Estado(EstadoBD);

            OrdenDia = new OrdenDia(OrdenDiaBD);
            Acta = ActaBD != null ? new Acta(ActaBD) : new Acta();
        }

        // Propiedades
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "El tipo de sesión es requerido")]
        public int IdTipoSesion { get; set; }

        [Required(ErrorMessage = "EL usuario es requerido")]
        [Display(Name = "Usuario")]
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "EL estado es requerido")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El número es requerido")]
        [Display(Name = "Numero de sesión")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        [Display(Name = "Fecha")]
        public DateTime FechaHora { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        //Padres
        public TipoSesion TipoSesion { get; set; }

        public ApplicationUser Usuario { get; set; }

        public Estado Estado { get; set; }

        //Hijos
        public OrdenDia OrdenDia { get; set; }

        public Acta Acta { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + "," +
                "Número:" + Numero + "," +
                "Fecha:" + FechaHora + "," +
                "idTiposesion:" + IdTipoSesion + "," +
                "idUsuario:" + IdUsuario + "," +
                "idEstado:" + IdEstado;
        }

        public SGJD_ACT_TAB_SESIONES BD() {
            return new SGJD_ACT_TAB_SESIONES() {
                LLP_Id = Id,
                LLF_IdTipoSesion = IdTipoSesion,
                LLF_IdUsuario = IdUsuario,
                LLF_IdEstado = IdEstado,
                FechaHora = FechaHora,
                Numero = Numero
            };
        }
    }
}