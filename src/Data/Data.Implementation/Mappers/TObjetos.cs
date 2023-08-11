using System;
using System.Collections.Generic;
using System.Text.Json;
using Models;

namespace Data.Implementation.EntityFramework {
	public partial class TObjetos {
		public TObjetos(Objeto modelo) {
			if (modelo is null) throw new ArgumentNullException(paramName: nameof(modelo), message: "Modelo nulo");

			Id = modelo.id;
			Nombre = modelo.nombre;
			Nomenclatura = modelo.nomenclatura;
			Consecutivo = modelo.consecutivo;
			Tabla = modelo.tabla;
		}

		public void ActualizarCampos(Objeto modelo) {
			this.Nombre = modelo.nombre;
			this.Nomenclatura = modelo.nomenclatura;
			this.Consecutivo = modelo.consecutivo;
			this.Tabla = modelo.tabla;
		}

		public Objeto Modelo() {
			return new Objeto {
				id = this.Id,
				nombre = this.Nombre,
				nomenclatura = this.Nomenclatura,
				consecutivo = this.Consecutivo,
				tabla = this.Tabla
			};
		}

		public override string ToString() => JsonSerializer.Serialize(this);
	}
}
