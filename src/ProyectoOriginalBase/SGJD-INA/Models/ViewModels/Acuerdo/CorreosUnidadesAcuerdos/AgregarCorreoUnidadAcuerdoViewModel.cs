using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarCorreoUnidadAcuerdoViewModel {
        //Atributos
        public int IdCorreoUnidadAcuerdo { get; set; }

        [Display(Name = "Unidad")]
        [Required(ErrorMessage = "La unidad es requerida.")]
        public int IdUnidad { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El correo es requerido.")]
        [StringLength(100, ErrorMessage = "El correo no puede ser mayor a {1} caracteres.")]
        public string Correo { get; set; }

        // Métodos
        public CorreoUnidadAcuerdo Entidad() {
            return new CorreoUnidadAcuerdo {
                Id = IdCorreoUnidadAcuerdo,
                IdUnidad = IdUnidad,
                Nombre = Nombre,
                Correo = Correo
            };
        }
    }
}