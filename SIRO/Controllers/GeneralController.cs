using EntidadNegocio;
using LogicaNegocio;
using Newtonsoft.Json;
using SIRO.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SIRO.Controllers
{
    public class GeneralController : Controller
    {
        GeneralModel model;
        public JsonResult ListaAgencias()
        {
            model = new GeneralModel();
            try
            {
                var oAgencias = AgenciasLN.ObtenerAgencias();
                if (oAgencias != null)
                {
                    model.oLstAgencia = oAgencias;
                }
                else { model.oLstAgencia = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        public JsonResult ListarAreasAgencia(string psAgeCod)
        {
            model = new GeneralModel();
            try
            {
                var oAreasAgencias = AreasLN.ObtenerAreasAgencia(psAgeCod);
                if (oAreasAgencias != null)
                {
                    model.oLstAreas = oAreasAgencias;
                }
                else { model.oLstAreas = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }
        public JsonResult ListaAreas()
        {
            model = new GeneralModel();
            try
            {
                var oAreas = AreasLN.ObtenerAreas();
                if (oAreas != null)
                {
                    model.oLstAreas = oAreas;
                }
                else { model.oLstAreas = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }
        public JsonResult ListarProcesosAreas(string psCodArea = "")
        {
            model = new GeneralModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var oProcesoArea = ProcesoAreaLN.ObtenerProcesoAreas(psCodArea = (psCodArea.Length > 0) ? psCodArea : usuario.oAreas.cAreaCod);
                if (oProcesoArea != null)
                {
                    model.oLstProcesoArea = oProcesoArea;
                }
                else { model.oLstProcesoArea = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarSubProcesosAreas(string psCodProceso)
        {
            model = new GeneralModel();
            try
            {
                var oSubProcesoArea = SubProcesosLN.ObtenerSubProcesosAreas(psCodProceso);
                if (oSubProcesoArea != null)
                {
                    model.oLstSubProcesoArea = oSubProcesoArea;
                }
                else { model.oLstSubProcesoArea = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListaControlesProceso(string psCodProceso = "")
        {
            model = new GeneralModel();
            try
            {
                //Usuario usuario = (Usuario)Session["Usuario"];
                var oControlProceso = ControlProcesoLN.ObtenerControlesProceso(psCodProceso);
                if (oControlProceso != null)
                {
                    model.oLstControlesAreas = oControlProceso;
                }
                else { model.oLstControlesAreas = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarCausasRiesgo()
        {
            model = new GeneralModel();
            try
            {
                var oCausaRiesgo = CausasRiesgoLN.ObtenerCausasRiesgo();
                if (oCausaRiesgo != null)
                {
                    model.oLstCausaRiesgo = oCausaRiesgo;
                }
                else { model.oLstCausaRiesgo = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarConstante(int pnConsCod)
        {
            model = new GeneralModel();
            try
            {
                var oConstantes = ConstantesLN.ObtenerConstantes(pnConsCod);
                if (oConstantes != null)
                {
                    model.oLstConstante = oConstantes;
                }
                else { model.oLstConstante = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarUsuarioAreaAgencia(string psAgeCod, string psAreaCod)
        {
            model = new GeneralModel();
            try
            {
                var oUsuarios = UsuarioLN.ObtenerUsuarioAgenciaArea(psAgeCod, psAreaCod);
                if (oUsuarios != null)
                {
                    model.oLstUsuarios = oUsuarios;
                }
                else { model.oLstUsuarios = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarUsuarioArea(string psAreaCod)
        {
            model = new GeneralModel();
            try
            {
                var oUsuarios = UsuarioLN.ObtenerUsuarioArea(psAreaCod);
                if (oUsuarios != null)
                {
                    model.oLstUsuarios = oUsuarios;
                }
                else { model.oLstUsuarios = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }


        public JsonResult ListarLineaNegocio()
        {
            LineaNegocioModel model = new LineaNegocioModel();
            try
            {
                //return Json(JsonConvert.SerializeObject(LineaNegocioLN.ObtenerLineaNegocio()));
                var oLineaNegocio = LineaNegocioLN.ObtenerLineaNegocio();
                if (oLineaNegocio != null)
                {
                    model.oLstLineaNegocio = oLineaNegocio;
                }
                else { model.oLstLineaNegocio = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }
        

        public JsonResult ListarSubLineaNegocio(string psCodLineaNegocio)
        {
            SubLineaNegocioModel model = new SubLineaNegocioModel();
            try
            {
                var oSubLineaNegocio = SubLineaNegocioLN.ObtenerSubLineaNegocio(psCodLineaNegocio);
                if (oSubLineaNegocio != null)
                {
                    model.oListSubLineaNegocio = oSubLineaNegocio;
                }
                else { model.oListSubLineaNegocio = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarProducto(string psCodLineaNegocio)
        {
            ProductoModel model = new ProductoModel();
            try
            {
                var oProducto = ProductoLN.ObtenerProducto(psCodLineaNegocio);
                if (oProducto != null)
                {
                    model.oLstProducto = oProducto;
                }
                else { model.oLstProducto = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ListarSubProducto(string psCodProducto)
        {
            SubProductoModel model = new SubProductoModel();
            try
            {
                var oSubProducto = SubProductoLN.ObtenerSubProducto(psCodProducto);
                if (oSubProducto != null)
                {
                    model.oLstSubProducto = oSubProducto;
                }
                else { model.oLstSubProducto = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }


        public JsonResult ListarCriteriosEval(int psCriterioCod)
        {
            CriteriosEvaluacionModel model = new CriteriosEvaluacionModel();
            try
            {
                var oCriteriosEval = CriteriosEvaluacionLN.ObtenerCriteriosEvaluacion(psCriterioCod);
                if (oCriteriosEval != null)
                {
                    model.oLstCriteriosEval = oCriteriosEval;
                }
                else { model.oLstCriteriosEval  = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }





       

    }
}