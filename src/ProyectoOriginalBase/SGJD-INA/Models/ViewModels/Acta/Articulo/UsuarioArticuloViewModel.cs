using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class UsuarioArticuloViewModel {
        public UsuarioArticuloViewModel() { }

        public UsuarioArticuloViewModel(UsuarioArticulo ModeloUsuarioArticulo) {
            IdUsuarioArticulo = ModeloUsuarioArticulo.Id;
            IdArticulo = ModeloUsuarioArticulo.IdArticulo;
            IdUsuario = ModeloUsuarioArticulo.Usuario != null ? ModeloUsuarioArticulo.Usuario.Id : ModeloUsuarioArticulo.OtroAsistente.Id.ToString();
            Nombre = ModeloUsuarioArticulo.Usuario != null ? ModeloUsuarioArticulo.Usuario.Nombre : ModeloUsuarioArticulo.OtroAsistente.Nombre;
        }

        // Propiedades
        public int IdUsuarioArticulo { get; set; }

        public int IdArticulo { get; set; }

        public string IdUsuario { get; set; }

        public string Nombre { get; set; }
    }
}