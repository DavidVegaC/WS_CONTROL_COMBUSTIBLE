using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_Vehicle
    {
        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            try
            {
                return new DA_Vehicle().validarExistenciaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_Vehicle> listarVehiculoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Vehicle> beVehicleList = new List<BE_Vehicle>();
            try
            {
                beVehicleList = new DA_Vehicle().listarVehiculoParaDispositivoMovil(idDispositivo);
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
          int idVehiculo,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_Vehicle().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idVehiculo, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
