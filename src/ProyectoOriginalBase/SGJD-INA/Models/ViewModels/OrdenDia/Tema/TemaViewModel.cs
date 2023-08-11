using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class TemaViewModel {
        public TemaViewModel() { }

        public TemaViewModel(Tema ModeloTema) {
            IdTema = ModeloTema.Id;
            IdEstado = ModeloTema.IdEstado;
            Titulo = ModeloTema.Titulo;
            Resumen = ModeloTema.Resumen;
            Observacion = ModeloTema.Observacion;
        }

        // Propiedades
        public int IdTema { get; set; }

        public int IdEstado { get; set; }

        [Display(Name = "Título del tema")]
        [StringLength(200, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Titulo { get; set; }

        [Display(Name = "Resumen o descripción del tema")]
        public string Resumen { get; set; }

        [Display(Name = "Observaciones al tema")]
        public string Observacion { get; set; }
    }
}