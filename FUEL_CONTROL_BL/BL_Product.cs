using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_Product
    {

        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            try
            {
                return new DA_Product().validarExistenciaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_Product> listarProductoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Product> beProductList = new List<BE_Product>();
            try
            {
                beProductList = new DA_Product().listarProductoParaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                beProductList.Clear();
                BE_Product beProduct = new BE_Product();
                beProduct.ValorConsulta = "0";
                beProduct.MensajeConsulta = ex.Message;
                beProductList.Add(beProduct);
            }
            return beProductList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idProducto,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_Product().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idProducto, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
