using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Evaluacion : IEvaluacion
    {
        
        public List<Negocio.Evaluacion> ObtenerTipoEvaluacion()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerTiposEvaluacion, CommandType.StoredProcedure))
                {
                    List<Negocio.Evaluacion> LstTpoEval = new List<Negocio.Evaluacion>();
                    Negocio.Evaluacion oTpoEval;
                    while (reader.Read())
                    {
                        oTpoEval = new Negocio.Evaluacion()
                        {
                            nCodEval = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]),
                            cEvalDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]),
                        };
                        LstTpoEval.Add(oTpoEval);
                    }
                    return LstTpoEval;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string RegistrarEvaluacion(int pnCodEvaluacion, string psRiesgoIdentificado, string psCauasRiesgo, string psUltimaActualizacion,
                                              string psAgeCod = "", string psAreaCod = "", string psCodProceso = "", string psCodSubProceso = "")
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoEvalucion", pnCodEvaluacion),
                    new SqlParameter("@cRiesgoIdent", psRiesgoIdentificado),
                    new SqlParameter("@cCausas", psCauasRiesgo),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cAgeCod", psAgeCod),
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@cCodProceso", psCodProceso),
                    new SqlParameter("@cCodSubProceso", psCodSubProceso),
                    new SqlParameter("@cCodRiesgo", SqlDbType.VarChar, 25) { Direction = ParameterDirection.Output }
                };


                var exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarEvalucion, CommandType.StoredProcedure, parameters);
                return (string)parameters[8].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.DatosRiesgos> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nTpoBuscar", pnTpoBuscar),
                    new SqlParameter("@cValorBus", psValorBuscar),
                    new SqlParameter("@cUser", psUsuario)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarEvaluacionesGestion, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.DatosRiesgos> LstEvaluaciones = new List<Negocio.DatosRiesgos>();
                    Negocio.DatosRiesgos oEval;

                    while (reader.Read())
                    {
                        oEval = new Negocio.DatosRiesgos()
                        {
                            oUsuarios = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")])
                            },
                            oEvaluaciones = new Negocio.Evaluacion()
                            {
                                nCodEval = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]),
                                cEvalDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")])
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraReg")])),
                            nProcRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProcRiesgo")]),
                            cProcRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcRiesgo")]),
                            nEstadoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                            cEstadoRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoRiesgo")]),
                        };
                        LstEvaluaciones.Add(oEval);
                    }
                    return LstEvaluaciones;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.DatosRiesgos MostrarDetalleEvaluacion(long pnNroRiesgo)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDetalleEvaluacion, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    Negocio.DatosRiesgos oEval = null;
                    while (reader.Read())
                    {
                        oEval = new Negocio.DatosRiesgos();
                        oEval.oUsuarios = new Negocio.Usuario();
                        oEval.oEvaluaciones = new Negocio.Evaluacion();
                        oEval.oAgencias = new Negocio.Agencias();
                        oEval.oAreas = new Negocio.Areas();
                        oEval.oCausas = new Negocio.CausaRiesgo();
                        oEval.oProceso = new Negocio.ProcesoArea();
                        oEval.oProceso.oSubProceso = new Negocio.SubProcesos();
                        oEval.oTaller = new Negocio.Taller();

                        oEval.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oEval.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oEval.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oEval.nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoRiesgo")]);
                        oEval.oUsuarios.cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserReporta")]);
                        oEval.oUsuarios.cUsuario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNombrecUserReporta")]);
                        oEval.oAgencias.cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]);
                        oEval.oAreas.cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        oEval.oCausas.cCausaDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCausas")]);
                        oEval.oEvaluaciones.nCodEval = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]);
                        oEval.oEvaluaciones.cEvalDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]);
                        oEval.oProceso.cDescProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProceso")]);
                        oEval.oProceso.oSubProceso.cDescSubProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubProceso")]);
                        oEval.nProcRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProcRiesgo")]);
                        oEval.cProcRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcRiesgo")]);
                        oEval.nEstadoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstadoRiesgo")]);
                        oEval.cEstadoRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoRiesgo")]);
                        oEval.dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")]));
                        oEval.oTaller.cCodTaller = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodTaller")]);
                        oEval.nCondicionTaller = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCondicion")]);
                    }
                    return oEval;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.DatosRiesgos> MostrarEvaluacionNotificacion(string psUser)
        {
            try
            {
                SqlParameter sqlUsuario = new SqlParameter("@cUser", psUser);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerEvaluacionesNotificacion, CommandType.StoredProcedure, sqlUsuario))
                {
                    List<Negocio.DatosRiesgos> LstEvaluaciones = new List<Negocio.DatosRiesgos>();
                    Negocio.DatosRiesgos oEval;
                    while (reader.Read())
                    {
                        oEval = new Negocio.DatosRiesgos();
                        oEval.oUsuarios = new Negocio.Usuario();
                        oEval.oEvaluaciones = new Negocio.Evaluacion();
                        
                        oEval.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oEval.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oEval.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oEval.oUsuarios.cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]);
                        oEval.oEvaluaciones.nCodEval = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]);
                        oEval.oEvaluaciones.cEvalDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]);
                        oEval.dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraReg")]));
                        oEval.nProcRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProcRiesgo")]);
                        oEval.cProcRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcRiesgo")]);
                        oEval.nEstadoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstadoRiesgo")]);
                        oEval.cEstadoRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoRiesgo")]);

                        LstEvaluaciones.Add(oEval);
                    }
                    return LstEvaluaciones;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.DatosRiesgos> MostrarEvaluacionVerificacionTaller(string psCodTaller)
        {
            try
            {
                SqlParameter sqlCodTaller = new SqlParameter("@cCodTaller", psCodTaller);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerEvaluacionesTaller, CommandType.StoredProcedure, sqlCodTaller))
                {
                    List<Negocio.DatosRiesgos> LstEvaluaciones = new List<Negocio.DatosRiesgos>();
                    Negocio.DatosRiesgos oEval;
                    while (reader.Read())
                    {
                        oEval = new Negocio.DatosRiesgos()
                        {
                            oUsuarios = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")])
                            },
                            oEvaluaciones = new Negocio.Evaluacion()
                            {
                                nCodEval = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]),
                                cEvalDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]),
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraReg")])),
                            nProcRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProcesoActual")]),
                            cProcRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcActual")]),
                            nEstadoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                            cEstadoRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoRiesgo")]),
                            nCondicionTaller = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstadoTaller")]),
                            cCondicionTaller = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoTaller")]),
                        };

                        LstEvaluaciones.Add(oEval);
                    }
                    return LstEvaluaciones;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
    }
}
