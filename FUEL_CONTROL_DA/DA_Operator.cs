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
    public class DA_Operator
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cn"]].ConnectionString;

        public string validarExistenciaDispositivoMovilParaOperador(string idDispositivo)
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_OPERATOR", sqlParameterArray))
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

        public List<BE_Operator> listarOperadorParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Operator> beOperadorList = new List<BE_Operator>();
            EncryptAndDecryptFile encryptAndDecryptFile = new EncryptAndDecryptFile();
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_OPERATOR_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_Operator beOperator = new BE_Operator();
                            beOperator.IdOperator = DataUtil.ObjectToInt32(dataReader["IdOperator"]);
                            beOperator.OperatorKey = DataUtil.ObjectToString(dataReader["OperatorKey"]);
                            beOperator.OperatorUser = DataUtil.ObjectToString(dataReader["OperatorUser"]);
                            beOperator.OperatorPassword= encryptAndDecryptFile.DecryptFromString(DataUtil.ObjectToString(dataReader["OperatorPassword"]));
                            beOperator.IdPerson = DataUtil.ObjectToInt32(dataReader["IdPerson"]);
                            beOperator.Photocheck = DataUtil.ObjectToString(dataReader["Photocheck"]);
                            beOperator.PersonName = DataUtil.ObjectToString(dataReader["PersonName"]);
                            beOperator.FirstLastName = DataUtil.ObjectToString(dataReader["FirstLastName"]);
                            beOperator.SecondLastName = DataUtil.ObjectToString(dataReader["SecondLastName"]);
                            beOperator.Photocheck = DataUtil.ObjectToString(dataReader["Photocheck"]);
                            beOperator.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);                            
                            beOperator.ValorConsulta = "1";
                            beOperadorList.Add(beOperator);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beOperadorList.Clear();
                BE_Operator bePerson = new BE_Operator();
                bePerson.ValorConsulta = "0";
                bePerson.MensajeConsulta = ex.Message;
                beOperadorList.Add(bePerson);
            }
            return beOperadorList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idUsuario,
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
                    sqlParameterArray[1] = new SqlParameter("@IdOperator", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)idUsuario;
                    sqlParameterArray[2] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_OPERATOR_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
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
