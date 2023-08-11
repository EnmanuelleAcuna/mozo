using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InicioTelefonoCorreoViewModel {
        // Constructores
        public InicioTelefonoCorreoViewModel(string IdUsuario, string NombreUsuario, IEnumerable<UsuarioTelefono> Telefonos, IEnumerable<UsuarioCorreo> Correos) {
            this.IdUsuario = IdUsuario;
            this.NombreUsuario = NombreUsuario;
            this.Telefonos = Telefonos.Select(t => new TelefonoUsuarioViewModel(t)).ToList();
            this.Correos = Correos.Select(c => new CorreoUsuarioViewModel(c)).ToList();
        }

        // Propiedades
        public string IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public IEnumerable<TelefonoUsuarioViewModel> Telefonos { get; set; }

        public IEnumerable<CorreoUsuarioViewModel> Correos { get; set; }
    }
}