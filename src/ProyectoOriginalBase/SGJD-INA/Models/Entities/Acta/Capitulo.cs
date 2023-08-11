using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Capitulo {
        // Constructores
        public Capitulo() { }

        // Constructor para atributos
        public Capitulo(SGJD_ACT_TAB_CAPITULOS CapituloBD) {
            Id = CapituloBD.LLP_Id;
            IdActa = CapituloBD.LLF_IdActa;
            Titulo = CapituloBD.Titulo;
            NombreObjeto = CapituloBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para atributos y propiedades de navegación padre
        public Capitulo(SGJD_ACT_TAB_CAPITULOS CapituloBD, SGJD_ACT_TAB_ACTAS ActaBD) {
            Id = CapituloBD.LLP_Id;
            IdActa = CapituloBD.LLF_IdActa;
            Titulo = CapituloBD.Titulo;
            NombreObjeto = CapituloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Acta = new Acta(ActaBD, ActaBD.SGJD_ACT_TAB_SESIONES, ActaBD.SGJD_ACT_TAB_TOMOS, ActaBD.SGJD_ADM_TAB_ESTADOS, ActaBD.SGJD_ACT_TAB_SESIONES1, ActaBD.SGJD_ADM_TAB_USUARIOS, ActaBD.SGJD_ADM_TAB_USUARIOS1);
        }

        /// <summary>
        /// Constructor para atributos y propiedades de navegación hijo
        /// </summary>
        public Capitulo(SGJD_ACT_TAB_CAPITULOS CapituloBD, IEnumerable<SGJD_ACT_TAB_ARTICULOS> ArticulosBD) {
            Id = CapituloBD.LLP_Id;
            IdActa = CapituloBD.LLF_IdActa;
            Titulo = CapituloBD.Titulo;
            NombreObjeto = CapituloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Articulos = ArticulosBD.Select(a => new Articulo(a, a.SGJD_ACT_TAB_CAPITULOS, a.SGJD_ACT_TAB_TEMAS, a.SGJD_ACT_TAB_USUARIOS_ARTICULO, a.SGJD_ACU_TAB_ACUERDOS.OrderByDescending(acu => acu.NumeroVersion).FirstOrDefault())).ToList();
        }

        // Constructor para atributos y propiedades de navegación padre e hijo
        public Capitulo(SGJD_ACT_TAB_CAPITULOS CapituloBD, SGJD_ACT_TAB_ACTAS ActaBD, IEnumerable<SGJD_ACT_TAB_ARTICULOS> ArticulosBD) {
            Id = CapituloBD.LLP_Id;
            IdActa = CapituloBD.LLF_IdActa;
            Titulo = CapituloBD.Titulo;
            NombreObjeto = CapituloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Acta = new Acta(ActaBD);
            Articulos = ArticulosBD.Select(a => new Articulo(a)).ToList();
        }

        // Constructor para crear un capitulo basado en una sección de un orden del día
        public Capitulo(int IdActa, string DescripcionSeccion, IEnumerable<Tema> ListaTemas) {
            this.Id = 0;
            this.IdActa = IdActa;
            this.Titulo = DescripcionSeccion;

            Articulos = ListaTemas.Where(t => !t.Estado.CodigoEstado.Equals("TEM-PEND")).Select(t => new Articulo(this.Id, t)).ToList();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El acta es requerida")]
        public int IdActa { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        public string Titulo { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padre
        public Acta Acta { get; set; }

        // Hijo
        public IEnumerable<Articulo> Articulos { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idActa:" + IdActa + ", " +
                "Titulo:" + Titulo;
        }

        public SGJD_ACT_TAB_CAPITULOS BD() {
            return new SGJD_ACT_TAB_CAPITULOS() {
                LLP_Id = Id,
                LLF_IdActa = IdActa,
                Numero = 0,
                Titulo = Titulo,
                Contenido = string.Empty
            };
        }
    }
}