using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Bitacora {
        public Bitacora() { }

        public Bitacora(SGJD_ADM_TAB_BITACORA BitacoraBD) {
            if (BitacoraBD is null) throw new ArgumentNullException(paramName: nameof(BitacoraBD), message: Resources.ModeloNulo);

            Id = BitacoraBD.LLP_Id;
            IdUsuario = BitacoraBD.LLF_IdUsuario;
            FechaHora = BitacoraBD.FechaHora;
            Accion = BitacoraBD.Accion;
            ValorAntiguo = BitacoraBD.ValorAntiguo;
            ValorNuevo = BitacoraBD.ValorNuevo;
            DireccionIP = BitacoraBD.DireccionIP;
            DescripcionDispositivo = BitacoraBD.DescripcionDispositivo;
            SeccionSistema = BitacoraBD.SeccionSistema;
            NombreObjeto = BitacoraBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public Bitacora(SGJD_ADM_TAB_BITACORA BitacoraBD, SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            if (BitacoraBD is null) throw new ArgumentNullException(paramName: nameof(BitacoraBD), message: Resources.ModeloNulo);

            Id = BitacoraBD.LLP_Id;
            IdUsuario = BitacoraBD.LLF_IdUsuario;
            FechaHora = BitacoraBD.FechaHora;
            Accion = BitacoraBD.Accion;
            ValorAntiguo = BitacoraBD.ValorAntiguo;
            ValorNuevo = BitacoraBD.ValorNuevo;
            DireccionIP = BitacoraBD.DireccionIP;
            DescripcionDispositivo = BitacoraBD.DescripcionDispositivo;
            SeccionSistema = BitacoraBD.SeccionSistema;
            NombreObjeto = BitacoraBD.GetType().UnderlyingSystemType.BaseType.Name;

            Usuario = new ApplicationUser(UsuarioBD);
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime FechaHora { get; set; }

        public string Accion { get; set; }

        public string ValorAntiguo { get; set; }

        public string ValorNuevo { get; set; }

        public string DireccionIP { get; set; }

        public string DescripcionDispositivo { get; set; }

        public string SeccionSistema { get; set; }

        public string NombreObjeto { get; set; }

        public ApplicationUser Usuario { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id: " + Id + ", " +
                "idUsuario: " + IdUsuario + ", " +
                "fechaHora: " + FechaHora + ", " +
                "accion" + Accion + ", " +
                "ValorAntiguo: " + ValorAntiguo + ", " +
                "ValorNuevo: " + ValorNuevo + ", " +
                "direccionIP: " + DireccionIP + ", " +
                "descripcionDispositivo: " + DescripcionDispositivo + ", " +
                "SeccionSistema: " + SeccionSistema;
        }

        public SGJD_ADM_TAB_BITACORA BD() {
            return new SGJD_ADM_TAB_BITACORA() {
                LLF_IdUsuario = IdUsuario,
                FechaHora = FechaHora,
                Accion = Accion,
                ValorAntiguo = ValorAntiguo,
                ValorNuevo = ValorNuevo,
                DireccionIP = DireccionIP,
                DescripcionDispositivo = DescripcionDispositivo,
                SeccionSistema = SeccionSistema
            };
        }
    }
}