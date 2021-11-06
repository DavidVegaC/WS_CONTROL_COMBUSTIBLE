using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_Compartment
    {

        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            try
            {
                return new DA_Compartment().validarExistenciaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_Compartment> listarCompartimientoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Compartment> beCompartmentList = new List<BE_Compartment>();
            try
            {
                beCompartmentList = new DA_Compartment().listarCompartimientoParaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                beCompartmentList.Clear();
                BE_Compartment beCompartment = new BE_Compartment();
                beCompartment.ValorConsulta = "0";
                beCompartment.MensajeConsulta = ex.Message;
                beCompartmentList.Add(beCompartment);
            }
            return beCompartmentList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idCompartimiento,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_Compartment().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idCompartimiento, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
