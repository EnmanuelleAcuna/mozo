using Moq;
using NUnit.Framework;
using Mozo.Models.Core.Implementation;
using Mozo.Models.DA.Interfaces;
using Mozo.Models.Entities;
using System.Threading.Tasks;

namespace SGJD.Tests.Unit {
    [TestFixture]
    public class TipoSesionTests {
        private TipoSesion _TipoSesion;
        private TipoSesionLogic _TipoSesionLogic;
        private Mock<ITipoSesionRepository> RepositorioTipoSesion;

        [SetUp]
        public void SetUp() {
            _TipoSesion = new TipoSesion {
                Descripcion = "Tipo de sesión de prueba",
                Id = 32
            };

            RepositorioTipoSesion = new Mock<ITipoSesionRepository>();

            // Agregar
            // Configurar clase Mock para que al ejecutarse la función GuardarAsync, que se encuentra en ITipoSesionRepository
            // que es una tarea que devuelve un bool, para que devuelva un true
            RepositorioTipoSesion.Setup(ts => ts.GuardarAsync(_TipoSesion)).Returns(() => {
                Task<bool> Agregar = Task.Run(() => { return true; });
                return Agregar;
            });

            // Actualizar
            // Configurar clase Mock para que al ejecutarse la función GuardarAsync, que se encuentra en ITipoSesionRepository
            // que es una tarea que devuelve un bool, para que devuelva un true
            RepositorioTipoSesion.Setup(ts => ts.ActualizarAsync(_TipoSesion)).Returns(() => {
                Task<bool> Actualizar = Task.Run(() => { return true; });
                return Actualizar;
            });

            // Eliminar
            // Configurar clase Mock para que al ejecutarse la función GuardarAsync, que se encuentra en ITipoSesionRepository
            // que es una tarea que devuelve un bool, para que devuelva un true
            RepositorioTipoSesion.Setup(ts => ts.EliminarAsync(_TipoSesion.Id)).Returns(() => {
                Task<bool> Eliminar = Task.Run(() => { return true; });
                return Eliminar;
            });

            // ObtenerPorId
            // Configurar clase mock para que al ejecutarse la función ObtenerPorIdAsync, que es una
            // tarea que devuelve una variable de tipo Selecc, devuelva uun Objeto del mismo tipo.
            RepositorioTipoSesion.Setup(x => x.ObtenerPorIdAsync(1)).Returns(() => {
                Task<TipoSesion> Tarea = Task.Run(() => { return _TipoSesion; });
                return Tarea;
            });

            _TipoSesionLogic = new TipoSesionLogic(RepositorioTipoSesion.Object);
        }

        [Test]
        public async Task ValidarAgregar() {
            // Act
            var TareaAgregarTipoSesion = _TipoSesionLogic.AgregarAsync(_TipoSesion);
            bool TipoSesionAgregada = await TareaAgregarTipoSesion;

            // Assert
            Assert.That(TipoSesionAgregada, Is.True);
        }

        [Test]
        public async Task ValidarActualizar() {
            // Act 
            var TareaActualizarTipoSesion = _TipoSesionLogic.ActualizarAsync(_TipoSesion);
            bool TipoSesionActualizada = await TareaActualizarTipoSesion;

            // Assert
            Assert.That(TipoSesionActualizada, Is.True);
        }

        [Test]
        public async Task ValidarEliminar() {
            // Act
            var TareaEliminarTipoSesion = _TipoSesionLogic.EliminarAsync(32);
            bool TipoSesionEliminada = await TareaEliminarTipoSesion;

            // Assert
            Assert.That(TipoSesionEliminada, Is.True);
        }

        [Test]
        public async Task ValidarObtenerPorId() {
            //Act
            var ObtenerPorId = _TipoSesionLogic.ObtenerPorIdAsync(1);
            TipoSesion TipoSesionObtenida = await ObtenerPorId;

            //Assert
            Assert.That(_TipoSesion, Is.EqualTo(TipoSesionObtenida));
        }
    }
}