using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD.Selenium.Variables {
    public class VariablesElementosWeb {
        // Inicio de sesion 
        public static string IdTxtUsuario() { return "Usuario"; }

        public static string IdTxtContrasena() { return "Contrasena"; }

        public static string IdBtnIngresar() { return "BtnIniciarSesion"; }

        public static string NombreClaseImgLogo() { return "brand-logo"; }

       //Actas
        public static string NombreClaseActas() { return "card"; }

        //Inicio sesion Junta Directiva
        public static string XpathSesionJD() { return "/html/body/main/div[2]/div[2]/div[1]/a/div"; }
      
        //BtnAgregar una nueva Sesion
        public static string IdBtnIngresarSesion() { return "AgregarSesion"; }
        

    }
}
