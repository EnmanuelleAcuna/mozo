using Moq;
using NUnit.Framework;
using SGJD_INA.Models.Core.Implementation;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD.Tests.Unit {
    [TestFixture]
    class VistaPerfilLogicTest {
        private VistaPerfil _VistaPerfil;
        private VistaPerfilLogic _VistaPerfilLogic;
        private Mock<IVistaPerfilRepository> RepositorioVistaPerfil;

        [SetUp]
        public void SetUp() {
            _VistaPerfil = new VistaPerfil { IdPerfil = "721lnd-287328", IdVista = 1, Permiso = 7 };

            RepositorioVistaPerfil = new Mock<IVistaPerfilRepository>();

            // Configurar clase mock para que al ejecutarse la función Agregar, que es una
            // tarea que devuelve un bool, devuelva true
            RepositorioVistaPerfil.Setup(x => x.AgregarAsync(_VistaPerfil)).Returns(() => {
                Task<bool> Tarea = Task.Run(() => { return true; });
                return Tarea;
            });

            // Configurar clase mock para que al ejecutarse la función Eliminar, que es una
            // tarea que devuelve un bool, devuelva true
            RepositorioVistaPerfil.Setup(x => x.EliminarAsync(_VistaPerfil)).Returns(() => {
                Task<bool> Tarea = Task.Run(() => { return true; });
                return Tarea;
            });

            // Configurar clase mock para que al ejecutarse la función Obterner el perfil y vista por Id , que es una
            // tarea que devuelve una variable de tipo Vistaperfil, devuelva uun Objeto del mismo tipo.
            RepositorioVistaPerfil.Setup(x => x.ObtenerPorIdAsync("Jaki", 1)).Returns(() => {
                Task<VistaPerfil> Tarea = Task.Run(() => { return _VistaPerfil; });
                return Tarea;
            });

            _VistaPerfilLogic = new VistaPerfilLogic(RepositorioVistaPerfil.Object);
        }

        [Test]
        public async Task ValidarAgregar() {
            //Act
            var TareaAgregarVistaPerfil = _VistaPerfilLogic.AgregarAsync(_VistaPerfil);
            bool VistaperfilAgregada = await TareaAgregarVistaPerfil;

            //Assert
            Assert.That(VistaperfilAgregada, Is.True);
        }

        [Test]
        public async Task ValidarEliminar() {
            var TareaEliminarVistaPerfil = _VistaPerfilLogic.EliminarAsync(_VistaPerfil);
            bool VistaperfilEliminada = await TareaEliminarVistaPerfil;

            //Assert
            Assert.That(VistaperfilEliminada, Is.True);
        }

        [Test]
        public async Task ValidarObtenerTodosPorId() {
            //Act
            var ObtenerPorId = _VistaPerfilLogic.ObtenerPorIdAsync("Jaki", 1);
            VistaPerfil VistaPerfilObtenido = await ObtenerPorId;

            //Assert
            Assert.That(_VistaPerfil, Is.EqualTo(VistaPerfilObtenido));
        }
    }
}
