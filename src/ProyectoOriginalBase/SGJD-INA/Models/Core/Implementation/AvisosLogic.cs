using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Implementation {
    public class AvisosLogic : IAvisosLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly IUsuarioLogic Usuario;

        public AvisosLogic(IBitacoraLogic Bitacora, IUsuarioLogic Usuario) {
            this.Bitacora = Bitacora;
            this.Usuario = Usuario;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(Aviso AvisoModel) {
            var AvisoBD = AvisoModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_AVISOS.Add(AvisoBD);

                // Si el tipo de destinatario es [Usuarios] agregar los destinatarios del aviso
                if (AvisoModel.TipoDestinatario is TipoDestinatario.Usuarios && AvisoModel.Usuarios != null) {
                    foreach (ApplicationUser Usuario in AvisoModel.Usuarios) {
                        var TareaObtenerUsuarioBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.FindAsync(Usuario.Id);
                        var UsuarioBD = await TareaObtenerUsuarioBD;
                        AvisoBD.SGJD_ADM_TAB_USUARIOS1.Add(UsuarioBD);
                    }
                }

                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: AvisoModel.ObtenerInformacion(), SeccionSistema: "Avisos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(Aviso AvisoModel) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAvisoBD = ContextoBD.SGJD_ADM_TAB_AVISOS.FindAsync(AvisoModel.Id);
                var AvisoBD = await TareaObtenerAvisoBD;

                // Si el tipo de destinatario es [Usuarios] y el nuevo valor es [Unidad] eliminar los usuarios destinatarios del aviso
                if (AvisoBD.TipoDestinatario.Equals("Usuarios") && AvisoModel.TipoDestinatario is TipoDestinatario.Unidad) {
                    foreach (var UsuarioBD in AvisoBD.SGJD_ADM_TAB_USUARIOS1.ToList()) {
                        AvisoBD.SGJD_ADM_TAB_USUARIOS1.Remove(UsuarioBD);
                    }
                }

                // Si el tipo de destinatario es [Usuarios] agregar los nuevos usuarios destinatarios al aviso
                if (AvisoModel.TipoDestinatario is TipoDestinatario.Usuarios && AvisoModel.Usuarios != null) {
                    AvisoBD.SGJD_ADM_TAB_USUARIOS1.Clear();

                    foreach (ApplicationUser Usuario in AvisoModel.Usuarios) {
                        var TareaObtenerUsuarioBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.FindAsync(Usuario.Id);
                        var UsuarioBD = await TareaObtenerUsuarioBD;
                        AvisoBD.SGJD_ADM_TAB_USUARIOS1.Add(UsuarioBD);
                    }
                }

                // Actualizar campos necesarios
                AvisoBD.Nombre = AvisoModel.Nombre;
                AvisoBD.Mensaje = AvisoModel.Mensaje;
                AvisoBD.Tipo = AvisoModel.Tipo.ToString();
                AvisoBD.TipoDestinatario = AvisoModel.TipoDestinatario.ToString();
                AvisoBD.LLF_IdUnidad = AvisoModel.IdUnidad;

                ContextoBD.Entry(AvisoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Modificar", ValorAntiguo: "", ValorNuevo: AvisoModel.ObtenerInformacion(), SeccionSistema: "Avisos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdAviso) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAvisoBD = ContextoBD.SGJD_ADM_TAB_AVISOS.FindAsync(IdAviso);
                var AvisoBD = await TareaObtenerAvisoBD;
                ContextoBD.SGJD_ADM_TAB_AVISOS.Attach(AvisoBD);
                ContextoBD.SGJD_ADM_TAB_AVISOS.Remove(AvisoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdAviso.ToString(), ValorNuevo: "", SeccionSistema: "Avisos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<Aviso> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaAvisosBD = ContextoBD.SGJD_ADM_TAB_AVISOS.ToList();
                IEnumerable<Aviso> ListaAvisos = ListaAvisosBD.Select(a => new Aviso(a, a.SGJD_ADM_TAB_UNIDADES, a.SGJD_ADM_TAB_USUARIOS1)).ToList();
                return ListaAvisos;
            }
        }

        public Task<IEnumerable<Aviso>> ObtenerTodosAsync() {
            Task<IEnumerable<Aviso>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<Aviso> ObtenerPorIdAsync(int IdAviso) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAvisoBD = ContextoBD.SGJD_ADM_TAB_AVISOS.FindAsync(IdAviso);
                var AvisoBD = await TareaObtenerAvisoBD;
                Aviso ModeloAviso = new Aviso(AvisoBD, AvisoBD.SGJD_ADM_TAB_UNIDADES, AvisoBD.SGJD_ADM_TAB_USUARIOS1);
                return ModeloAviso;
            }
        }

        public async Task<Aviso> ObtenerPorNombreAsync(string NombreAviso) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAvisoBD = ContextoBD.SGJD_ADM_TAB_AVISOS.Where(n => n.Nombre.Equals(NombreAviso)).FirstOrDefaultAsync();
                var AvisoBD = await TareaObtenerAvisoBD;
                Aviso ModeloAviso = new Aviso(AvisoBD, AvisoBD.SGJD_ADM_TAB_UNIDADES, AvisoBD.SGJD_ADM_TAB_USUARIOS1);
                return ModeloAviso;
            }
        }

        public IEnumerable<SelectListItem> ObtenerTiposDeAvisoParaSelect() {
            var ListaTiposDeAviso = Enum.GetNames(typeof(TipoAviso)).Select(name => new SelectListItem() { Value = name, Text = name });
            return ListaTiposDeAviso;
        }

        public IEnumerable<SelectListItem> ObtenerTiposDestinatarioParaSelect() {
            var ListaTiposDeDestinatario = Enum.GetNames(typeof(TipoDestinatario)).Select(name => new SelectListItem() { Value = name, Text = name });
            return ListaTiposDeDestinatario;
        }

        public async Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle) {
            if (ModeloAviso == null) {
                return false;
            }

            IEnumerable<ApplicationUser> ListaUsuariosDestinatarios = new List<ApplicationUser>();

            if (ModeloAviso.TipoDestinatario.ToString().Equals("Usuarios")) {
                // Obtener aviso con usuarios destinatarios
                var TareaObtenerAvisoConUsuariosDestinatarios = ObtenerPorIdAsync(ModeloAviso.Id);
                ModeloAviso = await TareaObtenerAvisoConUsuariosDestinatarios;

                ListaUsuariosDestinatarios = ModeloAviso.Usuarios;
            }

            if (ModeloAviso.TipoDestinatario.ToString().Equals("Unidad")) {
                //Tarea para obtener los usuarios de una unidad
                var TareaObtenerUsuariosUnidad = Usuario.ObtenerTodosPorUnidadAsync(Convert.ToInt32(ModeloAviso.IdUnidad));
                ListaUsuariosDestinatarios = await TareaObtenerUsuariosUnidad;
            }

            if (ListaUsuariosDestinatarios.Any()) {
                ApplicationUserManager UserManager = DependencyResolver.Current.GetService<ApplicationUserManager>();

                foreach (ApplicationUser Usuario in ListaUsuariosDestinatarios) {
                    string Mensaje = ApplicationEmailService.ConfigurarMensajeCorreo(Usuario.Nombre, ModeloAviso.Mensaje, TextoEnlace, Enlace, MensajeDetalle);
                    // Enviar correo
                    await UserManager.SendEmailAsync(Usuario.Id, "Sistema de Gestión de Junta Directiva", Mensaje);
                }

                return true;
            }
            else {
                return true;
            }
        }

        public async Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<ApplicationUser> ListaUsuariosDestinatarios) {
            if (ModeloAviso == null) {
                return false;
            }

            if (ListaUsuariosDestinatarios.Any()) {
                ApplicationUserManager UserManager = DependencyResolver.Current.GetService<ApplicationUserManager>();

                foreach (ApplicationUser Usuario in ListaUsuariosDestinatarios) {
                    string Mensaje = ApplicationEmailService.ConfigurarMensajeCorreo(Usuario.Nombre, ModeloAviso.Mensaje, TextoEnlace, Enlace, MensajeDetalle);
                    // Enviar correo
                    await UserManager.SendEmailAsync(Usuario.Id, "Sistema de Gestión de Junta Directiva", Mensaje);
                }

                return true;
            }

            return true;
        }

        public async Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<CorreoAdicional> ListaDestinatariosAdicionales) {
            if (ModeloAviso == null) {
                return false;
            }

            if (ListaDestinatariosAdicionales.Any()) {
                foreach (CorreoAdicional CorreoAdic in ListaDestinatariosAdicionales) {
                    string Mensaje = ApplicationEmailService.ConfigurarMensajeCorreo(CorreoAdic.Nombre, ModeloAviso.Mensaje, TextoEnlace, Enlace, MensajeDetalle);
                    // Enviar correo
                    await ApplicationEmailService.EnviarCorreo(CorreoAdic.Correo, "Sistema de Gestión de Junta Directiva", Mensaje);
                }

                return true;
            }

            return true;
        }

        public async Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<CorreoUnidadAcuerdo> ListaDestinatariosUnidad) {
            if (ModeloAviso == null) {
                return false;
            }

            if (ListaDestinatariosUnidad.Any()) {
                foreach (CorreoUnidadAcuerdo CorreoUnidad in ListaDestinatariosUnidad) {
                    string Mensaje = ApplicationEmailService.ConfigurarMensajeCorreo(CorreoUnidad.Nombre, ModeloAviso.Mensaje, TextoEnlace, Enlace, MensajeDetalle);
                    // Enviar correo
                    await ApplicationEmailService.EnviarCorreo(CorreoUnidad.Correo, "Sistema de Gestión de Junta Directiva", Mensaje);
                }

                return true;
            }

            return true;
        }

        public async Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<Unidad> ListaUnidades) {
            if (ModeloAviso == null) {
                return false;
            }

            if (ListaUnidades.Any()) {
                foreach (Unidad Unidad in ListaUnidades) {
                    string Mensaje = ApplicationEmailService.ConfigurarMensajeCorreo(Unidad.Nombre, ModeloAviso.Mensaje, TextoEnlace, Enlace, MensajeDetalle);
                    // Enviar correo
                    await ApplicationEmailService.EnviarCorreo(Unidad.Correo, "Sistema de Gestión de Junta Directiva", Mensaje);
                }

                return true;
            }

            return true;
        }

        public async Task<bool> ActualizarUsuarioAvisoAcuerdoAsync(string IdAviso, string IdUsuario) {
            int RegistrosAfectados = 0;

            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener Aviso
                var TareaObtenerAviso = ContextoBD.SGJD_ADM_TAB_AVISOS.FindAsync(Convert.ToInt32(IdAviso));
                var AvisoBD = await TareaObtenerAviso;

                var Usuarios = AvisoBD.SGJD_ADM_TAB_USUARIOS.ToList();

                // Quitar los usuarios relacionados al aviso
                AvisoBD.SGJD_ADM_TAB_USUARIOS.Clear();

                // Guardar en BD
                ContextoBD.Entry(AvisoBD).State = EntityState.Modified;
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados <= 0) {
                    return false;
                }

                // Buscar el nuevo usuario para el aviso
                var tareaObtenerusuario = ContextoBD.SGJD_ADM_TAB_USUARIOS.FindAsync(IdUsuario);
                var UsuarioBD = await tareaObtenerusuario;

                // Agregar relacion del usuario con el aviso
                AvisoBD.SGJD_ADM_TAB_USUARIOS.Add(UsuarioBD);

                // Guardar en BD
                ContextoBD.Entry(AvisoBD).State = EntityState.Modified;
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: "IdAviso :" + IdAviso + ", Idusuario: " + IdUsuario, SeccionSistema: "Usuario Aviso Acuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        #endregion
    }
}