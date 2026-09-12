using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Autoevaluacion : IAutoevaluacion
    {
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

        public List<Negocio.Autoevaluacion> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario)
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
                    List<Negocio.Autoevaluacion> LstEvaluaciones = new List<Negocio.Autoevaluacion>();
                    Negocio.Autoevaluacion oEval;

                    while (reader.Read())
                    {
                        oEval = new Negocio.Autoevaluacion()
                        {
                            oUsuario = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")])
                            },
                            oTipoEvaluacion = new Negocio.Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")])
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraReg")])),
                            oProcRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProcRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cProcRiesgo")]),
                            },
                            oEstado = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEstadoRiesgo")])
                            }
                        };
                        LstEvaluaciones.Add(oEval);
                    }
                    return LstEvaluaciones;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.Autoevaluacion MostrarDetalleEvaluacion(long pnNroRiesgo)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDetalleEvaluacion, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    Negocio.Autoevaluacion autoevaluacion = null;
                    while (reader.Read())
                    {
                        autoevaluacion = new Negocio.Autoevaluacion()
                        {
                            oUsuario = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserReporta")]),
                                cUsuario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNombrecUserReporta")]),
                            },
                            oTipoRiesgo= new Constante() {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTipoRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cTipoRiesgo")]),
                            },
                            oTipoEvaluacion = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]),
                            },
                            oAgencia = new Negocio.Agencias()
                            {
                                cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]),
                                oArea = new Negocio.Areas()
                                {
                                    cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")])
                                }
                            },
                            oCausas = new Negocio.CausaRiesgo()
                            {
                                cCausaDesc = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCausas")])
                            },
                            oProceso = new Negocio.ProcesoArea()
                            {
                                //cCodProceso = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCodProceso")]),
                                cDescProceso = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cDescProceso")]),
                                oSubProceso = new Negocio.SubProcesos()
                                {
                                    //nCodSubProceso = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCodSubProceso")]),
                                    cDescSubProceso = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cDescSubProceso")])
                                }
                            },
                            oProcRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProcRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcRiesgo")])
                            },
                            oTaller = new Negocio.Taller()
                            {
                                cCodTaller = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodTaller")]),
                                //cCodTpoEval = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescTaller")])
                            },
                            oEstado = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoRiesgo")])
                            },
                            //oEval.nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoRiesgo")]);

                            nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                        };
                    }
                    return autoevaluacion;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Autoevaluacion> MostrarEvaluacionNotificacion(string psUser)
        {
            try
            {
                SqlParameter sqlUsuario = new SqlParameter("@cUser", psUser);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerEvaluacionesNotificacion, CommandType.StoredProcedure, sqlUsuario))
                {
                    List<Negocio.Autoevaluacion> LstEvaluaciones = new List<Negocio.Autoevaluacion>();
                    Negocio.Autoevaluacion autoevaluacion;
                    while (reader.Read())
                    {
                        autoevaluacion = new Negocio.Autoevaluacion()
                        {
                            oUsuario = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]),
                            },
                            oTipoEvaluacion = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodEval")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")]),
                            },

                            oProcRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProcRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProcRiesgo")])
                            },
                            oEstado = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoRiesgo")])
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraReg")])),
                        };
                        LstEvaluaciones.Add(autoevaluacion);
                    }
                    return LstEvaluaciones;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Autoevaluacion> MostrarEvaluacionVerificacionTaller(string psCodTaller)
        {
            try
            {
                SqlParameter sqlCodTaller = new SqlParameter("@cCodTaller", psCodTaller);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerEvaluacionesTaller, CommandType.StoredProcedure, sqlCodTaller))
                {
                    List<Negocio.Autoevaluacion> LstEvaluaciones = new List<Negocio.Autoevaluacion>();
                    Negocio.Autoevaluacion oEval;
                    while (reader.Read())
                    {
                        oEval = new Negocio.Autoevaluacion()
                        {
                            oUsuario = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cUser")])
                            },
                            oTipoEvaluacion = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCodEval")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEvalDesc")]),
                            },
                            oProcRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProcesoActual")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cProcActual")])
                            },
                            oEstado = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEstadoRiesgo")])
                            },

                            oTallerRiesgo = new Negocio.TallerRiesgo()
                            {
                                oCondicionRiesgo = new Negocio.Constante()
                                {
                                    nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEstadoTaller")]),
                                    cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cEstadoTaller")])
                                }
                            },

                            nNroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraReg")])),
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
