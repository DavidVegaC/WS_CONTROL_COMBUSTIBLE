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
    public class DA_User
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_USER", sqlParameterArray))
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

        public List<BE_User> listarUsuarioParaDispositivoMovil(string idDispositivo)
        {
            List<BE_User> beUserList = new List<BE_User>();
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_USER_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_User beUser = new BE_User();
                            beUser.IdUser = DataUtil.ObjectToInt32(dataReader["IdUser"]);
                            beUser.IdPerson = DataUtil.ObjectToInt32(dataReader["IdPerson"]);
                            beUser.Photocheck = DataUtil.ObjectToString(dataReader["Photocheck"]);
                            beUser.PersonName = DataUtil.ObjectToString(dataReader["PersonName"]);
                            beUser.FirstLastName = DataUtil.ObjectToString(dataReader["FirstLastName"]);
                            beUser.SecondLastName = DataUtil.ObjectToString(dataReader["SecondLastName"]);
                            beUser.UUser = DataUtil.ObjectToString(dataReader["UUser"]);
                            beUser.UPassword = encryptAndDecryptFile.DecryptFromString(DataUtil.ObjectToString(dataReader["UPassword"]));
                            beUser.ULevel = DataUtil.ObjectToInt32(dataReader["ULevel"]);
                            beUser.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);
                            beUser.ValorConsulta = "1";
                            beUserList.Add(beUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beUserList.Clear();
                BE_User beUser = new BE_User();
                beUser.ValorConsulta = "0";
                beUser.MensajeConsulta = ex.Message;
                beUserList.Add(beUser);
            }
            return beUserList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idUsuario,
          int Ulevel,
          int idUsuarioRegistro)
        {
            string str = "";
            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(this.cadenaConexion))
                {
                    SqlParameter[] sqlParameterArray = new SqlParameter[4]
                    {
                        new SqlParameter("@IdMovilDevice", SqlDbType.VarChar),
                        null,
                        null,
                        null
                    };
                    sqlParameterArray[0].Direction = ParameterDirection.Input;
                    sqlParameterArray[0].Value = (object)idDispositivo;
                    sqlParameterArray[1] = new SqlParameter("@IdUser", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)idUsuario;
                    sqlParameterArray[2] = new SqlParameter("@Ulevel", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)Ulevel;
                    sqlParameterArray[3] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[3].Direction = ParameterDirection.Input;
                    sqlParameterArray[3].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_USER_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
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
