using FUEL_CONTROL_BE;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using AccesoDatos;

namespace FUEL_CONTROL_DA
{
    public class DA_Product
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cn"]].ConnectionString;

        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            string str = "";
            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(this.cadenaConexion))
                {
                    SqlParameter[] sqlParameterArray = new SqlParameter[1]
                    {
                        new SqlParameter("@IdMovilDevice", SqlDbType.VarChar)
                    };
                    sqlParameterArray[0].Direction = ParameterDirection.Input;
                    sqlParameterArray[0].Value = (object)idDispositivo;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_PRODUCT", sqlParameterArray))
                    {
                        while (dataReader.Read())
                            str = DataUtil.ObjectToString(dataReader["Resultado"]);
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        public List<BE_Product> listarProductoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Product> beProductList = new List<BE_Product>();            

            
            try
            {

                SqlConnection connection;
                using (connection = new SqlConnection(this.cadenaConexion))
                {
                    SqlParameter[] sqlParameterArray = new SqlParameter[1]
                    {
                        new SqlParameter("@IdMovilDevice", SqlDbType.VarChar)
                    };
                    sqlParameterArray[0].Direction = ParameterDirection.Input;
                    sqlParameterArray[0].Value = (object)idDispositivo;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_PRODUCT_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_Product beProduct = new BE_Product();
                            beProduct.IdProduct = DataUtil.ObjectToInt32(dataReader["IdProduct"]);
                            beProduct.NumberProduct = DataUtil.ObjectToInt32(dataReader["NumberProduct"]);
                            beProduct.ProductName = DataUtil.ObjectToString(dataReader["ProductName"]);
                            beProduct.MeasurementUnit = DataUtil.ObjectToString(dataReader["MeasurementUnit"]);
                            beProduct.ElipseCode = DataUtil.ObjectToString(dataReader["ElipseCode"]);
                            beProduct.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);
                            beProduct.ValorConsulta = "1";
                            beProductList.Add(beProduct);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beProductList.Clear();
                BE_Product beProduct = new BE_Product();
                beProduct.ValorConsulta = "0";
                beProduct.MensajeConsulta = ex.Message;
                beProductList.Add(beProduct);
            }
            return beProductList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idProducto,          
          int idUsuarioRegistro)
        {
            string str = "";
            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(this.cadenaConexion))
                {
                    SqlParameter[] sqlParameterArray = new SqlParameter[3]
                    {
                        new SqlParameter("@IdMovilDevice", SqlDbType.VarChar),
                        null,
                        null
                    };
                    sqlParameterArray[0].Direction = ParameterDirection.Input;
                    sqlParameterArray[0].Value = (object)idDispositivo;
                    sqlParameterArray[1] = new SqlParameter("@IdProduct", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)idProducto;
                    sqlParameterArray[2] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_PRODUCT_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                            str = DataUtil.ObjectToString(dataReader["Resultado"]);
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }
    }
}
