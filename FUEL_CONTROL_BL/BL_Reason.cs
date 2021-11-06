using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_Reason
    {

        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            try
            {
                return new DA_Reason().validarExistenciaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_Reason> listarRazonParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Reason> beReasonList = new List<BE_Reason>();
            try
            {
                beReasonList = new DA_Reason().listarReasonParaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                beReasonList.Clear();
                BE_Reason beReason = new BE_Reason();
                beReason.ValorConsulta = "0";
                beReason.MensajeConsulta = ex.Message;
                beReasonList.Add(beReason);
            }
            return beReasonList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idRazon,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_Reason().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idRazon, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
