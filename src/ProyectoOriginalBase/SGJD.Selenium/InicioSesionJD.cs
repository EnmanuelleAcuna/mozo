using OpenQA.Selenium;
using SGJD.Selenium.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD.Selenium {
    public class InicioSesionJD {
        private readonly IWebDriver Driver;

        public bool IniciarSesionJD() {
            try {
                //Se debe entrar a Actas para poder crear una nueva sesion para la JD
                IWebElement BtnActas = Driver.FindElement(By.ClassName(VariablesElementosWeb.NombreClaseActas()));
                BtnActas.Click();
                //Entrar a Sesion de JD
                IWebElement BtnSesionJD = Driver.FindElement(By.XPath(VariablesElementosWeb.XpathSesionJD()));
                BtnSesionJD.Click();

                IWebElement BtnAgregarSesion = Driver.FindElement(By.Id(VariablesElementosWeb.IdBtnIngresarSesion()));
                BtnAgregarSesion.Click();

                return true;
            }
            catch {
                // Driver.Close();

                return false;
            }
        }
    }
}
