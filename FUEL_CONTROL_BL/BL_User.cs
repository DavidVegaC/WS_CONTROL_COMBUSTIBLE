using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_User
    {

        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            try
            {
                return new DA_User().validarExistenciaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_User> listarUsuarioParaDispositivoMovil(string idDispositivo)
        {
            List<BE_User> beUserList = new List<BE_User>();
            try
            {
                beUserList = new DA_User().listarUsuarioParaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                beUserList.Clear();
                BE_User beUser = new BE_User();
                beUser.ValorConsulta = "0";
                beUser.MensajeConsulta = ex.Message;
                beUserList.Add(beUser);
            }
            return beUserList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idUsuario,
          int ULevel,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_User().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idUsuario, ULevel, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
