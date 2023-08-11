using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Clase para realizar operaciones relacionadas a la clase emcabezado y pie de pagina
    /// </summary>
    public class TamanoLetraLogic : ITamanoLetraLogic {
        /// <summary>
        /// Obtener listado de todos los tamaños de letra disponible para editor HTML
        /// </summary>
        public IEnumerable<TamanoLetra> ObtenerTodos() {
            var listaStringModel = new List<TamanoLetra>();

            var tipoArchivo1 = new TamanoLetra { Id = "1", Valor = "8" };
            var tipoArchivo2 = new TamanoLetra { Id = "2", Valor = "10" };
            var tipoArchivo3 = new TamanoLetra { Id = "3", Valor = "12" };
            var tipoArchivo4 = new TamanoLetra { Id = "4", Valor = "14" };
            var tipoArchivo5 = new TamanoLetra { Id = "5", Valor = "18" };
            var tipoArchivo6 = new TamanoLetra { Id = "6", Valor = "24" };
            var tipoArchivo7 = new TamanoLetra { Id = "7", Valor = "36" };

            listaStringModel.Add(tipoArchivo1);
            listaStringModel.Add(tipoArchivo2);
            listaStringModel.Add(tipoArchivo3);
            listaStringModel.Add(tipoArchivo4);
            listaStringModel.Add(tipoArchivo5);
            listaStringModel.Add(tipoArchivo6);
            listaStringModel.Add(tipoArchivo7);

            return listaStringModel;
        }
    }
}