using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleAyudaUsuarioViewModel {
        // Constructor
        public DetalleAyudaUsuarioViewModel() { }

        public DetalleAyudaUsuarioViewModel(AyudaUsuario ModeloAyudaUsuario) {
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
    }
}