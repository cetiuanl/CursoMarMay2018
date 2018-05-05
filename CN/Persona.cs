using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Persona
    {
        #region Propiedades

        internal string _nombreCompleto;

        public string nombreCompleto
        {
            get { return _nombreCompleto; }
            //set { _nombreCompleto = value; }
        }

        public string direccion { get; set; }

        internal DateTime _fechaNacimiento;

        public DateTime fechaNacimiento
        {
            get { return _fechaNacimiento; }
            //set { _fechaNacimiento = value; }
        }
        
        public int edad
        {
            get {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - fechaNacimiento.Year;

                if (hoy < fechaNacimiento.AddYears(edad))
                {
                    edad--;
                }

                return edad;
            }
        }        

        #endregion

        #region Constructores

        public Persona(string _nombreCompleto,
                        string _direccion,
                        DateTime _fechaNacimiento)
        {
            this._nombreCompleto = _nombreCompleto;
            this.direccion = _direccion;
            this._fechaNacimiento = _fechaNacimiento;
        }

        #endregion

        #region Metodos y Funciones
        public virtual string descripcion()
        {
            string resultado = string.Format("Nombre: {0} - Direccion: {1} - Fecha Nacimiento: {2} - Edad: {3}",
                nombreCompleto, direccion, fechaNacimiento, edad);

            return resultado;
        }

        #endregion        
    }
}
