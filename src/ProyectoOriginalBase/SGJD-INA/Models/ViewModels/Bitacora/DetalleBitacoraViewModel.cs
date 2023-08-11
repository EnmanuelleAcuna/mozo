using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleBitacoraViewModel {
        #region Constructores
        public DetalleBitacoraViewModel() { }

        public DetalleBitacoraViewModel(Bitacora ModeloBitacora) {
            IdRegistroBitacora = ModeloBitacora.Id;
            NombreUsuario = ModeloBitacora.Usuario.Nombre;
            Fecha = ModeloBitacora.FechaHora;
            Accion = ModeloBitacora.Accion;
            ValorAntiguo = ModeloBitacora.ValorAntiguo;
            ValorNuevo = ModeloBitacora.ValorNuevo;
            IP = ModeloBitacora.DireccionIP;
            Dispositivo = ModeloBitacora.DescripcionDispositivo;
            Seccion = ModeloBitacora.SeccionSistema;
        }
        #endregion

        #region Propiedades
        public int IdRegistroBitacora { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Acción")]
        public string Accion { get; set; }

        [Display(Name = "Valor anterior")]
        public string ValorAntiguo { get; set; }

        [Display(Name = "Nuevo valor")]
        public string ValorNuevo { get; set; }

        public string IP { get; set; }

        [Display(Name = "Descripción")]
        public string Dispositivo { get; set; }

        [Display(Name = "Sección del sistema")]
        public string Seccion { get; set; }
        #endregion
    }
}