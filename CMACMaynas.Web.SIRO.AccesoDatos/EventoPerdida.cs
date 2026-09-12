using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class EventoPerdida : IEventoPerdida
    {
        
        public List<dynamic> ObtenerClaseEventoPerdida()
        {

            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerClaseEventoPerdida, CommandType.StoredProcedure))
                {
                    List<dynamic> LstClaseEventoPerdida = new List<dynamic>();
                    dynamic oClaseEventoPerdida;
                    while (reader.Read())
                    {
                        oClaseEventoPerdida = new System.Dynamic.ExpandoObject();
                        oClaseEventoPerdida.nCodClaseEventoP = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodClasEventoP")]);
                        oClaseEventoPerdida.cDescClaseEventoP = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]);
                        oClaseEventoPerdida.bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]);
                        LstClaseEventoPerdida.Add(oClaseEventoPerdida);
                    }
                    return LstClaseEventoPerdida;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ObtenerSubClaseEventoPerdida(int pnCodClaseEventoP)
        {

            try
            {
                SqlParameter[] parameters = { new SqlParameter("@nCodClaseEventoP", pnCodClaseEventoP) };
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerSubClaseEventoPerdida, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstSubClaseEventoPerdida = new List<dynamic>();
                    dynamic oSubClaseEventoPerdida;
                    while (reader.Read())
                    {
                        oSubClaseEventoPerdida = new System.Dynamic.ExpandoObject();
                        oSubClaseEventoPerdida.nCodClasEventoP = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodClasEventoP")]);
                        oSubClaseEventoPerdida.nCodSubClasEventoP = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodSubClasEventoP")]);
                        oSubClaseEventoPerdida.cDescSubClasEventoP = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubClasEventoP")]);
                        oSubClaseEventoPerdida.bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]);
                        LstSubClaseEventoPerdida.Add(oSubClaseEventoPerdida);
                    }
                    return LstSubClaseEventoPerdida;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
        public List<Negocio.EventoPerdida> ObtenerEventosPerdida()
        {
            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarEventosPerdida, CommandType.StoredProcedure))
                {
                    List<Negocio.EventoPerdida> LstEventoPerdida = new List<Negocio.EventoPerdida>();
                    Negocio.EventoPerdida oEventoPerdida;
                    while (reader.Read())
                    {
                        oEventoPerdida = new Negocio.EventoPerdida();
                        oEventoPerdida.oDatosRiesgo = new Negocio.DatosRiesgos();
                        oEventoPerdida.oClasesEventoPerdida = new Negocio.ClaseEventoPerdida();
                        oEventoPerdida.oClasesEventoPerdida.oSubClaseEventoPerdida = new Negocio.SubClaseEventoPerdida();

                        oEventoPerdida.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oEventoPerdida.oDatosRiesgo.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oEventoPerdida.oDatosRiesgo.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oEventoPerdida.oClasesEventoPerdida.cDescClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]);
                        oEventoPerdida.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubClasEventoP")]);
                        oEventoPerdida.dFechaDescubrimiento = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaOcurrencia")]);
                        oEventoPerdida.nMontoBruto = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoBruto")]));
                        oEventoPerdida.nPerdidaNeta = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nPerdidaNeta")]));
                        //oEventoPerdida.cCtaContCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodCtaCont")]);
                        oEventoPerdida.cAnio = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAnio")]);
                        LstEventoPerdida.Add(oEventoPerdida);
                    }
                    return LstEventoPerdida;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.EventoPerdida ObtenerDetalleEventoPerdida(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_DetalleEventoPerdida, CommandType.StoredProcedure, parameters))
                {
                    Negocio.EventoPerdida oEventoPerdida = null;
                    while (reader.Read())
                    {
                        oEventoPerdida = new Negocio.EventoPerdida()
                        {
                            oDatosRiesgo = new Negocio.DatosRiesgos()
                            {
                                nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                                cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                                cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                                oAgencias = new Negocio.Agencias()
                                {
                                    cAgeCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeCod")]),
                                    cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]),
                                },
                                oAreas = new Negocio.Areas()
                                {
                                    cAreaCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaCod")]),
                                    cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")])
                                },
                                oProceso = new Negocio.ProcesoArea()
                                {
                                    cCodProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodProceso")]),
                                    cDescProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProceso")]),
                                    oSubProceso = new Negocio.SubProcesos()
                                    {
                                        nCodSubProceso = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodSubProceso")]),
                                        cDescSubProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubProceso")])
                                    }
                                }
                            },
                            oClasesEventoPerdida = new Negocio.ClaseEventoPerdida()
                            {
                                nCodClasEvento = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodClasEventoP")]),
                                cDescClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]),
                                oSubClaseEventoPerdida = new Negocio.SubClaseEventoPerdida()
                                {
                                    nCodSubClasEvento = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodSubClasEventoP")]),
                                    cDescSubClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubClasEventoP")])
                                }
                            },
                            oDescCortaEventoPerdida = new Negocio.Constante()
                            {
                                nConsCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCobertura")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCobertura")])
                            },
                            oLineaNeg = new Negocio.LineaNegocio()
                            {
                                cCodLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodLineaNeg")]),
                                cDescLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescLineaNeg")]),
                                oSubLineaNeg = new Negocio.SubLineaNegocio()
                                {
                                    cCodSubLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodSubLineaNeg")]),
                                    cDescSubLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubLineaNeg")]),
                                }
                            },
                            oCobertura = new Negocio.Constante()
                            {
                                nConsCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCobertura")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCobertura")])
                            },
                            cGrupoEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cGrupoEvento")]),
                            cMedidasCorrectivas = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescMedidas")]),
                            cAccionesRealizada = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescAcciones")]),
                            bReportado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bReportado")]),
                            cAnio = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAnio")]),
                            dFechaRegCont = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaRegContable")])),
                            dFechaOcurrencia = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaOcurrencia")])),
                            dFechaDescubrimiento = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDescubrimiento")])),
                            cPenMontoPerdida = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("nPenMontoPerdida")]),
                            nMontoPerdida = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoPerdida")])),
                            cPenMontoRecup = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("nPenMontoRecup")]),
                            nMontoRecup = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoRecup")])),
                            nMontoBruto = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoBruto")])),
                            cPenMontoProvision = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("nPenProvision")]),
                            nMontoProvision = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoProvision")])),
                            nPerdidaNeta = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nPerdidaNeta")])),
                            bAsociaRiesgo = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bAsocRiesgo")]),
                            bGrupo = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bGrupo")]),
                            cUserRegistra = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUsuarioReg")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaRegEvento")]))
                        };
                    }
                    return oEventoPerdida;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.SubClaseEventoPerdida> ObtenerSubEventoPerdida(int pnCodEvento)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnCodEvento);
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerSubEventoPerdida, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    List<Negocio.SubClaseEventoPerdida> LstSubEventoPerdida = new List<Negocio.SubClaseEventoPerdida>();
                    Negocio.SubClaseEventoPerdida oSubEventoPerdida;
                    while (reader.Read())
                    {
                        oSubEventoPerdida = new Negocio.SubClaseEventoPerdida();
                        oSubEventoPerdida.nCodSubClasEvento = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodSubEvento")]);
                        oSubEventoPerdida.cDescSubClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubEvento")]);
                        LstSubEventoPerdida.Add(oSubEventoPerdida);
                    }
                    return LstSubEventoPerdida;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public string[] GrabarGestionEventoPerdida(long pnNroRiesgo, string psAgeCod, string psAreaCod, string psLineNeg, string psSubLineaNeg, string psCausas,
                                            string psProcesos, string psSubProcesos, int pnSubEventoPerdida, bool pbRiesgoCrediticio, bool pbExEventoPerdida, string psUltimaActualizacion,
                                            string psMedidasCorrectivas = "", string psAccionRealizada = "")
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cAgeCod", psAgeCod),
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@cLineaCod", psLineNeg),
                    new SqlParameter("@cSubLineaCod", psSubLineaNeg),
                    new SqlParameter("@cCausas", psCausas),
                    new SqlParameter("@cProcesos", psProcesos),
                    new SqlParameter("@nSubProcesos", psSubProcesos),
                    new SqlParameter("@nSubEvenPerdida", pnSubEventoPerdida),
                    new SqlParameter("@bRiesgoCrediticio", pbRiesgoCrediticio),
                    new SqlParameter("@bExpEventoPerdida", pbExEventoPerdida),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cMedidasCorrectivas", psMedidasCorrectivas),
                    new SqlParameter("@cAccionesRealizadas", psAccionRealizada),
                    new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabaGestionEventoPerdida, CommandType.StoredProcedure, parameters);

                string[] respuesta = parameters[14].Value.ToString().Split(',');
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public dynamic ObtenerCuentasContables(string psFiltroCta)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cFiltroCta", psFiltroCta)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCtaCont, CommandType.StoredProcedure, parameters))
                {
                    // List<dynamic> LstCtaCont = new List<dynamic>();
                    dynamic oCuentas = null;
                    while (reader.Read())
                    {
                        oCuentas = new System.Dynamic.ExpandoObject();
                        oCuentas.cCtaCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCtaContCod")]);
                        oCuentas.cCtaCodDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCtaContDesc")]);
                        // LstCtaCont.Add(oCuentas);
                    }
                    return oCuentas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public double ObtenerTipoCambio(string psFecha)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@dFecha", psFecha),
                    new SqlParameter("@nValorCambio", SqlDbType.Float){Direction = ParameterDirection.Output}
                };
                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerTipoCambioxFecha, CommandType.StoredProcedure, parameters);

                return Convert.ToDouble(parameters[1].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string GrabarEventoPerdida(int pnCodDescCorta, string psEventoDesc, string psMedidas, string psAcciones, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                          string psAnio, int pnClasEventoP, int pnSubClasEventoP, bool pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                          int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                          int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, bool pbAsocRiesgo, string psUltimaActualizacion, string psCodEventoPadre = "",
                                          List<dynamic> poDetCuentas = null, List<dynamic> poDetGastos = null)
        {
            TransactionScope transaction = new TransactionScope();
            string[] respuesta = null;
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nCodDescCorta", pnCodDescCorta),
                    new SqlParameter("@cDescEventoP", psEventoDesc),
                    new SqlParameter("@cDescMedidas", psMedidas),
                    new SqlParameter("@cDescAcciones", psAcciones),
                    new SqlParameter("@cAgeCod", psAgeCod),
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@dFechaOcurrencia", Convert.ToDateTime(pdFechaOcurrencia)),
                    new SqlParameter("@dFechaDescubrimiento", Convert.ToDateTime(pdFechaDescubrimiento)),
                    new SqlParameter("@dFechaRegContable", Convert.ToDateTime(pdFechaRegContable)),
                    new SqlParameter("@cAnio", psAnio),
                    new SqlParameter("@nClasEventoP", pnClasEventoP),
                    new SqlParameter("@nClasSubEventoP", pnSubClasEventoP),
                    new SqlParameter("@bReportado", pnReportado),
                    new SqlParameter("@cCodCtaCont", psCtaCont),
                    new SqlParameter("@cLineaNeg", psLineaNeg),
                    new SqlParameter("@cSubLineaNeg", psSubLineaNeg),
                    new SqlParameter("@nCobertura", pnCobertura),
                    new SqlParameter("@cProcesos", psProceso),
                    new SqlParameter("@nSubProceso", pnSubProceso),
                    new SqlParameter("@nPenMontoPerdida", pnPenMontoPerdida),
                    new SqlParameter("@nMontoPerdida", pnMontoPerdida),
                    new SqlParameter("@nPenMontoRecup", pnPenMontoRecupera),
                    new SqlParameter("@nMontoRecup", pnMontoRecuperado),
                    new SqlParameter("@nMontoBruto", pnMontoBruto),
                    new SqlParameter("@nPenProvision", pnPenMontoProvisiona),
                    new SqlParameter("@nMontoProvision", pnMontoProvision),
                    new SqlParameter("@nPerdidaNeta", pnPerdidaNeta),
                    new SqlParameter("@bAsocRiesgo", pbAsocRiesgo),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cRespuesta", SqlDbType.VarChar, 150){ Direction = ParameterDirection.Output}
                };
                using (transaction)
                {
                    SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarEventoPerdida, CommandType.StoredProcedure, parameters);
                    respuesta = Convert.ToString(parameters[29].Value).Split(',');

                    //Cuentas
                    if (poDetCuentas != null)
                    {
                        for (int i = 0; i < poDetCuentas.Count; i++)
                        {
                            SqlParameter[] parametersCuentas = {
                            new SqlParameter("@nNroRiesgo", (long)Convert.ToInt32(respuesta[0])),
                            new SqlParameter("@nItem", (i+1)),
                            new SqlParameter("@cCtaCod", Convert.ToString(poDetCuentas[i].cCodCuenta.Value))
                            };

                            SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarCuentasEventoPerdida, CommandType.StoredProcedure, parametersCuentas);
                        }
                    }

                    //Gastos
                    if (poDetGastos != null)
                    {
                        for (int i = 0; i < poDetGastos.Count; i++)
                        {
                            SqlParameter[] parametersGastos = {
                            new SqlParameter("@nNroRiesgo", (long)Convert.ToInt32(respuesta[0])),
                            new SqlParameter("@cGlosa", Convert.ToString(poDetGastos[i].cGlosaGasto.Value)),
                            new SqlParameter("@nMoneda", Convert.ToInt32(poDetGastos[i].nPenGasto.Value)),
                            new SqlParameter("@nMonto", Convert.ToDouble(poDetGastos[i].nMontoGasto.Value))
                            };

                            SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarGastosEvento, CommandType.StoredProcedure, parametersGastos);
                        }
                    }

                    //Es agrupado
                    if (psCodEventoPadre != "")
                    {
                        SqlParameter[] parametersCuentas = {
                            new SqlParameter("@cCodEventoPadre", psCodEventoPadre),
                            new SqlParameter("@nAgrupado", (long)Convert.ToInt32(respuesta[0])),
                            new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                        };
                        SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarEventoPerdidaAgrupado, CommandType.StoredProcedure, parametersCuentas);
                    }

                    transaction.Complete();
                }
                return Convert.ToString(respuesta[1]);
            }
            catch (Exception ex) { transaction.Dispose(); throw new Exception(ex.Message); }
        }


        public bool GrabarActualizacionEventoPerdida(long pnCodEvento, int pnCodDescCorta, string psEventoDesc, string psMedidas, string psAcciones, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                       string psAnio, int pnClasEventoP, int pnSubClasEventoP, bool pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                       int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                       int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, bool pbAsocRiesgo, string psUltimaActualizacion, string psCodEventoPadre = "",
                                       List<dynamic> poDetCuentas = null, List<dynamic> poDetGastos = null)
        {
            TransactionScope transaction = new TransactionScope();
            bool respuesta = false;
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnCodEvento),
                    new SqlParameter("@nCodDescCorta", pnCodDescCorta),
                    new SqlParameter("@cDescEventoP", psEventoDesc),
                    new SqlParameter("@cDescMedidas", psMedidas),
                    new SqlParameter("@cDescAcciones", psAcciones),
                    new SqlParameter("@cAgeCod", psAgeCod),
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@dFechaOcurrencia", Convert.ToDateTime(pdFechaOcurrencia)),
                    new SqlParameter("@dFechaDescubrimiento", Convert.ToDateTime(pdFechaDescubrimiento)),
                    new SqlParameter("@dFechaRegContable", Convert.ToDateTime(pdFechaRegContable)),
                    new SqlParameter("@cAnio", psAnio),
                    new SqlParameter("@nClasEventoP", pnClasEventoP),
                    new SqlParameter("@nClasSubEventoP", pnSubClasEventoP),
                    new SqlParameter("@bReportado", pnReportado),
                    new SqlParameter("@cCodCtaCont", psCtaCont),
                    new SqlParameter("@cLineaNeg", psLineaNeg),
                    new SqlParameter("@cSubLineaNeg", psSubLineaNeg),
                    new SqlParameter("@nCobertura", pnCobertura),
                    new SqlParameter("@cProcesos", psProceso),
                    new SqlParameter("@nSubProceso", pnSubProceso),
                    new SqlParameter("@nPenMontoPerdida", pnPenMontoPerdida),
                    new SqlParameter("@nMontoPerdida", pnMontoPerdida),
                    new SqlParameter("@nPenMontoRecup", pnPenMontoRecupera),
                    new SqlParameter("@nMontoRecup", pnMontoRecuperado),
                    new SqlParameter("@nMontoBruto", pnMontoBruto),
                    new SqlParameter("@nPenProvision", pnPenMontoProvisiona),
                    new SqlParameter("@nMontoProvision", pnMontoProvision),
                    new SqlParameter("@nPerdidaNeta", pnPerdidaNeta),
                    new SqlParameter("@bAsocRiesgo", pbAsocRiesgo),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };
                using (transaction)
                {
                    int actualiza = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarActulizacionEventoPerdida, CommandType.StoredProcedure, parameters);
                    if (actualiza > 0)
                    {
                        //Cuentas del Evento de Perdida
                        if (poDetCuentas != null)
                        {
                            //Eliminanos las cuentas
                            int eliminado = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_del_EliminarCtaContEventoPerdida, CommandType.StoredProcedure, new SqlParameter("@nNroRiesgo", pnCodEvento));
                            for (int i = 0; i < poDetCuentas.Count; i++)
                            {
                                SqlParameter[] parametersCuentas = {
                            new SqlParameter("@nNroRiesgo", pnCodEvento),
                            new SqlParameter("@nItem", (i+1)),
                            new SqlParameter("@cCtaCod", Convert.ToString(poDetCuentas[i].cCodCuenta.Value))
                            };
                                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarCuentasEventoPerdida, CommandType.StoredProcedure, parametersCuentas);
                            }
                        }

                        //Gastos del Evento de Perdida
                        if (poDetGastos != null)
                        {
                            //Eliminanos los gastos
                            int eliminado = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_del_EliminarGastosEventoPerdida, CommandType.StoredProcedure, new SqlParameter("@nNroRiesgo", pnCodEvento));
                            for (int i = 0; i < poDetGastos.Count; i++)
                            {
                                SqlParameter[] parametersGastos = {
                            new SqlParameter("@nNroRiesgo", pnCodEvento),
                            new SqlParameter("@cGlosa", Convert.ToString(poDetGastos[i].cGlosaGasto.Value)),
                            new SqlParameter("@nMoneda", Convert.ToInt32(poDetGastos[i].nPenGasto.Value)),
                            new SqlParameter("@nMonto", Convert.ToDouble(poDetGastos[i].nMontoGasto.Value))
                            };
                                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarGastosEvento, CommandType.StoredProcedure, parametersGastos);
                            }
                        }
                        respuesta = true;
                    }
                    else {
                        transaction.Dispose();
                        respuesta = false;
                    }
                    transaction.Complete();
                }
                return respuesta;
            }
            catch (Exception ex) { transaction.Dispose(); throw new Exception(ex.Message); }
        }

        public List<dynamic> ObtenerDetGastosEventoPerdida(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDetGastoEventoPerdida, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstGastos = new List<dynamic>();
                    dynamic oGastos = null;
                    while (reader.Read())
                    {
                        oGastos = new System.Dynamic.ExpandoObject();
                        oGastos.NroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oGastos.Item = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nItem")]);
                        oGastos.Concepto = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cGlosa")]);
                        oGastos.Moneda = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nMoneda")]);
                        oGastos.Monto = DataDefault.DbValueToDefault<Double>(DataDefault.toDoubleDefault(reader[reader.GetOrdinal("nMonto")]));
                        LstGastos.Add(oGastos);
                    }
                    return LstGastos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ObtenerDetCtaContablesEventoPerdida(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCtaContablesEventoPerdida, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstCuentas = new List<dynamic>();
                    dynamic oCuentas = null;
                    while (reader.Read())
                    {
                        oCuentas = new System.Dynamic.ExpandoObject();
                        oCuentas.Item = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nItem")]);
                        oCuentas.Codigo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCtaContCod")]);
                        oCuentas.cCtaContDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCtaContDesc")]);
                        LstCuentas.Add(oCuentas);
                    }
                    return LstCuentas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.EventoPerdida> ObtenerMatrizEventoPerdida(string psDesde, string psHasta)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cDesde", psDesde),
                    new SqlParameter("@cHasta", psHasta)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarMatrizEventosPerdida, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.EventoPerdida> LstEventos = new List<Negocio.EventoPerdida>();
                    Negocio.EventoPerdida oEvento = null;
                    while (reader.Read())
                    {
                        oEvento = new Negocio.EventoPerdida();
                        oEvento.oClasesEventoPerdida = new Negocio.ClaseEventoPerdida();
                        oEvento.oClasesEventoPerdida.oSubClaseEventoPerdida = new Negocio.SubClaseEventoPerdida();
                        oEvento.oDatosRiesgo = new Negocio.DatosRiesgos();

                        oEvento.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oEvento.oDatosRiesgo.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oEvento.oDatosRiesgo.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oEvento.oClasesEventoPerdida.cDescClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]);
                        oEvento.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubClasEventoP")]);
                        oEvento.nMontoBruto = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nMontoBruto")]));
                        oEvento.nPerdidaNeta = DataDefault.DbValueToDefault<Double>(Convert.ToDouble(reader[reader.GetOrdinal("nPerdidaNeta")]));
                        oEvento.dFechaOcurrencia = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaOcurrencia")]);
                        oEvento.cCtaContDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodCtaCont")]);
                        oEvento.cAnio = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAnio")]);
                        LstEventos.Add(oEvento);
                    }
                    return LstEventos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //public string[] GrabarAgrupacionEventoPerdida(long pnNroRiesgo, string psAgrupado, string psUltimaActualizacion)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = {
        //            new SqlParameter("@nNroRiesgo", pnNroRiesgo),
        //            new SqlParameter("@cAgrupado", psAgrupado),
        //            new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
        //            new SqlParameter("@cRespuesta", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output}
        //        };

        //        SqlHelper.ExecuteNonQuery(conectar.GetConnectionString(), SP.stp_ins_GrabarAgrupacionEvento, CommandType.StoredProcedure, parameters);

        //        string[] respuesta = parameters[3].Value.ToString().Split(',');

        //        return respuesta;

        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        public List<Negocio.EventoPerdida> ObtenerEventoPerdidaAgrupados(/*string psDesde, string psHasta*/)
        {
            try
            {
                //SqlParameter[] parameters = {
                //    new SqlParameter("@cDesde", psDesde),
                //    new SqlParameter("@cHasta", psHasta)
                //};

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListaEventoAgrupar, CommandType.StoredProcedure /*, parameters*/))
                {
                    List<Negocio.EventoPerdida> LstEventos = new List<Negocio.EventoPerdida>();
                    Negocio.EventoPerdida oEvento = null;
                    while (reader.Read())
                    {
                        oEvento = new Negocio.EventoPerdida();
                        oEvento.oClasesEventoPerdida = new Negocio.ClaseEventoPerdida();
                        oEvento.oClasesEventoPerdida.oSubClaseEventoPerdida = new Negocio.SubClaseEventoPerdida();
                        oEvento.oDatosRiesgo = new Negocio.DatosRiesgos();

                        oEvento.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oEvento.oDatosRiesgo.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oEvento.oDatosRiesgo.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oEvento.oClasesEventoPerdida.cDescClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescClasEventoP")]);
                        oEvento.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubClasEventoP")]);
                        oEvento.nMontoBruto = DataDefault.DbValueToDefault<Double>(DataDefault.toDoubleDefault(reader[reader.GetOrdinal("nMontoBruto")]));
                        oEvento.nPerdidaNeta = DataDefault.DbValueToDefault<Double>(DataDefault.toDoubleDefault(reader[reader.GetOrdinal("nPerdidaNeta")]));
                        //oEvento.dFechaOcurrencia = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaOcurrencia")]);
                        oEvento.cCtaContDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodCtaCont")]);
                        oEvento.cAnio = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAnio")]);
                        LstEventos.Add(oEvento);
                    }
                    return LstEventos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.EventoPerdida> ObtenerAgrupacionEvento(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerAgrupacionEvento, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.EventoPerdida> LstEventos = new List<Negocio.EventoPerdida>();
                    Negocio.EventoPerdida oEvento = null;
                    while (reader.Read())
                    {
                        oEvento = new Negocio.EventoPerdida()
                        {
                            oDatosRiesgo = new Negocio.DatosRiesgos()
                            {
                                nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                                cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            },
                            cUserRegistra = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserReg")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")]))
                        };
                        LstEventos.Add(oEvento);
                    }
                    return LstEventos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Lista los evento de perdida que no pertenece a ningun grupo
        /// </summary>
        /// <returns></returns>
        public List<Negocio.EventoPerdida> ListarEventoPerdidaNoGrupo()
        {
            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarEventoNoGrupo, CommandType.StoredProcedure))
                {
                    List<Negocio.EventoPerdida> LstEventos = new List<Negocio.EventoPerdida>();
                    Negocio.EventoPerdida oEvento = null;
                    while (reader.Read())
                    {
                        oEvento = new Negocio.EventoPerdida();
                        oEvento.oClasesEventoPerdida = new Negocio.ClaseEventoPerdida();
                        oEvento.oClasesEventoPerdida.oSubClaseEventoPerdida = new Negocio.SubClaseEventoPerdida();
                        oEvento.oDatosRiesgo = new Negocio.DatosRiesgos();

                        oEvento.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oEvento.oDatosRiesgo.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oEvento.oDatosRiesgo.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        LstEventos.Add(oEvento);
                    }
                    return LstEventos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool ValidarEventoPerdida(string psCodEvento)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodEvento", psCodEvento)
                };
                var validar = false;
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_VerificarEventoPerdida, CommandType.StoredProcedure, parameters))
                {
                    if (reader.Read())
                    {
                        if (reader["cCodRiesgo"] != DBNull.Value)
                        {
                            validar = true;
                        }
                    }
                    return validar;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public int EliminarEventoGrupo(long pnCodEvento)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodEvento", pnCodEvento)
                };
                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_EliminarEventoGrupo, CommandType.StoredProcedure, parameters);
                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
