using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class InicioElementoRelacionadoViewModel {
        //Constructores
        public InicioElementoRelacionadoViewModel() { }

        public InicioElementoRelacionadoViewModel(ElementoRelacionado ModeloElemRelacionado, Acta ModeloActa, Acuerdo ModeloAcuerdo) {
            Id = ModeloElemRelacionado.Id;
            IdActa = (ModeloActa.Id == 0) ? 0 : ModeloElemRelacionado.IdActa;
            IdAcuerdo = (ModeloAcuerdo.Id == 0) ? 0 : ModeloElemRelacionado.IdAcuerdo;
            TipoElemento = ModeloElemRelacionado.TipoElemento;

            TipoSesion = (ModeloActa.Id == 0) ? "" : ModeloActa.Sesion.TipoSesion.Descripcion;
            NumeroSesion = (ModeloActa.Id == 0) ? 0 : ModeloActa.Sesion.Numero;
            FechaAno = (ModeloActa.Id == 0) ? 0 : ModeloActa.Sesion.FechaHora.Year;

            TituloAcuerdo = (ModeloAcuerdo.Id == 0) ? "" : ModeloAcuerdo.Titulo;
        }

        //Propiedades
        public int Id { get; set; }

        public int? IdActa { get; set; }

        public int? IdAcuerdo { get; set; }

        public string TipoElemento { get; set; }

        public string TipoSesion { get; set; }

        public int NumeroSesion { get; set; }

        public int FechaAno { get; set; }

        public string TituloAcuerdo { get; set; }
    }
}