using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interface {
	public interface IBaseDataAccess<T> where T : class {
		Task<bool> AgregarAsync(T modelo);

		Task<bool> ActualizarAsync(T modelo);

		Task<bool> EliminarAsync(int id);

		Task<IEnumerable<T>> ObtenerTodosAsync();
	}
}
