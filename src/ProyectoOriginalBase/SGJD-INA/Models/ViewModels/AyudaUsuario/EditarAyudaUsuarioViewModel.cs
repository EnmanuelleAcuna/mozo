using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SGJD_INA.Models.ViewModels {
    public class EditarAyudaUsuarioViewModel {
        // Constructor
        public EditarAyudaUsuarioViewModel() { }

        public EditarAyudaUsuarioViewModel(AyudaUsuario ModeloAyudaUsuario) {
            Id = ModeloAyudaUsuario.Id;
            NombreModulo = ModeloAyudaUsuario.NombreModulo;
            NombreVista = ModeloAyudaUsuario.NombreVista;
            MensajeAyuda = ModeloAyudaUsuario.MensajeAyuda;
            Abreviatura = ModeloAyudaUsuario.Abreviatura;
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre del modulo")]
        public string NombreModulo { get; set; }

        [Display(Name = "Nombre de la vista")]
        public string NombreVista { get; set; }

        [Display(Name = "Mensaje")]
        [AllowHtml]
        public string MensajeAyuda { get; set; }

        [Display(Name = "Abreviatura")]
        public string Abreviatura { get; set; }

        // Métodos
        public AyudaUsuario Entidad() {
            return new AyudaUsuario {
                Id = Id,
                NombreModulo = NombreModulo,
                NombreVista = NombreVista,
                MensajeAyuda = MensajeAyuda,
                Abreviatura = Abreviatura
            };
        }
    }
}