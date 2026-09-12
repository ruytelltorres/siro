using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Reportes : IReportes
    {
        
        public List<dynamic> MatrizRiesgo(int pnTpoRiesgo, bool pbFiltro, string psAreaCod, string psDesde, string psHasta)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@bFiltro", pbFiltro),
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@cDesde", psDesde),
                    new SqlParameter("@cHasta", psHasta)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerMatrizRiesgo, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstDatosMatriz = new List<dynamic>();
                    dynamic oMatriz;
                    while (reader.Read())
                    {
                        oMatriz = new System.Dynamic.ExpandoObject();
                        oMatriz.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oMatriz.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oMatriz.nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoRiesgo")]);
                        oMatriz.cEvalDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]);
                        oMatriz.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oMatriz.cAgeCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeCod")]);
                        oMatriz.cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]);
                        oMatriz.cAreaCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaCod")]);
                        oMatriz.cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        oMatriz.cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]);
                        oMatriz.dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaDetec")]);
                        oMatriz.dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaReg")]);
                        oMatriz.cFactorRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cFactorRiesgo")]);
                        oMatriz.cEventoPerdida = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEventoPerdida")]);
                        oMatriz.cDescLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescLineaNeg")]);
                        oMatriz.cDescProducto = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProducto")]);
                        oMatriz.cDescSubProducto = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubProducto")]);
                        oMatriz.nProbalidadInherente = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProbalidadInherente")]);
                        oMatriz.cProbabilidadInherente = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProbabilidadInherente")]);
                        oMatriz.nImpactoInherente = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nImpactoInherente")]);
                        oMatriz.cImpactoInhrente = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cImpactoInhrente")]);
                        oMatriz.cNivelRiesgoInherente = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNivelRiesgoInherente")]);
                        oMatriz.nMontoPerdida = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nMontoPerdida")]);
                        LstDatosMatriz.Add(oMatriz);
                    }
                    return LstDatosMatriz;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Constante> ReporteGraficoCantidadRiesgoNivelResidual(int pnTpoRiesgo)
        {
            try
            {
                SqlParameter sqlTpoRiesgo = new SqlParameter("@nTpoRiesgo", pnTpoRiesgo);
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCantidadRiesgoPorNivelRiesgoResidual, CommandType.StoredProcedure, sqlTpoRiesgo))
                {
                    List<Negocio.Constante> LstReporte = new List<Negocio.Constante>();
                    Negocio.Constante report;
                    while (reader.Read())
                    {
                        report = new Negocio.Constante()
                        {
                            nConsCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPorcentaje")]), //Cantidad de riesgos en porcentaje
                            nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nConsValor")]),
                            cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cConsDescripcion")]),
                            nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantNivel")])
                        };

                        LstReporte.Add(report);
                    }
                    return LstReporte;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Constante> ObtenerMapaRiesgo(int pnTpoRiesgo, int pnTpoNivRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@nTpoNivelRiesgo", pnTpoNivRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_MapaRiesgo, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Constante> LstMapaRiesgo = new List<Negocio.Constante>();
                    Negocio.Constante tmp;
                    while (reader.Read())
                    {
                        tmp = new Negocio.Constante()
                        {
                            nConsCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProbabilidad")]),
                            nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nImpacto")]),
                            nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")])
                        };
                        LstMapaRiesgo.Add(tmp);
                    }
                    return LstMapaRiesgo;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.DatosRiesgos> ObtenerDatosMapaDet(int pnProbabilidad, int pnImpacto, int pnTpoRiesgo, int pnTpoNivRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nProbabilidad", pnProbabilidad),
                    new SqlParameter("@nImpacto", pnImpacto),
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@nTpoNivRiesgo", pnTpoNivRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerRiesgosNivelRiesgo, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.DatosRiesgos> LstDatosMapaDet = new List<Negocio.DatosRiesgos>();
                    Negocio.DatosRiesgos oDatosMapa;
                    while (reader.Read())
                    {
                        oDatosMapa = new Negocio.DatosRiesgos()
                        {
                            oUsuarios = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserReporta")])
                            },
                            oAgencias = new Negocio.Agencias()
                            {
                                cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")])
                            },
                            oAreas = new Negocio.Areas()
                            {
                                cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")]),
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDetec")]))
                        };
                        LstDatosMapaDet.Add(oDatosMapa);
                    }
                    return LstDatosMapaDet;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<dynamic> ObtenerDetalleRiesgoNivelRiesgo(int pnTpoRiesgo, int pnTpoNivRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@nTpoNivelRiesgo", pnTpoNivRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDetalleRiesgosNivelRiesgo, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstDatosMapaDet = new List<dynamic>();
                    dynamic oDatosMapa;
                    while (reader.Read())
                    {
                        oDatosMapa = new System.Dynamic.ExpandoObject();

                        oDatosMapa.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oDatosMapa.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oDatosMapa.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oDatosMapa.cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserReporta")]);
                        oDatosMapa.cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]);
                        oDatosMapa.cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        oDatosMapa.cProcRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProceso")]);
                        oDatosMapa.cEstadoRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")]);
                        oDatosMapa.dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaDetec")]);
                        oDatosMapa.nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoNivel")]);
                        oDatosMapa.nProbabilidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProbabilidad")]);
                        oDatosMapa.nImpacto = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nImpacto")]);
                        oDatosMapa.nValorNivel = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nValorNivel")]);
                        oDatosMapa.cDescNivel = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescNivel")]);

                        LstDatosMapaDet.Add(oDatosMapa);
                    }
                    return LstDatosMapaDet;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        /// <summary>
        /// Muestra la cantidad de riesgo por estado
        /// </summary>
        /// <returns></returns>
        public List<dynamic> ReporteGraficoRiesgoOperacional(int pnIndex, int pnTpoRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo)
                };

                string[] query =  { SP.stp_sel_ObtenerCantidadRiesgoEstado, SP.stp_sel_CantidadRiesgoxAnio, SP.stp_sel_ProcentajeRegistrosRiesgosCausas,
                                    SP.stp_sel_ObtenerRiesgosFactoRiesgo, SP.stp_sel_ObtenerPlanesAccionEstado, SP.stp_sel_RiesgoEstadoProceso,};

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), query[pnIndex], CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstReporte = new List<dynamic>();
                    dynamic report;
                    while (reader.Read())
                    {
                        report = new System.Dynamic.ExpandoObject();
                        switch (pnIndex)
                        {
                            case 0:
                                report.nProceso = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProceso")]);
                                report.cProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcesoDesc")]);
                                report.nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                break;
                            case 1:
                                report.nAnio = DataDefault.DbValueToDefault<String>(Convert.ToString(reader[reader.GetOrdinal("nAnio")]));
                                report.nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("R" + Convert.ToString(pnTpoRiesgo))]);
                                break;
                            case 2:
                                report.cCodCausa = DataDefault.DbValueToDefault<String>(Convert.ToString(reader[reader.GetOrdinal("cCodCausa")]));
                                report.cCausaDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCausaDesc")]);
                                report.nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                report.nProcentaje = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nProcentaje")]);
                                break;
                            case 3:
                                report.nFactor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nFactor")]);
                                report.cFactor = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cFactor")]);
                                report.nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                report.nProcentaje = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nProcentaje")]);
                                break;
                            case 4:
                                report.nValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nValor")]);
                                report.cEstado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")]);
                                report.nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                break;
                            case 5:
                                report.cCodEstado = DataDefault.DbValueToDefault<String>(Convert.ToString(reader[reader.GetOrdinal("cCodEstado")]));
                                report.cEstado = DataDefault.DbValueToDefault<String>(Convert.ToString(reader[reader.GetOrdinal("cEstado")]));
                                report.nCantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                break;
                            
                        }
                        LstReporte.Add(report);
                    }
                    return LstReporte;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ReporteGraficoEvaluaciones(int pnTpoRiesgo)
        {
            try
            {

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ReportesGraficosEventoPerdida(int pnIndex)
        {
            try
            {
                string[] query = { SP.stp_sel_PerdidaNetaClaseEventoPerdida, SP.stp_sel_PerdidaNetaVsMontoBrutoAnio, SP.stp_sel_MontoBrutoVsMontoRecup, SP.stp_sel_PerdidaNetaVsMontoBrutoAgencia,
                                   SP.stp_sel_CantidadEventoPerdidaAnio };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), query[pnIndex], CommandType.StoredProcedure))
                {
                    List<dynamic> LstReporte = new List<dynamic>();
                    dynamic dReporte;
                    while (reader.Read())
                    {
                        dReporte = new System.Dynamic.ExpandoObject();
                        switch (pnIndex)
                        {
                            case 0:
                                dReporte.CodClaseEvento = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodClasEventoP")]);
                                dReporte.DescClaseEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]);
                                dReporte.PerdidaNeta = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nPerdidaNeta")]));
                                dReporte.Cantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                break;
                            case 1:
                                dReporte.Anio = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAnio")]);
                                dReporte.PerdidaNeta = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nPerdidaNeta")]));
                                dReporte.MontoBruto = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoBruto")]));
                                break;
                            case 2:
                                dReporte.CodClaseEvento = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodClasEventoP")]);
                                dReporte.DescClaseEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]);
                                dReporte.MontoBruto = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoBruto")]));
                                dReporte.MontoRecuperado = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoRecup")]));
                                break;
                            case 3:
                                dReporte.CodAge = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeCod")]);
                                dReporte.Agencia = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]);
                                dReporte.PerdidaNeta = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nPerdidaNeta")]));
                                dReporte.MontoBruto = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoBruto")]));
                                break;
                            case 4:
                                dReporte.Anio = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAnio")]);
                                dReporte.Cantidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCantidad")]);
                                break;
                            default:

                                break;
                        }
                        LstReporte.Add(dReporte);
                    }
                    return LstReporte;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Obtiene los evento de perdida de acuerdo al filtro enviado
        /// </summary>
        /// <param name="psNombreCol">Nombre de la Columna</param>
        /// <param name="psValorFiltro">Valor del filtro</param>
        /// <returns></returns>
        public List<dynamic> ObtenerEventoPerdidaValorFiltro(string psNombreCol, string psValorFiltro)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cNombreCol", psNombreCol),
                    new SqlParameter("@cValFiltro", psValorFiltro)
                };
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerEventoPerdidaValorFiltro, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstReporte = new List<dynamic>();
                    dynamic dReporte;
                    while (reader.Read())
                    {
                        dReporte = new System.Dynamic.ExpandoObject();
                        dReporte.Evento = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        LstReporte.Add(dReporte);
                    }
                    return LstReporte;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



        #region Reportes sobre Planes de Accion

        public List<dynamic> ObtenerRiesgoPlanAccionReporte(int pnTpoBuscar, int pnTpoRiesgo, string psValorBuscar)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoBusqueda", pnTpoBuscar),
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@cValorBuscar", psValorBuscar)
                };
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerNroRoiesgoPlanAccion, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstReporte = new List<dynamic>();
                    dynamic dReporte;
                    while (reader.Read())
                    {
                        dReporte = new System.Dynamic.ExpandoObject();
                        dReporte.NroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        LstReporte.Add(dReporte);
                    }
                    return LstReporte;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ObtenerRiesgoRiesgosOperacionalesProceso(string psProcesos)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cProceso", psProcesos)
                };
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerRiesgosOperacionalesProceso, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstReporte = new List<dynamic>();
                    dynamic dReporte;
                    while (reader.Read())
                    {
                        dReporte = new System.Dynamic.ExpandoObject();
                        dReporte.NroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        LstReporte.Add(dReporte);
                    }
                    return LstReporte;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        #endregion

        #region Reporte Varios - Riesgo Operacional

        #region Riesgos Operacionales 
        public List<dynamic> RiesgosOperacinalesRechazados(string psDesde, string psHasta)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cDesde", Convert.ToDateTime(psDesde).ToString("yyyy/MM/dd")),
                    new SqlParameter("@cHasta", Convert.ToDateTime(psHasta).ToString("yyyy/MM/dd"))
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerRiesgosOperacionalesRechazados, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstRiesgosRechados = new List<dynamic>();
                    dynamic oRiesgos;
                    while (reader.Read())
                    {
                        oRiesgos = new System.Dynamic.ExpandoObject();
                        oRiesgos.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oRiesgos.cMotivoRechazo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cMotivoRechazo")]);
                        oRiesgos.dFechaRechazo = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaRechazo")]));
                        oRiesgos.cUserRechazo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserRechazo")]);
                        LstRiesgosRechados.Add(oRiesgos);
                    }
                    return LstRiesgosRechados;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Planes de Accion
        public List<dynamic> ObtenerRiesgoEstadoPlanAccion(int pnTpoRiesgo, int pnEstadoPlan)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@nEstadoPlan", pnEstadoPlan)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerRiesgoEstadoPlanAccion, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstRiesgos = new List<dynamic>();
                    dynamic oRiesgos;
                    while (reader.Read())
                    {
                        oRiesgos = new System.Dynamic.ExpandoObject();
                        oRiesgos.NroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        //oRiesgos.nPlanCod = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]);
                        LstRiesgos.Add(oRiesgos);
                    }
                    return LstRiesgos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
        #endregion

        #region Incentivo

        public List<dynamic> ObtenerObjetoReporteIncentivo(int pnReporte, int pnTpoRiesgo, int pnMotivo = 0, decimal pnMonto = 0) {

            try {
                List<dynamic> _reporte = new List<dynamic>();
                switch (pnReporte)
                {
                    case 1: //Lista de riesgos por Motivo
                        _reporte = ObtenerIncentivoMotivo(pnTpoRiesgo, pnMotivo);
                        break;
                    case 2: //Lista de riesgos por Monto
                        _reporte = ObtenerIncentivoMonto(pnTpoRiesgo, pnMonto);
                        break;
                }
                return _reporte;
            }
            catch (Exception ex){ throw new Exception(ex.Message);  }

        }

        /// <summary>
        /// Obtiene los riesgos de acuerdo al tipo de riesgo y el motivo
        /// </summary>
        /// <param name="pnTpoRiesgo">Tipo del Riesgo</param>
        /// <param name="pnMotivo">Motivo del Riesgo</param>
        /// <returns></returns>
        private List<dynamic> ObtenerIncentivoMotivo(int pnTpoRiesgo, int pnMotivo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@nMotivo", pnMotivo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerIncentivoMotivo, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstRiesgos = new List<dynamic>();
                    dynamic oRiesgos;
                    while (reader.Read())
                    {
                        oRiesgos = new System.Dynamic.ExpandoObject();
                        oRiesgos.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        LstRiesgos.Add(oRiesgos);
                    }
                    return LstRiesgos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnTpoRiesgo"></param>
        /// <param name="pnMotivo"></param>
        /// <returns></returns>
        private List<dynamic> ObtenerIncentivoMonto(int pnTpoRiesgo, decimal pnMonto)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@nMonto", pnMonto)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerIncentivosMonto, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstRiesgos = new List<dynamic>();
                    dynamic oRiesgos;
                    while (reader.Read())
                    {
                        oRiesgos = new System.Dynamic.ExpandoObject();
                        oRiesgos.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        LstRiesgos.Add(oRiesgos);
                    }
                    return LstRiesgos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #endregion
    }
}