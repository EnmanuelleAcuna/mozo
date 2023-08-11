using NUnit.Framework;
using SGJD.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD.Tests.Integration {
    [TestFixture]
    public class RecorridoTestsSGJD {
        [Test]
        [Ignore("Test completado en computadora de desarrollo, en CI aún no está configurado el driver")]
        public void ValidarInicioDeSesion() {
            // Arrange
            InicioSesion InicioSesion = new InicioSesion();

            // Act
            bool InicioSesionCargado = InicioSesion.LlamarInicioSesion();
            bool InicioSesionLlenado = InicioSesion.LlenarInicioSesion();
            bool SesionIniciada = InicioSesion.IniciarSesion();

            // Assert
            Assert.That(InicioSesionCargado, Is.True);
            Assert.That(InicioSesionLlenado, Is.True);
            Assert.That(SesionIniciada, Is.True);
        }

        [Test]
        [Ignore("Test incompleto")]
        public void ValidarInicioSesionJD() {
            //Arrage
            InicioSesionJD SesionJD = new InicioSesionJD();

            //Act
            bool InicioSesionJDCargado = SesionJD.IniciarSesionJD();

            //Assert
            Assert.That(InicioSesionJDCargado, Is.True);
        }
    }
}
