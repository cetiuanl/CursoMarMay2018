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

        public bool esActivo { get; set; }

        internal DateTime _fechaCreacion;

        public DateTime fechaCreacion
        {
            get { return _fechaCreacion; }
            //set { _fechaCreacion = value; }
        }

        public int edad
        {
            get {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - fechaCreacion.Year;

                if (hoy < fechaCreacion.AddYears(edad))
                {
                    edad--;
                }

                return edad;
            }
        }        

        #endregion

        #region Constructores
        public Persona(string _nombreCompleto,
                        bool _esActivo,
                        DateTime _fechaCreacion)
        {
            this._nombreCompleto = _nombreCompleto;
            this.esActivo = _esActivo;
            this._fechaCreacion = _fechaCreacion;
        }
        #endregion     
    }
}
