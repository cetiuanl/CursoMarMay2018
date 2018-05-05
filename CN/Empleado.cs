using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Empleado : Persona
    {
        #region Propiedades
        public string usuario { get; set; }
        public int idEmpleado { get; set; }
        #endregion

        #region Constructores
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
        public Empleado(DataRow fila)
            : base(fila.Field<string>("nombreCompleto"),
            fila.Field<string>("direccion"),
            fila.Field<DateTime>("fechaNacimiento"))
        {
            usuario = fila.Field<string>("usuario");
            idEmpleado = fila.Field<int>("idEmpleado");
        }

        #endregion

        #region Metodos y Funciones
        public override string descripcion()
        {
            string result = String.Format("usuario:{0} - idEmpleado: {1} - {2}",
                usuario, idEmpleado, base.descripcion());

            return result;
        }
        #endregion
    }
}
