using FUEL_CONTROL_BE;
using FUEL_CONTROL_BL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace ws_fuel_control.Controllers
{
    public class ReasonController : ApiController
    {

        public JsonResult<List<BE_Reason>> GetlistarRazonParaDispositivoMovil(
                  string idDispositivo,
                  string acceso)
        {
            List<BE_Reason> content = new List<BE_Reason>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_Reason().validarExistenciaDispositivoMovil(idDispositivo);
                    if (str == "1" || str == "2")
                    {
                        content.AddRange(new BL_Reason().listarRazonParaDispositivoMovil(idDispositivo));                        
                    }
                    else if (str == "3")
                    {
                        content = new List<BE_Reason>();
                    }
                    else
                    {
                        content.Clear();
                        BE_Reason beReason = new BE_Reason();
                        beReason.ValorConsulta = "0";
                        beReason.MensajeConsulta = str;
                        content.Add(beReason);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_Reason beReason = new BE_Reason();
                    beReason.ValorConsulta = "0";
                    beReason.MensajeConsulta = ex.Message;
                    content.Add(beReason);
                }
            }
            return this.Json<List<BE_Reason>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idRazon,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_Reason().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idRazon, idUsuarioRegistro);
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