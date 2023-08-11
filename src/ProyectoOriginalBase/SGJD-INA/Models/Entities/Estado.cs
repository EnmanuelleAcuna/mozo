using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Estado {
        // Constructores
        public Estado() { }

        public Estado(SGJD_ADM_TAB_ESTADOS EstadoBD) {
            Id = EstadoBD.LLP_Id;
            Nombre = EstadoBD.Nombre;
            Descripcion = EstadoBD.Descripcion;
            CodigoEstado = EstadoBD.CodigoEstado;
            IdTipoObjeto = EstadoBD.LLF_IdTipoObjeto;
            IdAviso = EstadoBD.LLF_IdAviso;
            NombreObjeto = EstadoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public Estado(SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ADM_TAB_TIPOS_OBJETO TipoObjetoBD, SGJD_ADM_TAB_AVISOS AvisoBD) {
            Id = EstadoBD.LLP_Id;
            Nombre = EstadoBD.Nombre;
            Descripcion = EstadoBD.Descripcion;
            CodigoEstado = EstadoBD.CodigoEstado;
            IdTipoObjeto = EstadoBD.LLF_IdTipoObjeto;
            IdAviso = EstadoBD.LLF_IdAviso;
            NombreObjeto = EstadoBD.GetType().UnderlyingSystemType.BaseType.Name;

            TipoObjeto = new TipoObjeto(TipoObjetoBD);
            Aviso = new Aviso(AvisoBD);
        }

        public Estado(SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ADM_TAB_AVISOS AvisoBD) {
            Id = EstadoBD.LLP_Id;
            Nombre = EstadoBD.Nombre;
            Descripcion = EstadoBD.Descripcion;
            CodigoEstado = EstadoBD.CodigoEstado;
            IdTipoObjeto = EstadoBD.LLF_IdTipoObjeto;
            IdAviso = EstadoBD.LLF_IdAviso;
            NombreObjeto = EstadoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Aviso = new Aviso(AvisoBD, AvisoBD.SGJD_ADM_TAB_USUARIOS.FirstOrDefault());
        }

        // Propiedades
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string CodigoEstado { get; set; }

        public int IdTipoObjeto { get; set; }

        public int IdAviso { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padres
        public TipoObjeto TipoObjeto { get; set; }

        public Aviso Aviso { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id: " + Id + ", " +
                "nombre: " + Nombre + "," +
                "descripcion: " + Descripcion + "," +
                "CodigoEstado: " + CodigoEstado + "," +
                "idTipoObjeto: " + IdTipoObjeto + ", " +
                "idAviso: " + IdAviso;
        }

        public SGJD_ADM_TAB_ESTADOS BD() {
            return new SGJD_ADM_TAB_ESTADOS() {
                LLP_Id = Id,
                Nombre = Nombre,
                Descripcion = Descripcion,
                CodigoEstado = CodigoEstado,
                LLF_IdTipoObjeto = IdTipoObjeto,
                LLF_IdAviso = IdAviso
            };
        }
    }
}