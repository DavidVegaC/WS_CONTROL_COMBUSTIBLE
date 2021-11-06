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
    public class CompartmentController : ApiController
    {

        public JsonResult<List<BE_Compartment>> GetlistarCompartimientoParaDispositivoMovil(
                  string idDispositivo,
                  string acceso)
        {
            List<BE_Compartment> content = new List<BE_Compartment>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_Compartment().validarExistenciaDispositivoMovil(idDispositivo);
                    if (str == "1" || str == "2")
                    {
                        content.AddRange(new BL_Compartment().listarCompartimientoParaDispositivoMovil(idDispositivo));                        
                    }
                    else if (str == "3")
                    {
                        content = new List<BE_Compartment>();
                    }
                    else
                    {
                        content.Clear();
                        BE_Compartment beCompartment = new BE_Compartment();
                        beCompartment.ValorConsulta = "0";
                        beCompartment.MensajeConsulta = str;
                        content.Add(beCompartment);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_Compartment beCompartment = new BE_Compartment();
                    beCompartment.ValorConsulta = "0";
                    beCompartment.MensajeConsulta = ex.Message;
                    content.Add(beCompartment);
                }
            }
            return this.Json<List<BE_Compartment>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idCompartimiento,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_Compartment().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idCompartimiento, idUsuarioRegistro);
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