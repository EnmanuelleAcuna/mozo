using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.Globalization;

namespace SGJD_INA.Models.Entities {
    public class UsuarioArticulo {
        // Constructores
        public UsuarioArticulo() { }

        // Constructor para atributos
        public UsuarioArticulo(SGJD_ACT_TAB_USUARIOS_ARTICULO UsuarioArticuloBD) {
            if (UsuarioArticuloBD is null) throw new ArgumentNullException(paramName: nameof(UsuarioArticuloBD), message: Resources.ModeloNulo);

            Id = UsuarioArticuloBD.LLP_Id;
            IdArticulo = UsuarioArticuloBD.LLF_IdArticulo;
            IdUsuario = UsuarioArticuloBD.LLF_IdUsuario;
            IdOtroAsistente = UsuarioArticuloBD.LLF_IdOtroAsistente;
        }

        // Constructor para atributos y propiedades de navegación padre
        public UsuarioArticulo(SGJD_ACT_TAB_USUARIOS_ARTICULO UsuarioArticuloBD, SGJD_ACT_TAB_ARTICULOS ArticuloBD, SGJD_ADM_TAB_USUARIOS UsuarioBD, SGJD_ACT_TAB_OTROS_ASISTENTES_SESION OtroAsistenteBD) {
            if (UsuarioArticuloBD is null) throw new ArgumentNullException(paramName: nameof(UsuarioArticuloBD), message: Resources.ModeloNulo);

            Id = UsuarioArticuloBD.LLP_Id;
            IdArticulo = UsuarioArticuloBD.LLF_IdArticulo;
            IdUsuario = UsuarioArticuloBD.LLF_IdUsuario;
            IdOtroAsistente = UsuarioArticuloBD.LLF_IdOtroAsistente;

            Articulo = ArticuloBD != null ? new Articulo(ArticuloBD) : null;
            Usuario = !string.IsNullOrEmpty(UsuarioArticuloBD.LLF_IdUsuario) ? new ApplicationUser(UsuarioBD) : null;
            OtroAsistente = UsuarioArticuloBD.LLF_IdOtroAsistente.HasValue ? new OtroAsistenteSesion(OtroAsistenteBD) : null;
        }

        // Constructor para agregar un asistente a un articulo
        public UsuarioArticulo(int IdArticulo, string IdUsuario) {
            this.IdArticulo = IdArticulo;

            if (!Funciones.IsNumber(IdUsuario)) {
                this.IdUsuario = IdUsuario;
            }
            else {
                this.IdOtroAsistente = Convert.ToInt32(IdUsuario, new CultureInfo(Resources.CultureInfo));
            }
        }

        // Atributos
        public int Id { get; set; }

        public int IdArticulo { get; set; }

        public string IdUsuario { get; set; }

        public int? IdOtroAsistente { get; set; }

        // Propiedades de navegación
        // Padre
        public Articulo Articulo { get; set; }

        public ApplicationUser Usuario { get; set; }

        public OtroAsistenteSesion OtroAsistente { get; set; }

        public string ObtenerInformacion() {
            return "id:" + Id + ", " +
                "idArticulo:" + IdArticulo + ", " +
                "idUsuario:" + IdUsuario + ", " +
                "idOtroAsistente:" + IdOtroAsistente;
        }

        public SGJD_ACT_TAB_USUARIOS_ARTICULO BD() {
            return new SGJD_ACT_TAB_USUARIOS_ARTICULO() {
                LLP_Id = Id,
                LLF_IdArticulo = IdArticulo,
                LLF_IdUsuario = IdUsuario,
                LLF_IdOtroAsistente = IdOtroAsistente
            };
        }
    }
}