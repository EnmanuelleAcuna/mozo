using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Core.Interface {
	public interface IObjetoLogic {
		/// <summary>
		/// Agregar un objeto
		/// </summary>
		Task<bool> AgregarAsync(Objeto modelo);

		/// <summary>
		/// Actualizar un objeto
		/// </summary>
		Task<bool> ActualizarAsync(Objeto modelo);

		/// <summary>
		/// Eliminar un objeto
		/// </summary>
		Task<bool> EliminarAsync(int idObjeto);

		/// <summary>
		/// Obtener todos los objeto
		/// </summary>
		Task<IEnumerable<Objeto>> ObtenerTodosAsync();

		/// <summary>
		/// Obtener un objeto por su id
		/// </summary>
		Task<Objeto> ObtenerPorIdAsync(int idObjeto);

		/// <summary>
		/// Obtener un objeto por su nombre
		/// </summary>
		Task<Objeto> ObtenerPorNombreAsync(string nombre);

		/// <summary>
		/// Obtener un objeto por su nombre de tabla
		/// </summary>
		Task<Objeto> ObtenerPorTablaAsync(string nombreTabla);
	}
}
