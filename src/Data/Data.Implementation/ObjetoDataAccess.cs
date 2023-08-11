using System.Security.Cryptography.X509Certificates;
using System;
using System.Linq;
using System.Linq.Expressions;
using Data.Interface;
using Data.Implementation.EntityFramework;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Implementation {
	public class ObjetoDataAccess : IBaseDataAccess<Objeto>, IObjetoDataAccess {
		private readonly Mozo_DB_Context dbContext;

		public ObjetoDataAccess(Mozo_DB_Context dbContext) {
			this.dbContext = dbContext;
		}

		public async Task<bool> AgregarAsync(Objeto modelo) {
			if (modelo is null) return false;

			TObjetos entidadBD = new TObjetos(modelo);
			dbContext.TObjetos.Add(entidadBD);
			dbContext.Entry(entidadBD).State = EntityState.Added;

			var tareaGuardarCambiosEnBD = dbContext.SaveChangesAsync();
			int registrosAfectados = await tareaGuardarCambiosEnBD;

			return registrosAfectados >= 1;
		}

		public async Task<bool> ActualizarAsync(Objeto modelo) {
			if (modelo is null) return false;

			var tareaObtenerEntidadBD = dbContext.TObjetos.FindAsync(modelo.id);
			TObjetos entidadBD = await tareaObtenerEntidadBD;

			entidadBD.ActualizarCampos(modelo);
			dbContext.Entry(entidadBD).State = EntityState.Modified;

			var tareaGuardarCambiosEnBD = dbContext.SaveChangesAsync();
			int registrosAfectados = await tareaGuardarCambiosEnBD;

			return registrosAfectados >= 1;
		}

		public async Task<bool> EliminarAsync(int id) {
			var tareaObtenerEntidadBD = dbContext.TObjetos.FindAsync(id);
			TObjetos entidadBD = await tareaObtenerEntidadBD;

			dbContext.TObjetos.Remove(entidadBD);
			dbContext.Entry(entidadBD).State = EntityState.Deleted;

			var tareaGuardarCambiosEnBD = dbContext.SaveChangesAsync();
			int registrosAfectados = await tareaGuardarCambiosEnBD;

			return registrosAfectados >= 1;
		}

		public async Task<IEnumerable<Objeto>> ObtenerTodosAsync() {
			var tareaObtenerEntidadesBD = dbContext.TObjetos.AsNoTracking().ToListAsync();
			IEnumerable<TObjetos> listaEntidadesBD = await tareaObtenerEntidadesBD;
			IEnumerable<Objeto> listaObjetos = listaEntidadesBD.Select(x => x.Modelo());
			return listaObjetos;
		}

		public async Task<Objeto> ObtenerPorIdAsync(int id) {
			var tareaObtenerEntidadBD = dbContext.TObjetos.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
			TObjetos entidadBD = await tareaObtenerEntidadBD;
			Objeto modelo = entidadBD.Modelo();
			return modelo;
		}
	}
}
