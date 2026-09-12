using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class PlanAccion : IPlanAccion
    {

        public List<Negocio.PlanAccion> MostrarPlanesAccionRiesgo(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerPlanesAccion, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.PlanAccion> lstPlanAccion = new List<Negocio.PlanAccion>();
                    Negocio.PlanAccion oPlanAccion;

                    while (reader.Read())
                    {
                        oPlanAccion = new Negocio.PlanAccion()
                        {
                            oDatosRiesgo = new Negocio.DatosRiesgos()
                            {
                                nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")])
                            },
                            nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]),
                            cPlanCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPlanCod")]),
                            cPlanDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPlanDescripcion")]),
                            bSugerenciaGM = Convert.ToBoolean(reader[reader.GetOrdinal("bSugeGerenManc")]),
                            cComentario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cComentario")]),
                            dFechaImplement = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaImplement")])),
                            nEstado = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]),
                            cEstado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaReg")])),
                            cNombreDoc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNombreDocumento")]),
                            dFechaUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cUltimaActualizacion")])),

                        };
                        lstPlanAccion.Add(oPlanAccion);
                    }
                    return lstPlanAccion;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.ResponsablePlanAccion> MostrarResponsablesPlanesAccionRiesgo(long pnNroRiesgo)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_MostrarRespndablesPlanesAccion, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    List<Negocio.ResponsablePlanAccion> lstResponPlanAccion = new List<Negocio.ResponsablePlanAccion>();
                    Negocio.ResponsablePlanAccion oResponPlanAccion;

                    while (reader.Read())
                    {
                        oResponPlanAccion = new Negocio.ResponsablePlanAccion()
                        {
                            oPlanAccion = new Negocio.PlanAccion()
                            {
                                nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]),
                                cPlanDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPlanDescripcion")])
                            },
                            oDatosRiesgo = new Negocio.DatosRiesgos()
                            {
                                nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")])
                            },
                            cUserResponsable = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNombreResponsables")])
                        };
                        lstResponPlanAccion.Add(oResponPlanAccion);
                    }
                    return lstResponPlanAccion;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.ResponsablePlanAccion> MostrarDetalleResponsablesPlanesAccionRiesgo(long pnNroRiesgo, long pnPlanCod)
        {
            try
            {
                SqlParameter[] parameter = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nPlanCod", pnPlanCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerResponsablePlanAccion, CommandType.StoredProcedure, parameter))
                {
                    List<Negocio.ResponsablePlanAccion> lstResponPlanAccion = new List<Negocio.ResponsablePlanAccion>();
                    Negocio.ResponsablePlanAccion oResponPlanAccion;

                    while (reader.Read())
                    {
                        oResponPlanAccion = new Negocio.ResponsablePlanAccion();
                        oResponPlanAccion.oPersona = new Negocio.Persona();
                        oResponPlanAccion.oPlanAccion = new Negocio.PlanAccion();
                        oResponPlanAccion.oDatosRiesgo = new Negocio.DatosRiesgos();
                        oResponPlanAccion.oDatosRiesgo.oAgencias = new Negocio.Agencias();

                        oResponPlanAccion.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oResponPlanAccion.oPlanAccion.nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]);
                        oResponPlanAccion.nItemResponPlan = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nItem")]);
                        oResponPlanAccion.oPersona.cPersCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPersCod")]);
                        oResponPlanAccion.cUserResponsable = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]);
                        oResponPlanAccion.oPersona.cPersNombre = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPersNombre")]);
                        oResponPlanAccion.oDatosRiesgo.oAgencias.cAgeCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeCod")]);
                        oResponPlanAccion.oDatosRiesgo.oAgencias.cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]);
                        lstResponPlanAccion.Add(oResponPlanAccion);
                    }
                    return lstResponPlanAccion;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.PlanAccion ObtenerPlanAccionRiesgoPlan(long pnNroRiesgo, long pnCodPlan)
        {
            try
            {
                SqlParameter[] parameter = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nCodPlanAccion", pnCodPlan)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerPlanAccionRiesgoPlan, CommandType.StoredProcedure, parameter))
                {
                    Negocio.PlanAccion oPlanAccion = null;
                    while (reader.Read())
                    {
                        oPlanAccion = new Negocio.PlanAccion();
                        oPlanAccion.oDatosRiesgo = new Negocio.DatosRiesgos();
                        oPlanAccion.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oPlanAccion.nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]);
                        oPlanAccion.cPlanDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPlanDescripcion")]);
                        oPlanAccion.dFechaImplement = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaImplement")]));
                        oPlanAccion.bSugerenciaGM = Convert.ToBoolean(reader[reader.GetOrdinal("bSugeGerenManc")]);
                        oPlanAccion.nEstado = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]);
                        oPlanAccion.cEstado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")]);
                        oPlanAccion.cIconoEstado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cIconoEstado")]);
                        oPlanAccion.cComentario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cComentario")]);
                        oPlanAccion.dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaReg")]));
                        oPlanAccion.dFechaUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cUltimaActualizacion")]));
                    }
                    return oPlanAccion;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.PlanAccion> ObtenerPlanAccionResponsable(string psUsuario, int pnTpoRiesgo, string psFiltro = "")
        {
            try
            {
                SqlParameter[] parameter = {
                    new SqlParameter("@cUser", psUsuario),
                    new SqlParameter("@nTpoRiesgo", pnTpoRiesgo),
                    new SqlParameter("@cBuscar", psFiltro)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerPlanesAccionAsignados, CommandType.StoredProcedure, parameter))
                {
                    List<Negocio.PlanAccion> oLstPlanAccion = new List<Negocio.PlanAccion>();
                    Negocio.PlanAccion oPlanAccion = null;
                    while (reader.Read())
                    {
                        oPlanAccion = new Negocio.PlanAccion()
                        {
                            oDatosRiesgo = new Negocio.DatosRiesgos()
                            {
                                nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                                cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                                nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoRiesgo")]),
                                cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]),
                                cEstadoRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstadoActual")]),
                                dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDetec")]))
                            },
                            oReponsablePlanAccion = new Negocio.ResponsablePlanAccion()
                            {
                                cUserResponsable = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cResponsables")]),
                                cConfrimados = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cConfirmados")]),
                                cUserConfrimados = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUserConfirmados")])
                            },
                            nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]),
                            cPlanDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPlanDescripcion")]),
                            nEstado = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]),
                            cEstado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")]),
                            cIconoEstado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cIconoEstado")]),
                            dFechaImplement = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaImplementa")]))
                        };
                        oLstPlanAccion.Add(oPlanAccion);
                    }
                    return oLstPlanAccion;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int RegistrarPlanAccion(long pnNroRiesgo, string psDescPlanAccion, DateTime pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "")
        {
            try
            {
                SqlParameter[] parameter = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cDescPlanAccion", psDescPlanAccion),
                    new SqlParameter("@dFechaImplement", pdFechImplement),
                    new SqlParameter("@bSugeridoGM", pnSugerenciaGM),
                    new SqlParameter("@cComentarios", psComentarios),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cNombreDoc", psNombreDoc),
                    new SqlParameter("@cNombreDocDB", psNombreDocBD)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_GrabarPlanAccionRiesgoOperacional, CommandType.StoredProcedure, parameter);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int ActualizarPlanAccion(long pnCodPlan, string psDescPlanAccion, DateTime pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "")
        {
            try
            {
                SqlParameter[] parameters = {
                    //new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nPlanCod", pnCodPlan),
                    new SqlParameter("@cDescPlanAccion", psDescPlanAccion),
                    new SqlParameter("@dFechaImplement", pdFechImplement),
                    new SqlParameter("@bSugeridoGM", pnSugerenciaGM),
                    new SqlParameter("@cComentarios", psComentarios),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cNombreDoc", psNombreDoc),
                    new SqlParameter("@cNombreDocDB", psNombreDocBD)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_GrabarActualizacionPlanAccion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RegistrarResponsablePlanAccion(long pnPlanCod, string psUserResponsable, string psUltimaActualizacion)
        {
            try
            {

                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnPlanCod),
                    new SqlParameter("@cUserResponsable", psUserResponsable),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarUsuarioResponPlanAccion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int QuitarResponsablePlanAccion(long pnPlanCod, int pnItemResponsable, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnPlanCod),
                    new SqlParameter("@ItemRespon", pnItemResponsable),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_QuitarUsuarioResponPlanAccion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RegistrarResponsablePlanAccionReasignado(long pnPlanCod, string psUserResponsable, string psUserReasignado, string psReasignacion)
        {
            try
            {

                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnPlanCod),
                    new SqlParameter("@cUserResponsable", psUserResponsable),
                    new SqlParameter("@cUserReasignado", psUserReasignado),
                    new SqlParameter("@cReasignacion", psReasignacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarUsuarioReasignadoPlanAccion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int EliminarPlanAccion(long pnNroRiesgo, long pnPlanCod, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@nPlanCod", pnPlanCod),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_EliminarPlanAccion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public List<Negocio.ResponsablePlanAccion> ObtenerResponsablePlanAccion(long pnPlanCod)
        {
            try
            {
                SqlParameter sqlPlanCod = new SqlParameter("@nPlanCod", pnPlanCod);

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerResponsablesPlanesAccion, CommandType.StoredProcedure, sqlPlanCod))
                {
                    List<Negocio.ResponsablePlanAccion> LstResponsable = new List<Negocio.ResponsablePlanAccion>();
                    Negocio.ResponsablePlanAccion oResponsable;
                    while (reader.Read())
                    {
                        oResponsable = new Negocio.ResponsablePlanAccion();
                        oResponsable.oPlanAccion = new Negocio.PlanAccion();
                        oResponsable.oPlanAccion.nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]);
                        oResponsable.nItemResponPlan = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nItem")]);
                        oResponsable.cUserResponsable = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]);
                        LstResponsable.Add(oResponsable);
                    }
                    return LstResponsable;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public string ValidarResponsablesPlanAccion(long pnNroRiesgo)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesgo),
                    new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_ValidarResponsablesPlanAccion, CommandType.StoredProcedure, parameters);

                return (string)parameters[1].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public string[] ActualizarEstadoPlanAccion(long pnCodPlanAccion, int pnEstadoPlan, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nCodPlan", pnCodPlanAccion),
                    new SqlParameter("@nEstado", pnEstadoPlan),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cFinalizar", SqlDbType.VarChar, 1500){ Direction = ParameterDirection.Output}
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_ActualizaEstadoPlanAccion, CommandType.StoredProcedure, parameters);

                string[] nValRespuesta = parameters[3].Value.ToString().Split('|');

                return nValRespuesta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string[] AgregarFechasImplementacion(long pnCodPlanAccion, string pdFechaImplementacion, bool pbImplementado, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnCodPlanAccion),
                    new SqlParameter("@dFechaImplementa", Convert.ToDateTime(pdFechaImplementacion).ToString("yyyy/MM/dd")),
                    new SqlParameter("@bImplementado", pbImplementado),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cMensajeSis", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_AgregarFechaImplementacionPlanAccion, CommandType.StoredProcedure, parameters);
                string[] respuesta = parameters[4].Value.ToString().Split(',');
                return respuesta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int ActualizaFechasImplementacionPlanAccion(long pnCodPlanAccion)
        {
            try
            {
                SqlParameter[] parameters = {
                new SqlParameter("@nPlanCod", pnCodPlanAccion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_ActualizaFechasImplementacionPlanAccion, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ObtenerFechasImplementacionPlanAccion(long pnPlanCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnPlanCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListaFechasImplementacionPlanAccion, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstFechas = new List<dynamic>();
                    dynamic Fechas = null;
                    while (reader.Read())
                    {
                        Fechas = new System.Dynamic.ExpandoObject();
                        Fechas.CodPlan = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]);
                        Fechas.FechaImplementa = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaImplementa")]).ToString("dd/MM/yyyy");
                        Fechas.ValorEstado = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]);
                        Fechas.Estado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")]);
                        LstFechas.Add(Fechas);
                    }
                    return LstFechas;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int GuardarComentariosPlanAccion(long pnCodPlan, string psUsuario, string psComentario)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnCodPlan),
                    new SqlParameter("@cUser", psUsuario),
                    new SqlParameter("@cComentario", psComentario)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_ComentarioPlanAccion, CommandType.StoredProcedure, parameters);
                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<dynamic> ObtenerComenariosPlanAccion(long pnPlanCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nPlanCod", pnPlanCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ComentariosPlanAccion, CommandType.StoredProcedure, parameters))
                {
                    List<dynamic> LstComentarios = new List<dynamic>();
                    dynamic Comentario = null;
                    while (reader.Read())
                    {
                        Comentario = new System.Dynamic.ExpandoObject();

                        Comentario.nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]);
                        Comentario.nItem = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nItem")]);
                        Comentario.cUsuario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]);
                        Comentario.cComentario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cComentario")]);
                        Comentario.dFecha = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFecha")]));
                        Comentario.cFotoUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cFotoUser")]);
                        LstComentarios.Add(Comentario);
                    }
                    return LstComentarios;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.PlanAccion> ObtenerHisotialEstadoPlanAccion(long pnPlanCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nCodPlan", pnPlanCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerHistorialEstadosPlanAccion, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.PlanAccion> LstEstado = new List<Negocio.PlanAccion>();
                    Negocio.PlanAccion planAccion = null;
                    while (reader.Read())
                    {
                        planAccion = new Negocio.PlanAccion()
                        {
                            nPlanCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPlanCod")]),
                            nEstado = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]),
                            dFechaUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaHoraActualiza")])),
                        };
                        LstEstado.Add(planAccion);
                    }
                    return LstEstado;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



    }
}
