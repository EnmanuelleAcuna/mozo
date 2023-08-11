using System.Security.Claims;
using System.Security.Principal;

namespace SGJD_INA {
    public static class ClaimsPrincipalConfig {
        // En esta clase se hace uso de Pattern Matching el cual es una forma
        // de validar si un objeto es de un tipo y asignarlo a una variable
        // para extraerle información si es del tipo que se especifica en la
        // condición
        public static string NombreCompleto(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "NombreCompleto")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }

        public static string IdRol(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "IdRol")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }

        public static string NombreRol(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "NombreRol")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }

        public static string IdUnidad(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "IdUnidad")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }

        public static string NombreUnidad(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "NombreUnidad")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }

        public static string IdTipoUsuario(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "IdTipoUsuario")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }

        public static string NombreTipoUsuario(this IPrincipal User) {
            if (User.Identity.IsAuthenticated) {
                if (User.Identity is ClaimsIdentity ClaimsIdentity) {
                    foreach (var Claim in ClaimsIdentity.Claims) {
                        if (Claim.Type == "NombreTipoUsuario")
                            return Claim.Value;
                    }
                }
            }
            return "";
        }
    }
}