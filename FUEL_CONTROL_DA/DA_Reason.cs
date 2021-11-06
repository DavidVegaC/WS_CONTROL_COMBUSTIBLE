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
    public class DA_Reason
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_REASON", sqlParameterArray))
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

        public List<BE_Reason> listarReasonParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Reason> beReasonList = new List<BE_Reason>();            

            
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_REASON_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_Reason beReason = new BE_Reason();

                            beReason.IdReason = DataUtil.ObjectToInt32(dataReader["IdReason"]);
                            beReason.IdProduct = DataUtil.ObjectToInt32(dataReader["IdProduct"]);
                            beReason.ReasonName = DataUtil.ObjectToString(dataReader["ReasonName"]);
                            beReason.ReasonNumber = DataUtil.ObjectToInt32(dataReader["ReasonNumber"]);
                            beReason.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);
                            beReason.ValorConsulta = "1";
                            beReasonList.Add(beReason);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beReasonList.Clear();
                BE_Reason beReason = new BE_Reason();
                beReason.ValorConsulta = "0";
                beReason.MensajeConsulta = ex.Message;
                beReasonList.Add(beReason);
            }
            return beReasonList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idRazon,          
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
                    sqlParameterArray[1] = new SqlParameter("@IdReason", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)idRazon;
                    sqlParameterArray[2] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_REASON_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
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
