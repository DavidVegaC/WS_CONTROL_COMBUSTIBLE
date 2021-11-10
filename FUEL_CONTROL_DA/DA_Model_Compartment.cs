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
    public class DA_Model_Compartment
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_MODEL_COMPARTMENT", sqlParameterArray))
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

        public List<BE_Model_Compartment> listarModeloCompartimientoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Model_Compartment> beModelCompartmentList = new List<BE_Model_Compartment>();

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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_MODEL_COMPARTMENT_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_Model_Compartment beModelCompartment = new BE_Model_Compartment();
                            beModelCompartment.IdModelCompartment = DataUtil.ObjectToInt32(dataReader["IdModelCompartment"]);
                            beModelCompartment.IdModel = DataUtil.ObjectToInt32(dataReader["IdModel"]);
                            beModelCompartment.IdCompartment = DataUtil.ObjectToInt32(dataReader["IdCompartment"]);
                            beModelCompartment.CompartmentNumber = DataUtil.ObjectToInt32(dataReader["CompartmentNumber"]);
                            beModelCompartment.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);
                            beModelCompartment.ValorConsulta = "1";
                            beModelCompartmentList.Add(beModelCompartment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beModelCompartmentList.Clear();
                BE_Model_Compartment beModelCompartment = new BE_Model_Compartment();
                beModelCompartment.ValorConsulta = "0";
                beModelCompartment.MensajeConsulta = ex.Message;
                beModelCompartmentList.Add(beModelCompartment);
            }
            return beModelCompartmentList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int IdModelCompartimiento,
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
                    sqlParameterArray[1] = new SqlParameter("@IdModelCompartment", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)IdModelCompartimiento;
                    sqlParameterArray[2] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_MODEL_COMPARTMENT_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
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
