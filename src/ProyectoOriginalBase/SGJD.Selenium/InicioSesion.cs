using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SGJD.Selenium.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD.Selenium {
    public class InicioSesion {
        public InicioSesion() {
            Driver = new FirefoxDriver(@"C:\Users\Terry\Desktop\Datasoft\SGJD\SGJD.Selenium\Drivers\");
        }

        private readonly IWebDriver Driver;

        public bool LlamarInicioSesion() {
            try {
                Driver.Manage().Window.Maximize();
                Driver.Navigate().GoToUrl(VariablesGobales.URL());
                return true;
            }
            catch {
                Driver.Close();
                return false;
            }
        }

        public bool LlenarInicioSesion() {
            try {
                // Usuario
                IWebElement InputUsuario = Driver.FindElement(By.Id(VariablesElementosWeb.IdTxtUsuario()));
                InputUsuario.SendKeys(VariablesUsuario.Usuario());

                // Contraseña
                IWebElement InputContrasenna = Driver.FindElement(By.Id(VariablesElementosWeb.IdTxtContrasena()));
                InputContrasenna.SendKeys(VariablesUsuario.Contrasenna());

                return true;
            }
            catch {
                Driver.Close();
                return false;
            }
        }

        public bool IniciarSesion() {
            try {
                // BtnIngresar
                IWebElement InputBtnIngresar = Driver.FindElement(By.Id(VariablesElementosWeb.IdBtnIngresar()));
                InputBtnIngresar.Click();

                //TODO: Verificar que la pagina que se cargó después de hacer clic es la correcta
                IWebElement VerificarEntrada = Driver.FindElement(By.ClassName(VariablesElementosWeb.NombreClaseImgLogo()));

                if (VerificarEntrada is null) {
                    return false;
                }

                return true;
            }
            catch {
                Driver.Close();
                return false;
            }
        }

        public bool IniciarSesionConUsuario() {
            try {
                LlamarInicioSesion();
                LlenarInicioSesion();
                IniciarSesion();
                return true;
            }
            catch {

                Driver.Close();
                return false;
            }
           

        }
    }
}
