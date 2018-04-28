using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Empleado : Persona
    {
        public string usuario { get; set; }
        public int idEmpleado { get; set; }

        public Empleado(string _usuario,
            int _idEmpleado,
            string _nombreCompleto, 
            string _direccion, 
            DateTime _fechaNac)
            : base(_nombreCompleto, _direccion, _fechaNac)
        {
            this.usuario = _usuario;
            this.idEmpleado = _idEmpleado;
        }

        public override string descripcion()
        {
            string result = String.Format("usuario:{0} - idEmpleado: {1} - {2}",
                usuario, idEmpleado, base.descripcion());

            return result;
        }
    }
}
