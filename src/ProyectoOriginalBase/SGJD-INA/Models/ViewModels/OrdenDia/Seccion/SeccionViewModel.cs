using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class SeccionViewModel {
        public SeccionViewModel() { }

        public SeccionViewModel(Seccion ModeloSeccion) {
            IdSeccion = ModeloSeccion.Id;
            NombreSeccion = ModeloSeccion.Descripcion;
            Temas = ModeloSeccion.Temas.Select(Tema => new TemaViewModel(Tema)).ToList();
        }

        // Propiedades
        public int IdSeccion { get; set; }

        public string NombreSeccion { get; set; }

        public IEnumerable<TemaViewModel> Temas { get; set; }
    }
}