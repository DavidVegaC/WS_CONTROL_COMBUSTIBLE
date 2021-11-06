using FUEL_CONTROL_BE;
using FUEL_CONTROL_BL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Results;

namespace ws_fuel_control.Controllers
{
    public class OperatorController : ApiController
    {
        public JsonResult<List<BE_Operator>> GetlistarOperadorParaDispositivoMovil(
       string idDispositivo,
       string acceso)
        {
            List<BE_Operator> content = new List<BE_Operator>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_Operator().validarExistenciaDispositivoMovilParaOperador(idDispositivo);
                    if (str == "1" || str == "2")
                        content = new BL_Operator().listarOperadorParaDispositivoMovil(idDispositivo);
                    else if (str == "3")
                    {
                        content = new List<BE_Operator>();
                    }
                    else
                    {
                        content.Clear();
                        BE_Operator beUser = new BE_Operator();
                        beUser.ValorConsulta = "0";
                        beUser.MensajeConsulta = str;
                        content.Add(beUser);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_Operator beUser = new BE_Operator();
                    beUser.ValorConsulta = "0";
                    beUser.MensajeConsulta = ex.Message;
                    content.Add(beUser);
                }
            }
            return this.Json<List<BE_Operator>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idOperador,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_Operator().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idOperador, idUsuarioRegistro);
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
