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
    public class DA_Compartment
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_VALIDATE_MOVIL_DEVICE_FOR_COMPARTMENT", sqlParameterArray))
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

        public List<BE_Compartment> listarCompartimientoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Compartment> beCompartmentList = new List<BE_Compartment>();            
            
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
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_COMPARTMENT_LIST_FOR_MOVIL_DEVICE", sqlParameterArray))
                    {
                        while (dataReader.Read())
                        {
                            BE_Compartment beCompartment = new BE_Compartment();
                            beCompartment.IdCompartment = DataUtil.ObjectToInt32(dataReader["IdCompartment"]);
                            beCompartment.IdProduct = DataUtil.ObjectToInt32(dataReader["IdProduct"]);
                            beCompartment.IdCompartmentType = DataUtil.ObjectToInt32(dataReader["IdCompartmentType"]);
                            beCompartment.CompartmentName = DataUtil.ObjectToString(dataReader["CompartmentName"]);
                            beCompartment.Capacity = DataUtil.ObjectToDouble(dataReader["Capacity"]);
                            beCompartment.AlertCapacity = DataUtil.ObjectToInt32(dataReader["AlertCapacity"]);
                            beCompartment.RegistrationStatus = DataUtil.ObjectToString(dataReader["RegistrationStatus"]);
                            beCompartment.ValorConsulta = "1";
                            beCompartmentList.Add(beCompartment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                beCompartmentList.Clear();
                BE_Compartment beCompartment = new BE_Compartment();
                beCompartment.ValorConsulta = "0";
                beCompartment.MensajeConsulta = ex.Message;
                beCompartmentList.Add(beCompartment);
            }
            return beCompartmentList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int IdCompartimiento,          
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
                    sqlParameterArray[1] = new SqlParameter("@IdCompartment", SqlDbType.Int);
                    sqlParameterArray[1].Direction = ParameterDirection.Input;
                    sqlParameterArray[1].Value = (object)IdCompartimiento;
                    sqlParameterArray[2] = new SqlParameter("@RegistrationUser", SqlDbType.Int);
                    sqlParameterArray[2].Direction = ParameterDirection.Input;
                    sqlParameterArray[2].Value = (object)idUsuarioRegistro;
                    using (IDataReader dataReader = (IDataReader)SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "USP_COMPARTMENT_UPDATE_STATUS_FOR_MOVIL_DEVICE", sqlParameterArray))
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
