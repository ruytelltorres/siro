using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class RiesgoResidual : IRiesgoResidual
    {
        public List<Negocio.RiesgoResidual> ObtenerControlRiesgoResidual(Negocio.DetalleRiesgo riesgo)
        {
            try
            {
                SqlParameter sqlCodRiesgo = new SqlParameter("@nNroRiesgo", riesgo.nNroRiesgo);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarControlesRiesgoResidual, CommandType.StoredProcedure, sqlCodRiesgo))
                {
                    List<Negocio.RiesgoResidual> lstControles = new List<Negocio.RiesgoResidual>();
                    Negocio.RiesgoResidual oRiesgoResidual;
                    while (reader.Read())
                    {
                        oRiesgoResidual = new Negocio.RiesgoResidual()
                        {
                            nItem = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroItem")]),
                            cComentario = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cComentario")]),
                            oResposableDefinido = new List<Negocio.Item<string>>() {
                               new Negocio.Item<string>(){
                                   Id =  DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nRCValor")]).ToString(),
                                   Nombre = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cRCDescripcion")])
                               }
                            },
                            oPeriodoEfecucion = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nFDValor")]).ToString(),
                                    Nombre = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cFDDescripcion")])
                                }
                            },
                            oEvidenciaControl = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEDValor")]).ToString(),
                                    Nombre = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEDDescripcion")])
                                }
                            },
                            oEjecucionControl = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nTCValor")]).ToString(),
                                    Nombre = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cTCDescripcion")])
                                }
                            },
                            oCumplimientoObjectivo = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCOValor")]).ToString(),
                                    Nombre = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCODescripcion")])
                                }
                            },
                            oEfectividadControl = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEfectControl")]).ToString(),
                                    Nombre = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEfectControl")])
                                }
                            },
                            dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaReg")])),
                            dFechaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cUltimaActualzacion")]))

                        };
                        //oRiesgoResidual.oDatosRiesgo = new Negocio.DatosRiesgos();
                        //oRiesgoResidual.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        //oRiesgoResidual.nItem = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroItem")]);
                        //oRiesgoResidual.cComentario = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cComentario")]);
                        //oRiesgoResidual.nResponsableDef = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nRCValor")]);
                        //oRiesgoResidual.cResponsableDef = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cRCDescripcion")]);
                        //oRiesgoResidual.nPeriodoEjecucion = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nFDValor")]);
                        //oRiesgoResidual.cPeriodoEjecucion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cFDDescripcion")]);
                        //oRiesgoResidual.nEvidenciaControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEDValor")]);
                        //oRiesgoResidual.cEvidenciaControl = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEDDescripcion")]);
                        //oRiesgoResidual.nEjecucionControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nTCValor")]);
                        //oRiesgoResidual.cEjecucionControl = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cTCDescripcion")]);
                        //oRiesgoResidual.nCumpleObjetivo = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCOValor")]);
                        //oRiesgoResidual.cCumpleObjetivo = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCODescripcion")]);
                        //oRiesgoResidual.nEfectividadControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEfectControl")]);
                        //oRiesgoResidual.cEfectividadControl = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEfectControl")]);
                        //oRiesgoResidual.dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaReg")]));
                        //oRiesgoResidual.dFechaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cUltimaActualzacion")]));
                        lstControles.Add(oRiesgoResidual);
                    }
                    return lstControles;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RegistrarControlesRiesgoResidual(long pnNroRiesgo, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl,
                                                            int pnCumpleObj, int pnEfecControl, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cComentario", psComentario),
                    new SqlParameter("@nReponControl", pnReponControl),
                    new SqlParameter("@nPeriEjec", pnPeriEjec),
                    new SqlParameter("@nEvidenControl", pnEvidenControl),
                    new SqlParameter("@nEjecControl", pnEjecControl),
                    new SqlParameter("@nCumpleObj", pnCumpleObj),
                    new SqlParameter("@nEfectControl", pnEfecControl),
                    new SqlParameter("@cFechaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarControlesRiesgoResidual, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int ActualizarControlesRiesgoResidual(long pnNroRiesgo, int pnItem, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl,
                                                            int pnCumpleObj, int pnEfecControl, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nItem", pnItem),
                    new SqlParameter("@cComentario", psComentario),
                    new SqlParameter("@nReponControl", pnReponControl),
                    new SqlParameter("@nPeriEjec", pnPeriEjec),
                    new SqlParameter("@nEvidenControl", pnEvidenControl),
                    new SqlParameter("@nEjecControl", pnEjecControl),
                    new SqlParameter("@nCumpleObj", pnCumpleObj),
                    new SqlParameter("@nEfectControl", pnEfecControl),
                    new SqlParameter("@cFechaActualizacion", psUltimaActualizacion)
                };

                int exito = Helper.SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_ActualizarControlesRiesgoResidual, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int EliminarControlRiesgoResidual(long pnNroRiesgo, int pnItem, string psUltimaActualizacion)
        {
            try
            {

                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nItem", pnItem),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = Helper.SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_EliminarControlRiesgoResidual, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.RiesgoResidual ObtenerCriteriosEvalControlRiesgoResidual(long pnNroRiesgo, int pnItem)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nItem", pnItem)
                };

                using (SqlDataReader reader = Helper.SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerValorCriterioEvalControlRiesgoResidual, CommandType.StoredProcedure, parameters))
                {

                    Negocio.RiesgoResidual oRiesgoResidual = null;
                    while (reader.Read())
                    {
                        oRiesgoResidual = new Negocio.RiesgoResidual()
                        {
                            nItem = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroItem")]),
                            cComentario = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cComentario")]),
                            oResposableDefinido = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nResponControl")]).ToString(),
                                }
                            },
                            oPeriodoEfecucion = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nPeriEjec")]).ToString()
                                }
                            },
                            oEvidenciaControl = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEvidControl")]).ToString()
                                }
                            },
                            oEjecucionControl = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEjecControl")]).ToString()
                                }
                            },
                            oCumplimientoObjectivo = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCumpliObjetivo")]).ToString()
                                }
                            },
                            oEfectividadControl = new List<Negocio.Item<string>>() {
                                new Negocio.Item<string>(){
                                    Id = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEfectControl")]).ToString()
                                }
                            }
                        };
                        //oRiesgoResidual.oDatosRiesgo = new Negocio.DatosRiesgos();
                        //oRiesgoResidual.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        //oRiesgoResidual.nItem = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroItem")]);
                        //oRiesgoResidual.cComentario = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cComentario")]);
                        //oRiesgoResidual.nResponsableDef = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nResponControl")]);
                        //oRiesgoResidual.nPeriodoEjecucion = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nPeriEjec")]);
                        //oRiesgoResidual.nEvidenciaControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEvidControl")]);
                        //oRiesgoResidual.nEjecucionControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEjecControl")]);
                        //oRiesgoResidual.nCumpleObjetivo = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCumpliObjetivo")]);
                        //oRiesgoResidual.nEfectividadControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEfectControl")]);
                    }
                    return oRiesgoResidual;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public Negocio.EscalaNivelesRiesgo ObtenerNivelRiesgoResidualEscala(int pnEscala)
        {
            try
            {
                SqlParameter sqlEscala = new SqlParameter("@nValorEscala", pnEscala);
                using (SqlDataReader reader = Helper.SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerNivelRiesgoEscala, CommandType.StoredProcedure, sqlEscala))
                {

                    Negocio.EscalaNivelesRiesgo oEscala = null;
                    while (reader.Read())
                    {
                        oEscala = new Negocio.EscalaNivelesRiesgo()
                        {
                            nProbabilidad = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProbabilidad")]),
                            cProbabilidad = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cProbabilidad")]),
                            nImpacto = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nImpacto")]),
                            cImpacto = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cImpacto")]),
                            nValorEstala = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nValorEscala")]),
                            nNivelRiesgo = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNivelRiesgo")]),
                        };
                    }
                    return oEscala;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.EscalaNivelesRiesgo ObtenerEscalaNivelRiesgo(int pnProbabilidad, int pnImpacto)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@nProbabilidad", pnProbabilidad),
                    new SqlParameter("@nImpacto", pnImpacto)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerNivelRiesgoEscalaxProbabilidadImpacto, CommandType.StoredProcedure, sqlParameters))
                {

                    Negocio.EscalaNivelesRiesgo oEscala = null;
                    while (reader.Read())
                    {
                        oEscala = new Negocio.EscalaNivelesRiesgo()
                        {
                            nProbabilidad = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProbabilidad")]),
                            cProbabilidad = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cProbabilidad")]),
                            nImpacto = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nImpacto")]),
                            cImpacto = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cImpacto")]),
                            nValorEstala = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nValorEscala")])
                        };
                    }
                    return oEscala;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
