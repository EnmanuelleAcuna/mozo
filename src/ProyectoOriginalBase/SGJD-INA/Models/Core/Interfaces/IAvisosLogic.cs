using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IAvisosLogic {
        /// <summary>
        /// Agregar un Aviso
        /// </summary>
        Task<bool> AgregarAsync(Aviso AvisoModel);

        /// <summary>
        /// Actualizar un Aviso
        /// </summary>
        Task<bool> ActualizarAsync(Aviso AvisoModel);

        /// <summary>
        /// Eliminar un Aviso
        /// </summary>
        Task<bool> EliminarAsync(int IdAviso);

        /// <summary>
        /// Obtener todos los Avisos
        /// </summary>
        IEnumerable<Aviso> ObtenerTodos();
        Task<IEnumerable<Aviso>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un Aviso por id
        /// </summary>
        Task<Aviso> ObtenerPorIdAsync(int IdAviso);

        /// <summary>
        /// Obtener un Aviso por nombre
        /// </summary>
        Task<Aviso> ObtenerPorNombreAsync(string Nombre);

        /// <summary>
        /// Obtener los tipos de Aviso
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTiposDeAvisoParaSelect();

        /// <summary>
        /// Obtener los tipos de destinatarios del Aviso
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTiposDestinatarioParaSelect();

        /// <summary>
        /// Envia un aviso por correo siempre y cuando el tipo de aviso sea [Correo]
        /// </summary>
        Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle);
        Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<ApplicationUser> ListaUsuariosDestinatarios);
        Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<CorreoAdicional> ListaDestinatariosAdicionales);
        Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<CorreoUnidadAcuerdo> ListaDestinatariosUnidad);
        Task<bool> EnviarCorreo(Aviso ModeloAviso, string TextoEnlace, string Enlace, string MensajeDetalle, IEnumerable<Unidad> ListaUnidades);

        //Actualizar el usuario de un aviso del acuerdo
        Task<bool> ActualizarUsuarioAvisoAcuerdoAsync(string IdAviso, string IdUsuario);
    }
}