using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Models;
using System.Threading.Tasks;

namespace Data.Interface {
	public interface IObjetoDataAccess : IBaseDataAccess<Objeto> {
		Task<Objeto> ObtenerPorIdAsync(int id);
	}
}
