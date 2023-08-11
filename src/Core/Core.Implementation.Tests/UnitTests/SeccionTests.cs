using Moq;
using NUnit.Framework;
using Mozo.Models.Core.Implementation;
using Mozo.Models.DA.Interfaces;
using Mozo.Models.Entities;
using System.Threading.Tasks;

namespace SGJD.Tests.Unit {
    [TestFixture]
    public class SeccionTests {
        private Seccion _Seccion;
        private SeccionLogic _SeccionLogic;
        private Mock<ISeccionRepository> RepositorioSeccion;

        [SetUp]
        public void SetUp() {
            _Seccion = new Seccion {
                Id = 1,
                Descripcion = "Sección de prueba"
            };

            RepositorioSeccion = new Mock<ISeccionRepository>();

            // Agregar
            // Configurar clase mock para que al ejecutarse la función GuardarAsync, que es una
            // tarea que devuelve un bool, devuelva true
            RepositorioSeccion.Setup(x => x.GuardarAsync(_Seccion)).Returns(() => {
                Task<bool> Tarea = Task.Run(() => { return true; });
                return Tarea;
            });

            // Actualizar
            // Configurar clase Mock para que al ejecutarse la función GuardarAsync, que se encuentra en ITipoSesionRepository
            // que es una tarea que devuelve un bool, para que devuelva un true
            RepositorioSeccion.Setup(x => x.ActualizarAsync(_Seccion)).Returns(() => {
                Task<bool> Actualizar = Task.Run(() => { return true; });
                return Actualizar;
            });

            // Eliminar
            // Configurar clase mock para que al ejecutarse la función EliminarAsync, que es una
            // tarea que devuelve un bool, devuelva true
            RepositorioSeccion.Setup(x => x.EliminarAsync(1)).Returns(() => {
                Task<bool> Tarea = Task.Run(() => { return true; });
                return Tarea;
            });

            // ObtenerPorId
            // Configurar clase mock para que al ejecutarse la función ObtenerPorIdAsync, que es una
            // tarea que devuelve una variable de tipo Selecc, devuelva uun Objeto del mismo tipo.
            RepositorioSeccion.Setup(x => x.ObtenerPorIdAsync(1)).Returns(() => {
                Task<Seccion> Tarea = Task.Run(() => { return _Seccion; });
                return Tarea;
            });

            _SeccionLogic = new SeccionLogic(RepositorioSeccion.Object);
        }

        [Test]
        public async Task ValidarAgregar() {
            //Act
            var TareaAgregarSeccion = _SeccionLogic.AgregarAsync(_Seccion);
            bool SeccionAgregada = await TareaAgregarSeccion;

            //Assert
            Assert.That(SeccionAgregada, Is.True);
        }

        [Test]
        public async Task ValidarActualizar() {
            // Act 
            var TareaActualizarSeccion = _SeccionLogic.ActualizarAsync(_Seccion);
            bool SeccionActualizada = await TareaActualizarSeccion;

            // Assert
            Assert.That(SeccionActualizada, Is.True);
        }

        [Test]
        //[Ignore("Test no listo aún")]
        public async Task ValidarEliminar() {
            //Act
            var TareaEliminarSeccion = _SeccionLogic.EliminarAsync(1);
            bool SeccionEliminada = await TareaEliminarSeccion;

            //Assert
            Assert.That(SeccionEliminada, Is.True);
        }

        [Test]
        public async Task ValidarObtenerPorId() {
            //Act
            var ObtenerPorId = _SeccionLogic.ObtenerPorIdAsync(1);
            Seccion SeccionObtenida = await ObtenerPorId;

            //Assert
            Assert.That(_Seccion, Is.EqualTo(SeccionObtenida));
        }
    }
}
