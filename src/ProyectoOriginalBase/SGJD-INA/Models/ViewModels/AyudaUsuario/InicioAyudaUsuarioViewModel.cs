using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class InicioAyudaUsuarioViewModel {
        // Constructor
        public InicioAyudaUsuarioViewModel(AyudaUsuario ModeloAyudaUsuario) {
            IdAyudaUsuario = ModeloAyudaUsuario.Id;
            NombreVista = ModeloAyudaUsuario.NombreVista;
            NombreModulo = ModeloAyudaUsuario.NombreModulo;
            Abreviatura = ModeloAyudaUsuario.Abreviatura;
        }

        // Propiedades
        public int IdAyudaUsuario { get; set; }

        [Display(Name = "Nombre de vista")]
        public string NombreVista { get; set; }

        [Display(Name = "Nombre de Modulo")]
        public string NombreModulo { get; set; }

        public string Abreviatura { get; set; }
    }
}