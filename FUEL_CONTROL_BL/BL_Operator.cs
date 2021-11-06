using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_Operator
    {
        public string validarExistenciaDispositivoMovilParaOperador(string idDispositivo)
        {
            try
            {
                return new DA_Operator().validarExistenciaDispositivoMovilParaOperador(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_Operator> listarOperadorParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Operator> bePersonList = new List<BE_Operator>();
            try
            {
                bePersonList = new DA_Operator().listarOperadorParaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                bePersonList.Clear();
                BE_Operator bePerson = new BE_Operator();
                bePerson.ValorConsulta = "0";
                bePerson.MensajeConsulta = ex.Message;
                bePersonList.Add(bePerson);
            }
            return bePersonList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idUsuario,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_Operator().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idUsuario, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
