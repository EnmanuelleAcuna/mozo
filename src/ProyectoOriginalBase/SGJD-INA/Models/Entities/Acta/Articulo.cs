using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Articulo {
        // Constructores
        public Articulo() { }

        // Constructor para atributos
        public Articulo(SGJD_ACT_TAB_ARTICULOS ArticuloBD) {
            Id = ArticuloBD.LLP_Id;
            IdTema = ArticuloBD.LLF_IdTema;
            IdCapitulo = ArticuloBD.LLF_IdCapitulo;
            Titulo = ArticuloBD.Titulo;
            Contenido = ArticuloBD.Contenido;
            Confidencial = ArticuloBD.Confidencial;
            Observacion = ArticuloBD.Observacion;
            NombreObjeto = ArticuloBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para atributos y propiedades de navegación padre
        public Articulo(SGJD_ACT_TAB_ARTICULOS ArticuloBD, SGJD_ACT_TAB_CAPITULOS CapituloBD, SGJD_ACT_TAB_TEMAS TemaBD, IEnumerable<SGJD_ACT_TAB_USUARIOS_ARTICULO> UsuariosArticuloBD) {
            Id = ArticuloBD.LLP_Id;
            IdTema = ArticuloBD.LLF_IdTema;
            IdCapitulo = ArticuloBD.LLF_IdCapitulo;
            Titulo = ArticuloBD.Titulo;
            Contenido = ArticuloBD.Contenido;
            Confidencial = ArticuloBD.Confidencial;
            Observacion = ArticuloBD.Observacion;
            NombreObjeto = ArticuloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Capitulo = new Capitulo(CapituloBD, CapituloBD.SGJD_ACT_TAB_ACTAS);
            Tema = new Tema(TemaBD);
            UsuariosArticulo = UsuariosArticuloBD.Select(ua => new UsuarioArticulo(ua, ua.SGJD_ACT_TAB_ARTICULOS, ua.SGJD_ADM_TAB_USUARIOS, ua.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION)).ToList();
        }

        /// <summary>
        /// Constructor para atributos y propiedades de navegacion padre e hijo
        /// </summary>
        public Articulo(SGJD_ACT_TAB_ARTICULOS ArticuloBD, SGJD_ACT_TAB_CAPITULOS CapituloBD, SGJD_ACT_TAB_TEMAS TemaBD, IEnumerable<SGJD_ACT_TAB_USUARIOS_ARTICULO> UsuariosArticuloBD, SGJD_ACU_TAB_ACUERDOS AcuerdoBD) {
            Id = ArticuloBD.LLP_Id;
            IdTema = ArticuloBD.LLF_IdTema;
            IdCapitulo = ArticuloBD.LLF_IdCapitulo;
            Titulo = ArticuloBD.Titulo;
            Contenido = ArticuloBD.Contenido;
            Confidencial = ArticuloBD.Confidencial;
            Observacion = ArticuloBD.Observacion;
            NombreObjeto = ArticuloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Capitulo = new Capitulo(CapituloBD);
            Tema = new Tema(TemaBD);
            UsuariosArticulo = UsuariosArticuloBD.Select(ua => new UsuarioArticulo(ua, ua.SGJD_ACT_TAB_ARTICULOS, ua.SGJD_ADM_TAB_USUARIOS, ua.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION)).ToList();

            Acuerdo = AcuerdoBD != null ? new Acuerdo(AcuerdoBD) : new Acuerdo();
        }

        // Constructor para crear un artículo a partir de un tema de ordel del día
        public Articulo(int IdCapitulo, Tema ModeloTema) {
            this.Id = 0;
            this.IdCapitulo = IdCapitulo;
            this.IdTema = ModeloTema.Id;
            this.Titulo = ModeloTema.Titulo;
            this.Contenido = string.Empty;
            this.Confidencial = false;
            this.Observacion = string.Empty;

            Tema = ModeloTema;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El capitulo es requerido")]
        public int IdCapitulo { get; set; }

        public int IdTema { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El contenido (transcripción) es requerido")]
        public string Contenido { get; set; }

        [Display(Name = "Confidencial")]
        public bool Confidencial { get; set; }

        [Display(Name = "Observación")]
        public string Observacion { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padre
        public Capitulo Capitulo { get; set; }

        public Tema Tema { get; set; }

        // Hijo
        public Acuerdo Acuerdo { get; set; }

        public ICollection<UsuarioArticulo> UsuariosArticulo { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idTema:" + IdTema + ", " +
                "idCapitulo:" + IdCapitulo + ", " +
                "Titulo:" + Titulo + ", " +
                "Contenido:" + Contenido + ", " +
                "Confidencial" + Confidencial + ", " +
                "Observación" + Observacion;
        }

        public SGJD_ACT_TAB_ARTICULOS BD() {
            return new SGJD_ACT_TAB_ARTICULOS() {
                LLP_Id = Id,
                LLF_IdCapitulo = IdCapitulo,
                Titulo = Titulo,
                Contenido = Contenido,
                Confidencial = Confidencial,
                LLF_IdTema = IdTema,
                Observacion = Observacion
            };
        }
    }
}