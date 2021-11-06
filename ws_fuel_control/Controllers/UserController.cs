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
    public class UserController : ApiController
    {

        public JsonResult<List<BE_User>> GetlistarUsuarioParaDispositivoMovil(
                  string idDispositivo,
                  string acceso)
        {
            List<BE_User> content = new List<BE_User>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_User().validarExistenciaDispositivoMovil(idDispositivo);
                    if (str == "1" || str == "2")
                    {
                        content.AddRange(new BL_User().listarUsuarioParaDispositivoMovil(idDispositivo));                        
                    }
                    else if (str == "3")
                    {
                        content = new List<BE_User>();
                    }
                    else
                    {
                        content.Clear();
                        BE_User beUser = new BE_User();
                        beUser.ValorConsulta = "0";
                        beUser.MensajeConsulta = str;
                        content.Add(beUser);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_User beUser = new BE_User();
                    beUser.ValorConsulta = "0";
                    beUser.MensajeConsulta = ex.Message;
                    content.Add(beUser);
                }
            }
            return this.Json<List<BE_User>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idUsuario,
          int ULevel,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_User().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idUsuario, idUsuarioRegistro, ULevel);
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