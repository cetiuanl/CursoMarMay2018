using CD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Empleado : Persona
    {
        #region Propiedades
        public string usuario { get; }
        public int idEmpleado { get; }
        public string contrasena { get; }
        public int idRol { get; }
        #endregion

        #region Constructores
        public Empleado(string _usuario,
            int _idEmpleado,
            string _contrasena,
            int _idRol,
            string _nombreCompleto, 
            string _direccion, 
            DateTime _fechaCrea)
            : base(_nombreCompleto, _direccion, _fechaCrea)
        {
            this.usuario = _usuario;
            this.idEmpleado = _idEmpleado;
            this.contrasena = _contrasena;
            this.idRol = _idRol;
        }
        public Empleado(DataRow fila)
            : base(fila.Field<string>("nombreCompleto"),
            fila.Field<string>("direccion"),
            fila.Field<DateTime>("fechaCreacion"))
        {
            usuario = fila.Field<string>("usuario");
            idEmpleado = fila.Field<int>("idEmpleado");
            contrasena = fila.Field<string>("contrasena");
            idRol = fila.Field<int>("idRol");
        }

        #endregion

        #region Metodos y Funciones 
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombreCompleto", nombreCompleto));
            parametros.Add(new SqlParameter("@usuario", usuario));
            parametros.Add(new SqlParameter("@contrasena", contrasena));
            parametros.Add(new SqlParameter("@idRol", idRol));
            parametros.Add(new SqlParameter("@direccion", direccion));

            try
            {
                if (idEmpleado > 0)
                {
                    //Update                    
                    parametros.Add(new SqlParameter("@idEmpleado", idEmpleado));
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPUEmpleados", parametros.ToArray()) == 0)
                    {
                        throw new Exception("No se actualizo el registro");
                    }
                }
                else
                {
                    //Insert
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPIEmpleados", parametros.ToArray()) == 0)
                    {
                        throw new Exception("No se creo el registro");
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }
        }
        public static void desactivar(int idEmpleado, bool esActivo = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@idEmpleado", idEmpleado));
            parametros.Add(new SqlParameter("@esActivo", esActivo));

            try
            {
                if (DataBaseHelper.ExecuteNonQuery("dbo.SPDEmpleados", parametros.ToArray()) == 0)
                {
                    throw new Exception("No se desactivo el registro");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }
        }
        public static Empleado buscarPorID(int idEmpleado)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idEmpleado", idEmpleado));

            DataTable dt = new DataTable();

            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSEmpleados", parametros.ToArray());

                Empleado resultado = null;

                foreach (DataRow fila in dt.Rows)
                {
                    resultado = new Empleado(fila);
                    break;
                }

                if (resultado == null)
                {
                    throw new Exception("No se han encontrado coincidencias.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }
        }
        public static List<Empleado> traerTodos(bool filtrarSoloActivos = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (filtrarSoloActivos)
            {
                parametros.Add(new SqlParameter("@esActivo", true));
            }

            DataTable dt = new DataTable();

            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSEmpleados", parametros.ToArray());

                List<Empleado> listado = new List<Empleado>();

                foreach (DataRow fila in dt.Rows)
                {
                    listado.Add(new Empleado(fila));
                }

                return listado;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }
        }
        #endregion
    }
}
