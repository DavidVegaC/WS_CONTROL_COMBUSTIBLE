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
    public class ModelCompartmentController : ApiController
    {
        public JsonResult<List<BE_Model_Compartment>> GetlistarModeloCompartimientoParaDispositivoMovil(
                  string idDispositivo,
                  string acceso)
        {
            List<BE_Model_Compartment> content = new List<BE_Model_Compartment>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_ModelCompartment().validarExistenciaDispositivoMovil(idDispositivo);
                    if (str == "1" || str == "2")
                    {
                        content.AddRange(new BL_ModelCompartment().listarModeloCompartimientoParaDispositivoMovil(idDispositivo));
                    }
                    else if (str == "3")
                    {
                        content = new List<BE_Model_Compartment>();
                    }
                    else
                    {
                        content.Clear();
                        BE_Model_Compartment beModelCompartment = new BE_Model_Compartment();
                        beModelCompartment.ValorConsulta = "0";
                        beModelCompartment.MensajeConsulta = str;
                        content.Add(beModelCompartment);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_Model_Compartment beModelCompartimiento = new BE_Model_Compartment();
                    beModelCompartimiento.ValorConsulta = "0";
                    beModelCompartimiento.MensajeConsulta = ex.Message;
                    content.Add(beModelCompartimiento);
                }
            }
            return this.Json<List<BE_Model_Compartment>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idModeloCompartimiento,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_Vehicle().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idModeloCompartimiento, idUsuarioRegistro);
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