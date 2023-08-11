using NUnit.Framework;
using SGJD_INA;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD.Tests.Unit {
    [TestFixture]
    public class FuncionesTests {
        [Test]
        public void IsNumber() {
            // Arrange & act
            var SUT = Funciones.IsNumber("1");

            // Assert
            Assert.That(SUT, Is.EqualTo(true));
        }
    }
}