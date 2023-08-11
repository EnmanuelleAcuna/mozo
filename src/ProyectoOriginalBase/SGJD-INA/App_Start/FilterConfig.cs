using SGJD_INA.Properties;
using System;
using System.Web.Mvc;

namespace SGJD_INA {
    public static class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection Filtros) {
            if (Filtros is null) throw new ArgumentNullException(paramName: nameof(Filtros), message: Resources.ModeloNulo);

            Filtros.Add(new HandleErrorAttribute());
            Filtros.Add(new RequireHttpsAttribute());
            Filtros.Add(new AuthorizeConfig());
        }
    }
}