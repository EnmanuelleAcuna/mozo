using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class VistaPerfilRepository : IVistaPerfilRepository {
        public async Task<bool> AgregarAsync(VistaPerfil Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var VistaPerfilBD = ConvertirModelo_BD(Modelo);

                ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Add(VistaPerfilBD);
                int RegistroAfectados = await ContextoBD.SaveChangesAsync();

                return (RegistroAfectados >= 1) ? true : false;
            }
        }

        public async Task<bool> ActualizarAsync(VistaPerfil Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var VistaPerfilBD = ConvertirModelo_BD(Modelo);

                ContextoBD.Entry(VistaPerfilBD).State = EntityState.Modified;
                int RegistroAfectados = await ContextoBD.SaveChangesAsync();

                return (RegistroAfectados >= 1) ? true : false;
            }
        }

        public async Task<bool> EliminarAsync(VistaPerfil Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var VistaPerfilBD = ConvertirModelo_BD(Modelo);

                ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Attach(VistaPerfilBD);
                ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Remove(VistaPerfilBD);
                int RegistroAfectados = await ContextoBD.SaveChangesAsync();

                return (RegistroAfectados >= 1) ? true : false;
            }
        }

        public async Task<VistaPerfil> ObtenerPorIdAsync(string IdPerfil, int IdVista) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerVistPerfilBD = ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Where(m => m.LLF_IdRol.Equals(IdPerfil) && m.LLF_IdVista == IdVista).FirstOrDefaultAsync();
                var VistaPerfilBD = await TareaObtenerVistPerfilBD;
                var Modelo = ConvertirBD_Modelo(VistaPerfilBD);
                return Modelo;
            }
        }

        #region Metodos de conversión
        internal SGJD_ADM_TAB_VISTAS_PERFIL ConvertirModelo_BD(VistaPerfil vistaPerfilModel) {
            return new SGJD_ADM_TAB_VISTAS_PERFIL() {
                LLF_IdRol = vistaPerfilModel.IdPerfil,
                LLF_IdVista = vistaPerfilModel.IdVista,
                Permiso = vistaPerfilModel.Permiso
            };
        }

        internal VistaPerfil ConvertirBD_Modelo(SGJD_ADM_TAB_VISTAS_PERFIL vistaPerfilBD) {
            return new VistaPerfil() {
                IdPerfil = vistaPerfilBD.LLF_IdRol,
                IdVista = vistaPerfilBD.LLF_IdVista,
                Permiso = vistaPerfilBD.Permiso,
                NombreObjeto = vistaPerfilBD.GetType().UnderlyingSystemType.BaseType.Name,
                Perfil = new ApplicationRole() {
                    Id = vistaPerfilBD.SGJD_ADM_TAB_ROLES.Id,
                    Name = vistaPerfilBD.SGJD_ADM_TAB_ROLES.Name
                },
                Vista = new Vista() {
                    Id = vistaPerfilBD.SGJD_ADM_TAB_VISTAS.LLP_Id,
                    NombreVista = vistaPerfilBD.SGJD_ADM_TAB_VISTAS.NombreVista,
                    DireccionVista = vistaPerfilBD.SGJD_ADM_TAB_VISTAS.DireccionVista
                }
            };
        }
        #endregion

    }
}