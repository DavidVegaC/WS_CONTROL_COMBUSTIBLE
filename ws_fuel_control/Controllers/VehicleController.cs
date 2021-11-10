using FUEL_CONTROL_BE;
using FUEL_CONTROL_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ws_fuel_control.Controllers
{
    public class VehicleController : ApiController
    {
        public JsonResult<List<BE_Vehicle>> GetlistarVehiculoParaDispositivoMovil(
                  string idDispositivo,
                  string acceso)
        {
            List<BE_Vehicle> content = new List<BE_Vehicle>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_Vehicle().validarExistenciaDispositivoMovil(idDispositivo);
                    if (str == "1" || str == "2")
                    {
                        content.AddRange(new BL_Vehicle().listarVehiculoParaDispositivoMovil(idDispositivo));
                    }
                    else if (str == "3")
                    {
                        content = new List<BE_Vehicle>();
                    }
                    else
                    {
                        content.Clear();
                        BE_Vehicle beVehicle = new BE_Vehicle();
                        beVehicle.ValorConsulta = "0";
                        beVehicle.MensajeConsulta = str;
                        content.Add(beVehicle);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_Vehicle beVehicle = new BE_Vehicle();
                    beVehicle.ValorConsulta = "0";
                    beVehicle.MensajeConsulta = ex.Message;
                    content.Add(beVehicle);
                }
            }
            return this.Json<List<BE_Vehicle>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idVehiculo,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_Vehicle().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idVehiculo, idUsuarioRegistro);
                }
                catch (Exception ex)
                {
                    content = ex.Message;
                }
            }
            return this.Json<string>(content);
        }
    }
}
