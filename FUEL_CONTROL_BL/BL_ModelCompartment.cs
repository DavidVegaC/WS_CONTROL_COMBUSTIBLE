using FUEL_CONTROL_BE;
using FUEL_CONTROL_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BL
{
    public class BL_ModelCompartment
    {
        public string validarExistenciaDispositivoMovil(string idDispositivo)
        {
            try
            {
                return new DA_Model_Compartment().validarExistenciaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<BE_Model_Compartment> listarModeloCompartimientoParaDispositivoMovil(string idDispositivo)
        {
            List<BE_Model_Compartment> beModelCompartmentList = new List<BE_Model_Compartment>();
            try
            {
                beModelCompartmentList = new DA_Model_Compartment().listarModeloCompartimientoParaDispositivoMovil(idDispositivo);
            }
            catch (Exception ex)
            {
                beModelCompartmentList.Clear();
                BE_Model_Compartment beModelCompartment = new BE_Model_Compartment();
                beModelCompartment.ValorConsulta = "0";
                beModelCompartment.MensajeConsulta = ex.Message;
                beModelCompartmentList.Add(beModelCompartment);
            }
            return beModelCompartmentList;
        }

        public string actualizarEstadoMigracionDispositivoMovil(
          string idDispositivo,
          int idModeloCompartimiento,
          int idUsuarioRegistro)
        {
            try
            {
                return new DA_Compartment().actualizarEstadoMigracionDispositivoMovil(idDispositivo, idModeloCompartimiento, idUsuarioRegistro);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
