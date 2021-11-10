using AccesoDatos;
using FUEL_CONTROL_BE;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_DA
{
    public class DA_Vehicle
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_VEHICLE", sqlParameterArray))
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

        public List<BE_Vehicle> listarVehiculoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Vehicle> beVehicleList = new List<BE_Vehicle>();

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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VEHICLE_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_Vehicle beVehicle = new BE_Vehicle();
                            beVehicle.IdVehicle = DataUtil.ObjectToInt32(dataReader["IdVehicle"]);
                            beVehicle.IdCompany = DataUtil.ObjectToInt32(dataReader["IdCompany"]);
                            beVehicle.IdModel = DataUtil.ObjectToInt32(dataReader["IdModel"]);
                            beVehicle.Plate = DataUtil.ObjectToString(dataReader["Plate"]);
                            beVehicle.VehicleDescription = DataUtil.ObjectToString(dataReader["VehicleDescription"]);
                            beVehicle.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);
                            beVehicle.ValorConsulta = "1";
                            beVehicleList.Add(beVehicle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beVehicleList.Clear();
                BE_Vehicle beVehicle = new BE_Vehicle();
                beVehicle.ValorConsulta = "0";
                beVehicle.MensajeConsulta = ex.Message;
                beVehicleList.Add(beVehicle);
            }
            return beVehicleList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int IdVehicle,
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
                    sqlParameterArray[1] = new SqlParameter("@IdVehicle", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)IdVehicle;
                    sqlParameterArray[2] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VEHICLE_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
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
