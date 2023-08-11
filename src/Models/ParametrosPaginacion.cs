using System.Text.Json;

namespace Models {
	public class ParametrosPaginacion {
		public int id { get; set; }

		public string nombre { get; set; }

		public string nomenclatura { get; set; }

		public int consecutivo { get; set; }

		public string tabla { get; set; }

		public override string ToString() => JsonSerializer.Serialize(this);
	}
}
