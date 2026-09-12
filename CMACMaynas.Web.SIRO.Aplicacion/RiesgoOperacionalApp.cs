using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class RiesgoOperacionalApp : IRiesgoOperacionalApp
    {
        private readonly IRiesgoOperacional repository;

        public RiesgoOperacionalApp(IRiesgoOperacional repository)
        {
            this.repository = repository;
        }

        public string Registrar(RiesgoOperacional model) => repository.Registrar(model);

        public int Actualizar(RiesgoOperacional model) => repository.Actualizar(model);

        public int ConfirmarModificacionesRiesgo(long pnNroRiesgo, string psUltimaActualizacion)
        {
            return repository.ConfirmarModificacionesRiesgo(pnNroRiesgo, psUltimaActualizacion);
        }

        public int EliminarRiesgo(long pnNroRiesgo, string psUltimaActualizacion)
        {
            return repository.EliminarRiesgo(pnNroRiesgo, psUltimaActualizacion);
        }

        public int GrabarObservacionGestion(long pnNroRiesgo, List<dynamic> poObservaciones, string psUltimaActializacion)
        {
            return repository.GrabarObservacionGestion(pnNroRiesgo, poObservaciones, psUltimaActializacion);
        }

        public string[] GrabarProcesoPaso1(long pnNroRiesgo, int pnFactorRiesgo, int pnEventoPerdida, int pnSubEventoPerdida, string psLineaNeg, string psProducto, string psSubProducto, string psUltimaActializacion)
        {
            return repository.GrabarProcesoPaso1(pnNroRiesgo, pnFactorRiesgo, pnEventoPerdida, pnSubEventoPerdida, psLineaNeg, psProducto, psSubProducto, psUltimaActializacion);
        }

        public string[] GrabarProcesoPaso2(long pnNroRiesgo, int pnProbabilidad, int pnImpacto, string psUltimaActualizacion)
        {
            return repository.GrabarProcesoPaso2(pnNroRiesgo, pnProbabilidad, pnImpacto, psUltimaActualizacion);
        }

        public string[] GrabarProcesoPaso3(long pnNroRiesgo, string psComentarios, string psUltimaActualizacion)
        {
            return repository.GrabarProcesoPaso3(pnNroRiesgo, psComentarios, psUltimaActualizacion);
        }

        public string[] GrabarProcesoPaso4(long pnNroRiesgo, string psUltimaActualizacion)
        {
            return repository.GrabarProcesoPaso4(pnNroRiesgo, psUltimaActualizacion);
        }

        public int GrabarProcesoPaso5(long pnNroRiesgo, string psUltimaActualizacion)
        {
            return repository.GrabarProcesoPaso5(pnNroRiesgo, psUltimaActualizacion);
        }

        public List<RiesgoOperacional> ObtenerDatosRiesgoOperacional(DetalleRiesgo model) => repository.ObtenerDatosRiesgoOperacional(model);

        public RiesgoOperacional ObtenerInfoGeneralRiesgoOperacional(DetalleRiesgo model) => repository.ObtenerInfoGeneralRiesgoOperacional(model);
        

        public List<string> ObtenerInfoGestionRiesgo(long pnNroRiesgo, int pnProceso)
        {
            return repository.ObtenerInfoGestionRiesgo(pnNroRiesgo, pnProceso);
        }

        public List<dynamic> ObtenerObservacionesGestion(long pnNroRiesgo, int pnProcedencia = 2)
        {
            return repository.ObtenerObservacionesGestion(pnNroRiesgo, pnProcedencia);
        }

        public int ObtenerTpoRiesgo(long pnNroRiesgo)
        {
            return repository.ObtenerTpoRiesgo(pnNroRiesgo);
        }

        public int RechazarRiesgo(long pnNroRiesgo, string psMotivoRechazo, string psUltimaActualizacion)
        {
            return repository.RechazarRiesgo(pnNroRiesgo, psMotivoRechazo, psUltimaActualizacion);
        }

     


        
    }
}
