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
    public class Cliente : Persona
    {
        #region Propiedades
        public int idCliente { get; }
        public string RFC { get; }
        #endregion

        #region Constructores
        public Cliente(int _idCliente, string _RFC, string _nombreCompleto)
            : base(_nombreCompleto, true, DateTime.Now)
        {
            this.idCliente = _idCliente;
            this.RFC = _RFC;            
        }

        public Cliente(DataRow fila) : 
            base(fila.Field<string>("nombreCompleto"), 
                fila.Field<bool>("esActivo"), 
                fila.Field<DateTime>("fechaCreacion"))
        {
            this.idCliente = fila.Field<int>("idCliente");
            this.RFC = fila.Field<string>("RFC");
        }
        #endregion

        #region Metodos y Funciones 
        public void guardar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombreCompleto", nombreCompleto));
            parametros.Add(new SqlParameter("@RFC", RFC));

            try
            {
                if (idCliente > 0)
                {
                    //Update                    
                    parametros.Add(new SqlParameter("@idCliente", idCliente));
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPUClientes", parametros.ToArray()) == 0)
                    {
                        throw new Exception("No se actualizo el registro");
                    }
                }
                else
                {
                    //Insert
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPIClientes", parametros.ToArray()) == 0)
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
        public static void desactivar(int idCliente, bool esActivo = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@idCliente", idCliente));
            parametros.Add(new SqlParameter("@esActivo", esActivo));

            try
            {
                if (DataBaseHelper.ExecuteNonQuery("dbo.SPDClientes", parametros.ToArray()) == 0)
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
        public static Cliente buscarPorID(int idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idCliente", idCliente));

            DataTable dt = new DataTable();

            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSClientes", parametros.ToArray());

                Cliente resultado = null;

                foreach (DataRow fila in dt.Rows)
                {
                    resultado = new Cliente(fila);
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
        public static List<Cliente> traerTodos(bool filtrarSoloActivos = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (filtrarSoloActivos)
            {
                parametros.Add(new SqlParameter("@esActivo", true));
            }

            DataTable dt = new DataTable();

            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSClientes", parametros.ToArray());

                List<Cliente> listado = new List<Cliente>();

                foreach (DataRow fila in dt.Rows)
                {
                    listado.Add(new Cliente(fila));
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
