using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioBitacoraViewModel {
        #region Constructores
        public InicioBitacoraViewModel() { }

        public InicioBitacoraViewModel(Bitacora ModeloBitacora) {
            IdRegistroBitacora = ModeloBitacora.Id;
            NombreUsuario = ModeloBitacora.Usuario.Nombre;
            Fecha = ModeloBitacora.FechaHora.ToString("dd/MM/yyyy hh:mm:ss tt");
            Accion = ModeloBitacora.Accion;
            Seccion = ModeloBitacora.SeccionSistema;
        }

        public InicioBitacoraViewModel(BitacoraPorFechaDTO ModeloBitacora) {
            IdRegistroBitacora = ModeloBitacora.Id;
            NombreUsuario = ModeloBitacora.NombreUsuario;
            Fecha = ModeloBitacora.FechaHora.ToString("dd/MM/yyyy hh:mm:ss tt");
            Accion = ModeloBitacora.Accion;
            Seccion = ModeloBitacora.SeccionSistema;
        }
        #endregion

        #region Propiedades
        public int IdRegistroBitacora { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Fecha")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")] // Especificar explicitamente formato de texto
        public string Fecha { get; set; }

        [Display(Name = "Acción")]
        public string Accion { get; set; }

        [Display(Name = "Sección del sistema")]
        public string Seccion { get; set; }
        #endregion
    }
}