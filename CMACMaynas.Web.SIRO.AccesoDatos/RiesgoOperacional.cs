using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class RiesgoOperacional : IRiesgoOperacional
    {

        public string Registrar(Negocio.RiesgoOperacional model)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        SqlParameter[] parameters = {
                            new SqlParameter("@cRiesgoIdent", model.cRiesgoIdentiticado),
                            new SqlParameter("@cAgeCod", model.oAgencia.cAgeCod),
                            new SqlParameter("@cFechaDetec", Convert.ToDateTime(model.dFechaDeteccion)),
                            new SqlParameter("@cAreaCod", model.oAgencia.oArea.cAreaCod),
                            new SqlParameter("@cCausas", model.oCausas.cCodCausa),
                            new SqlParameter("@cProceso", model.oProceso.cCodProceso),
                            new SqlParameter("@nSubProceso", model.oProceso.oSubProceso.nCodSubProceso),
                            new SqlParameter("@cControlArea", null),
                            new SqlParameter("@bEfectControl", model.bControlEfectivo),
                            new SqlParameter("@cUltimaActualizacion", model.cNroRiesgo),
                            new SqlParameter("@cCodRiesgo", SqlDbType.VarChar, 25) { Direction = ParameterDirection.Output },
                            new SqlParameter("@nNroRiesgo", SqlDbType.Int) { Direction = ParameterDirection.Output },
                            new SqlParameter("@cObservacion", model.cEfectosRiesgo)
                        };

                        SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_RegistrarRiesgoOperacional, CommandType.StoredProcedure, parameters);

                        if (model.lstControles.Count > 0)
                        {
                            var nNroRiesgo = Convert.ToInt64(parameters[11].Value);
                            for (int i = 0; i < model.lstControles.Count; i++)
                            {
                                SqlParameter[] parametersControles = {
                                    new SqlParameter("@nNroRiesgo", nNroRiesgo),
                                    new SqlParameter("@nItem", (i+1)),
                                    new SqlParameter("@cDescControl", Convert.ToString(model.lstControles[i].cDescControles.Value)),
                                    new SqlParameter("@cFechaReg", model.cNroRiesgo)
                                };

                                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_AgregarControlesRiesgo, CommandType.StoredProcedure, parametersControles);
                            }
                        }
                        transaction.Complete();
                        return parameters[10].Value.ToString();
                    }
                    catch(Exception ex)
                    {
                        transaction.Dispose();
                        return string.Empty;
                    }
                    //    finally {
                    //        transaction.Dispose();
                    //        return string.Empty;
                    //    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int Actualizar(Negocio.RiesgoOperacional model)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        SqlParameter[] parameters = {
                            new SqlParameter("@nNroRiesgo", model.nNroRiesgo),
                            new SqlParameter("@cRiesgoIdent", model.cRiesgoIdentiticado),
                            new SqlParameter("@cAgeCod", model.oAgencia.cAgeCod),
                            new SqlParameter("@cFechaDetec", Convert.ToDateTime(model.dFechaDeteccion)),
                            new SqlParameter("@cAreaCod", model.oAgencia.oArea.cAreaCod),
                            new SqlParameter("@cCausas", model.oCausas.cCodCausa),
                            new SqlParameter("@cProceso", model.oProceso.cCodProceso),
                            new SqlParameter("@nSubProceso", model.oProceso.oSubProceso.nCodSubProceso),
                            //new SqlParameter("@cControlArea", null),
                            new SqlParameter("@bEfectControl", model.bControlEfectivo),
                            new SqlParameter("@cUltimaActualizacion", model.cNroRiesgo),
                            new SqlParameter("@cObservacion", model.cEfectosRiesgo)
                        };
                        var sqlExito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GuardarModificacionRiesgoOperacional, CommandType.StoredProcedure, parameters);

                        if (model.lstControles.Count > 0)
                        {
                            SqlParameter[] deleteParams = {
                                new SqlParameter("@nNroRiesgo", model.nNroRiesgo),
                                new SqlParameter("@cActualizaEliminacion", model.cNroRiesgo)
                            };

                            SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_del_EliminarControlesRiesgo, CommandType.StoredProcedure, deleteParams);

                            for (int i = 0; i < model.lstControles.Count; i++)
                            {

                                SqlParameter[] parametersControles = {
                                    new SqlParameter("@nNroRiesgo", model.nNroRiesgo),
                                    new SqlParameter("@nItem", (i+1)),
                                    new SqlParameter("@cDescControl", Convert.ToString(model.lstControles[i].cDescControles.Value)),
                                    new SqlParameter("@cFechaReg", model.cNroRiesgo)
                                };

                                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_AgregarControlesRiesgo, CommandType.StoredProcedure, parametersControles);
                            }
                        }
                        transaction.Complete();

                        return sqlExito;

                    }
                    finally { transaction.Dispose(); }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Obtiene informacion del registro inical del riesgo operacional
        /// </summary>
        /// <param name="pnNroRiesgo"></param>
        /// <returns></returns>
        public Negocio.RiesgoOperacional ObtenerInfoGeneralRiesgoOperacional(DetalleRiesgo model)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", model.nNroRiesgo);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDetalleRiesgoOperacional, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    Negocio.RiesgoOperacional oRiesgosDet = null;
                    while (reader.Read())
                    {
                        oRiesgosDet = new Negocio.RiesgoOperacional()
                        {
                            oUsuario = new Negocio.Usuario()
                            {
                                cUsuario = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cNombrecUserReporta")]),
                                cUser = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cUserReporta")])
                            },
                            oAgencia = new Negocio.Agencias()
                            {
                                cAgeCod = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAgeCod")]),
                                cAgeDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAgeDescripcion")]),
                                oArea = new Negocio.Areas()
                                {
                                    cAreaCod = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAreaCod")]),
                                    cAreaDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAreaDescripcion")])
                                }
                            },
                            oCausas = new Negocio.CausaRiesgo()
                            {
                                cCodCausa = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCodCausas")]),
                                cCausaDesc = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCausas")])
                            },
                            oProceso = new Negocio.ProcesoArea()
                            {
                                cCodProceso = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCodProceso")]),
                                cDescProceso = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cDescProceso")]),
                                oSubProceso = new Negocio.SubProcesos()
                                {
                                    nCodSubProceso = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCodSubProceso")]),
                                    cDescSubProceso = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cDescSubProceso")])
                                }
                            },
                            //Comentado por mejora de proceso
                            //oControlAreas = new Negocio.ControlProceso()
                            //{
                            //    nCodControl = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCodControl")]),
                            //    cControlDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cControlDescripcion")])
                            //},
                            oProcRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProcRiesgo")]),
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            oEstado = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nEstadoRiesgo")]),
                            },
                            oTipoRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nTpoRiesgo")])
                            },
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDetec")])),
                            cEfectosRiesgo = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cObservacion")]),
                            bControlEfectivo = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bControlEfect")])
                        };
                    }
                    return oRiesgosDet;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public List<Negocio.RiesgoOperacional> ObtenerDatosRiesgoOperacional(DetalleRiesgo riesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    //new SqlParameter("@cValorBusqueda", psBuscar),
                    new SqlParameter("@cUser", riesgo.oUsuario.cUser) //,
                    //new SqlParameter("@nTpoRiesgo", nTpoRiesgo),
                    //new SqlParameter("@nTpoBusqueda", pnTpoBusqueda)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarRiesgoOperacional, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.RiesgoOperacional> LstDatosRiesgo = new List<Negocio.RiesgoOperacional>();
                    Negocio.RiesgoOperacional oRiesgos;
                    while (reader.Read())
                    {
                        oRiesgos = new Negocio.RiesgoOperacional()
                        {
                            oUsuario = new Negocio.Usuario()
                            {
                                cUser = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cUser")])
                            },
                            oAgencia = new Negocio.Agencias()
                            {
                                cAgeCod = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAgeCod")]),
                                cAgeDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAgeDescripcion")]),
                                oArea = new Negocio.Areas()
                                {
                                    cAreaCod = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAreaCod")]),
                                    cAreaDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cAreaDescripcion")])
                                }
                            },
                            nNroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]),
                            cRiesgoIdentiticado = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                            cCodRiesgo = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCodRiesgo")]),
                            oTipoRiesgo = new Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nTpoRiesgo")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cTpoRiesgo")])
                            },
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaHoraReg")])),
                            dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDetec")])),
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

                        LstDatosRiesgo.Add(oRiesgos);
                    }
                    return LstDatosRiesgo;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public string[] GrabarProcesoPaso1(long pnNroRiesgo, int pnFactorRiesgo, int pnEventoPerdida, int pnSubEventoPerdida, string psLineaNeg, string psProducto, string psSubProducto, string psUltimaActializacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nFactorRiesgo", pnFactorRiesgo),
                    new SqlParameter("@nEventPerdida", pnEventoPerdida),
                    new SqlParameter("@nSubEventPerdida", pnSubEventoPerdida),
                    new SqlParameter("@cLineaNeg", psLineaNeg),
                    new SqlParameter("@cProducto", psProducto),
                    new SqlParameter("@cSubProdcto", psSubProducto),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActializacion),
                    new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }
                };


                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarGestionRiesgoOperacionaPaso1, CommandType.StoredProcedure, parameters);
                string[] respuesta = parameters[8].Value.ToString().Split(',');
                return respuesta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string[] GrabarProcesoPaso2(long pnNroRiesgo, int pnProbabilidad, int pnImpacto, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nProbabilidad", pnProbabilidad),
                    new SqlParameter("@nImpacto", pnImpacto),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarGestionRiesgoOperacionaPaso2, CommandType.StoredProcedure, parameters);
                string[] respuesta = parameters[4].Value.ToString().Split(',');
                return respuesta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public string[] GrabarProcesoPaso3(long pnNroRiesgo, string psComentarios, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cComentarios", psComentarios),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }
            };


                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarGestionRiesgoOperacionaPaso3, CommandType.StoredProcedure, parameters);
                string[] respuesta = parameters[3].Value.ToString().Split(',');
                return respuesta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string[] GrabarProcesoPaso4(long pnNroRiesgo, /*string psComentarios,*/ string psUltimaActualizacion)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);
                //SqlParameter sqlComentario = new SqlParameter("@cComentarios", psComentarios);
                SqlParameter sqlUltimaActulizacion = new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion);
                SqlParameter sqlMensaje = new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarGestionRiesgoOperacionaPaso4, CommandType.StoredProcedure, sqlNroRiesgo, sqlUltimaActulizacion, sqlMensaje);
                string[] respuesta = sqlMensaje.Value.ToString().Split(',');
                return respuesta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int GrabarProcesoPaso5(long pnNroRiesgo, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);
                SqlParameter sqlUltimaActulizacion = new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion);

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarGestionRiesgoOperacionaPaso5, CommandType.StoredProcedure, sqlNroRiesgo, /*sqlComentario,*/ sqlUltimaActulizacion);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<string> ObtenerInfoGestionRiesgo(long pnNroRiesgo, int pnProceso)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);
                SqlParameter sqlProceso = new SqlParameter("@nPaso", pnProceso);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerInfoProcesoGestionRiesgoOperacional, CommandType.StoredProcedure, sqlNroRiesgo, sqlProceso))
                {
                    List<string> LstInfoProceso = new List<string>();
                    while (reader.Read())
                    {
                        if (pnProceso == 1)
                        {
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nNroRiesgo")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nFactorRiesgo")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nEventPerdida")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nSubEventoPerdida")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("cLineaNeg")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("cProducto")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("cSubProdcto")]));
                        }
                        else if (pnProceso == 2)
                        {
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nNroRiesgo")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nProbabilidad")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nImpacto")]));
                        }
                        else if (pnProceso == 3)
                        {
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("nNroRiesgo")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("bContRiesResidual")]));
                            LstInfoProceso.Add(Convert.ToString(reader[reader.GetOrdinal("cComentRiesgoResidual")]));
                        }

                        //DatosRiesgosEN datos = new DatosRiesgosEN();

                    }
                    return LstInfoProceso;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int GrabarObservacionGestion(long pnNroRiesgo, List<dynamic> poObservaciones, string psUltimaActializacion)
        {
            TransactionScope transaction = new TransactionScope();
            int lnExito = 0;
            try
            {
                using (transaction)
                {
                    for (int i = 0; i < poObservaciones.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(poObservaciones[i].cObservacion.Value))
                        {
                            SqlParameter[] parameters = {
                                new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                                new SqlParameter("@nProceso", (int)poObservaciones[i].nProceso.Value),
                                new SqlParameter("@cDescObservacion", poObservaciones[i].cObservacion.Value),
                                new SqlParameter("@cUltimaActualizacion", psUltimaActializacion)
                            };

                            lnExito += SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarObservacionGestion, CommandType.StoredProcedure, parameters);
                        }
                    }
                    transaction.Complete();
                }
            }
            catch (Exception ex) { transaction.Dispose(); lnExito = 0; throw new Exception(ex.Message); }
            return lnExito;

        }

        public List<dynamic> ObtenerObservacionesGestion(long pnNroRiesgo, int pnProcedencia = 2)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nProcedencia", pnProcedencia)
                };


                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerObservacionesRiesgo, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstObservaciones = new List<dynamic>();
                    //ObservacionGestionRiesgoEN oObservaciones = null;
                    dynamic dObservaciones = null;
                    while (reader.Read())
                    {
                        if (pnProcedencia == 1) //Riesgos Operacionales
                        {
                            dObservaciones = new System.Dynamic.ExpandoObject();
                            dObservaciones.NroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]);
                            dObservaciones.Proceso = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProceso")]);
                            dObservaciones.Observacion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cDescObservacion")]);

                        }
                        else if (pnProcedencia == 2) //Evaluaciones
                        {
                            dObservaciones = new System.Dynamic.ExpandoObject();
                            dObservaciones.NroRiesgo = (long)DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nNroRiesgo")]);
                            dObservaciones.Proceso = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nProceso")]);
                            dObservaciones.Observacion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cDescObservacion")]);
                            dObservaciones.CodCondicion = DataDefault.DbValueToDefault<int>(reader[reader.GetOrdinal("nCondicion")]);
                            dObservaciones.DescCondicion = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cCondicionTaller")]);
                        }

                        LstObservaciones.Add(dObservaciones);
                    }
                    return LstObservaciones;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int ConfirmarModificacionesRiesgo(long pnNroRiesgo, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_GrabarConfirmacionModificacion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RechazarRiesgo(long pnNroRiesgo, string psMotivoRechazo, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cMotivoRechazo", psMotivoRechazo),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                    };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_RechazarRiesgo, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int EliminarRiesgo(long pnNroRiesgo, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_EliminarRiesgo, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int ObtenerTpoRiesgo(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nTpoRiesgo", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerTipoRiesgo, CommandType.StoredProcedure, parameters);

                return (int)parameters[1].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
