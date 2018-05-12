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
    public class Rol
    {
        #region Propiedades
        public string nombre { get; }
        public int idRol { get; }
        public string descripcion { get; }
        public bool esAtivo { get; }
        public DateTime fechaCreacion { get; }
        #endregion

        #region Constructores
        public Rol(string _nombre, int _idRol, string _descripcion)
        {
            this.nombre = _nombre;
            this.descripcion = _descripcion;
            this.idRol = _idRol;
        }
        public Rol(DataRow fila)
        {
            nombre = fila.Field<string>("nombre");
            idRol = fila.Field<int>("idRol");
            descripcion = fila.Field<string>("descripcion");
            esAtivo = fila.Field<bool>("esActivo");
        }
        #endregion
        
        #region Metodos Y Funciones 
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@nombre", nombre));
            parametros.Add(new SqlParameter("@descripcion", descripcion));
            parametros.Add(new SqlParameter("@esActivo", esAtivo));

            try
            {
                if (idRol > 0)
                {
                    //Update
                    parametros.Add(new SqlParameter("@idRol", idRol));
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPURoles", parametros.ToArray()) == 0)
                    {
                        throw new Exception("No se actualizo el registro");
                    }
                }
                else
                {
                    // Insert 
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPIRoles", parametros.ToArray()) == 0)
                    {
                        throw new Exception("No se creo el registro ");
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de dato ");
#endif
            }
        }

        public static void desactivar(int idRol, bool esActivo = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@idRol", idRol));
            parametros.Add(new SqlParameter("@esActivo", esActivo));

            try
            {
                if (DataBaseHelper.ExecuteNonQuery("dbo.SPDRoles", parametros.ToArray()) == 0)
                {
                    throw new Exception("No se actualizo el registro");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de dato ");
#endif
            }
        }
        public static Rol buscarPorID(int idRol)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idRol", idRol));

            DataTable dt = new DataTable();

            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPRoles", parametros.ToArray());

                Rol resultado = null;

                foreach (DataRow fila in dt.Rows)
                {
                    resultado = new Rol(fila);
                    break;
                }

                if (resultado == null)
                {
                    throw new Exception("No se han Encontrado Coincidencia");
                }

                return resultado;

            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de dato ");
#endif
            }
        }

        public static List<Rol> traerTodos(bool filtrarSoloActivos = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (filtrarSoloActivos)
            {
                parametros.Add(new SqlParameter("@esActivo", true));
            }

            DataTable dt = new DataTable();

            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSRoles", parametros.ToArray());

                List<Rol> listado = new List<Rol>();

                foreach (DataRow fila in dt.Rows)
                {
                    listado.Add(new Rol(fila));
                }

                return listado;

            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de dato ");
#endif
            }
        }
        #endregion
    }
}
