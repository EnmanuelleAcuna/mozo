using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleSeguimientoViewModel {
        //Constructores
        public DetalleSeguimientoViewModel(DetalleSeguimiento ModeloDetalleSeguimiento) {
            IdDetalle = ModeloDetalleSeguimiento.Id;
            Fecha = ModeloDetalleSeguimiento.FechaModificacion.ToShortDateString();
            NombreUsuario = ModeloDetalleSeguimiento.Usuario.Nombre;
            Descripcion = ModeloDetalleSeguimiento.Descripcion;
        }

        #region Atributos
        public int IdDetalle { get; set; }

        [Display(Name = "Fecha del avance")]
        public string Fecha { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        #endregion
    }
}