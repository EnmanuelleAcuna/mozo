using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Implementation.EntityFramework
{
    public partial class TObjetos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nomenclatura { get; set; }
        public int Consecutivo { get; set; }
        public string Tabla { get; set; }
    }
}
