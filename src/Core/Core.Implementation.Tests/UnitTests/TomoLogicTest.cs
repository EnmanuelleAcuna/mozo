using Moq;
using NUnit.Framework;
using Mozo.Models.Core.Implementation;
using Mozo.Models.Core.Interfaces;
using Mozo.Models.DA.Interfaces;
using Mozo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD.Tests.Unit {
    [TestFixture]
    public class TomoLogicTest {
        private Tomo _Tomo;
        
        private ITomoLogic _TomoLogic;
        private readonly IBitacoraLogic _Bitacora;
        private readonly ITipoObjetoLogic _TipoObjeto;
        private readonly IEstadoLogic _Estado;
        private readonly IRepositorioLogic _Repositorio;
        private readonly IAvisosLogic _Aviso;

        private Mock<ITomoRepository> RepositorioTomo;

        [SetUp]
        public void SetUp() {
            _Tomo = new Tomo {
                Nombre = "Tomo EA",
                Numero = 56
            };

            RepositorioTomo = new Mock<ITomoRepository>();

            //Configurar clase mock para que al ejecutarse la función Agregar el Tomo por Id, que es una
            // tarea que devuelve una variable de tipo Tomo, devuelva uun Objeto del mismo tipo.
            RepositorioTomo.Setup(x => x.GuardarAsync(_Tomo)).Returns(() => {
                Task<Tomo> Tarea = Task.Run(() => { _Tomo.Id = 2; return _Tomo; });
                return Tarea;
            });

            // Configurar clase mock para que al ejecutarse la función Eliminar, que es una
            // tarea que devuelve un bool, devuelva true
            RepositorioTomo.Setup(x => x.EliminarAsync(2)).Returns(() => {
                Task<bool> Tarea = Task.Run(() => { return true; });
                return Tarea;
            });

            // Configurar clase mock para que al ejecutarse la función Obterner el Tomo por Id , que es una
            // tarea que devuelve una variable de tipo Tomo, devuelva uun Objeto del mismo tipo.
            RepositorioTomo.Setup(x => x.ObtenerPorIdConActas(1)).Returns(() => {
                ICollection<Acta> ActasTomo = new List<Acta> {
                        new Acta{ Id = 101, Encabezado = "Acta de prueba" },
                        new Acta{ Id = 101, Encabezado = "Acta de prueba" }
                    };

                _Tomo.Actas = ActasTomo;

                return _Tomo;
            });


            _TomoLogic = new TomoLogic(_Bitacora, _TipoObjeto, _Estado, _Repositorio, _Aviso, RepositorioTomo.Object);
        }

        [Test]
        public async Task ValidarEliminarTomo() {
            var TareaEliminarTomo = _TomoLogic.EliminarAsync(2);
            bool TomoEliminado = await TareaEliminarTomo;

            //Assert
            Assert.That(TomoEliminado, Is.True);
        }

        [Test]
        public async Task ValidarObtenerPorIdConActasAsync() {
            //Act
            var ObtenerPorId = _TomoLogic.ObtenerPorIdConActasAsync(1);
            Tomo TomoObtenido = await ObtenerPorId;

            //Assert
            Assert.That(_Tomo, Is.EqualTo(TomoObtenido));
        }
    }
}