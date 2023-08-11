using SGJD_INA.Models.Helpers;
using System;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class InicioController : Controller {
        public ActionResult Inicio() {
            return View();
        }

        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Archivo")]
        public ActionResult Administracion() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //[AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Abogado, Gerente General, Jefe de despacho, Subgerente Administrativo, Profesional de apoyo")]
        public ActionResult Actas() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //[AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Abogado, Gerente General, Jefe de despacho, Subgerente Administrativo, Profesional de apoyo")]
        public ActionResult Acuerdos() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult E400() {
            return View();
        }

        // Acceso denegado
        [HttpGet]
        [AllowAnonymous]
        public ActionResult E403() {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult E404() {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult E500() {
            return View();
        }
    }
}