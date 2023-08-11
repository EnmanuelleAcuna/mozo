﻿using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarOtroAsistenteViewModel {
        // constructor 
        public EditarOtroAsistenteViewModel() { }

        public EditarOtroAsistenteViewModel(OtroAsistenteSesion ModeloOtroAsistenteSesion) {
            Id = ModeloOtroAsistenteSesion.Id;
            IdSesion = ModeloOtroAsistenteSesion.IdSesion;
            IdTipoUsuario = ModeloOtroAsistenteSesion.IdTipoUsuario;
            //TipoUsuario = ModeloOtroAsistenteSesion.TipoUsuario.Nombre;
            Nombre = ModeloOtroAsistenteSesion.Nombre;
            Puesto = ModeloOtroAsistenteSesion.Puesto;
            HoraInicio = ModeloOtroAsistenteSesion.HoraInicio;
            HoraFin = ModeloOtroAsistenteSesion.HoraFin;
            NombreObjeto = ModeloOtroAsistenteSesion.NombreObjeto;
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "La sesión es requerida")]
        [Display(Name = "Sesión")]
        public int IdSesion { get; set; }

        [Required(ErrorMessage = "El tipo usuario es requerido")]
        [Display(Name = "Tipo Usuario")]
        public int IdTipoUsuario { get; set; }

        //[Display(Name = "Nombre Tipo Usuario")]
        //public string TipoUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El puesto es requerido")]
        [Display(Name = "Puesto")]
        public string Puesto { get; set; }

        [Required(ErrorMessage = "La hora de ingreso es requerida")]
        [Display(Name = "Hora de ingreso")]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de salida es requerida")]
        [Display(Name = "Hora de salida")]
        public string HoraFin { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public OtroAsistenteSesion Entidad() {
            return new OtroAsistenteSesion {
                Id = Id,
                IdSesion = IdSesion,
                IdTipoUsuario = IdTipoUsuario,
                Nombre = Nombre,
                Puesto = Puesto,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin
            };
        }


    }
}