using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTrump
{
    public class clsImpuestos
    {
        public clsImpuestos(decimal tipo, decimal valor, string nombre, string nomencla, string descr)
        {
            this.tipo = tipo;
            this.valor = valor;
            this.nombre = nombre;
            this.nomencla = nomencla;
            this.descr = descr;
        }

        public decimal tipo { get; set; }
        public decimal valor { get; set; }
        public string nombre { get; set; }
        public string nomencla { get; set; }
        public string descr { get; set; }

    }
}
