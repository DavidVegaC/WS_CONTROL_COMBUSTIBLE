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
    public class ProductController : ApiController
    {

        public JsonResult<List<BE_Product>> GetlistarProductoParaDispositivoMovil(
                  string idDispositivo,
                  string acceso)
        {
            List<BE_Product> content = new List<BE_Product>();
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    string str = new BL_Product().validarExistenciaDispositivoMovil(idDispositivo);
                    if (str == "1" || str == "2")
                    {
                        content.AddRange(new BL_Product().listarProductoParaDispositivoMovil(idDispositivo));                        
                    }
                    else if (str == "3")
                    {
                        content = new List<BE_Product>();
                    }
                    else
                    {
                        content.Clear();
                        BE_Product beProduct = new BE_Product();
                        beProduct.ValorConsulta = "0";
                        beProduct.MensajeConsulta = str;
                        content.Add(beProduct);
                    }
                }
                catch (Exception ex)
                {
                    content.Clear();
                    BE_Product beProduct = new BE_Product();
                    beProduct.ValorConsulta = "0";
                    beProduct.MensajeConsulta = ex.Message;
                    content.Add(beProduct);
                }
            }
            return this.Json<List<BE_Product>>(content);
        }

        public JsonResult<string> GetactualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idProducto,
          int idUsuarioRegistro,
          string acceso)
        {
            string content = "";
            if (acceso == "Ll4V3serv1ci0.Api")
            {
                try
                {
                    content = new BL_Product().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idProducto, idUsuarioRegistro);
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