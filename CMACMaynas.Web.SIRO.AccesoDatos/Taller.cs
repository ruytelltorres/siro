using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Taller : ITaller
    {

        public string ObtenerTaller(string psAreaCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@cCodTaller", SqlDbType.VarChar, 25) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCodTaller, CommandType.StoredProcedure, parameters);
                return (string)parameters[1].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Usuario> ObtenerUsuarioCodTpoEvaluacion(string psCodTpoEval)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodTpoEval", psCodTpoEval)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerUsuariosTpoEvaluacion, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Usuario> oLstUsuarios = new List<Negocio.Usuario>();
                    Negocio.Usuario oUsuarios;
                    while (reader.Read())
                    {
                        oUsuarios = new Negocio.Usuario()
                        {
                            cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]),
                            cUsuario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUsuario")])
                        };
                        oLstUsuarios.Add(oUsuarios);
                    }
                    return oLstUsuarios;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //public List<Negocio.Taller> ObtenerTallerUsuario(string psCodTpoEval, string psUsuario, string psEstados = "")
        public List<Negocio.Taller> ObtenerTallerUsuario(string psCodTpoEval)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodTpoEval", psCodTpoEval),
                    //new SqlParameter("@cUser", psUsuario),
                    //new SqlParameter("@cEstado", psEstados)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerTallerUsuario, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Taller> oLstTalleres = new List<Negocio.Taller>();
                    Negocio.Taller oTaller;
                    while (reader.Read())
                    {
                        oTaller = new Negocio.Taller()
                        {
                            cCodTaller = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodTaller")]),
                        };
                        oLstTalleres.Add(oTaller);
                    }
                    return oLstTalleres;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.TallerRiesgo> ObtenerRiesgosTaller(string psCodTaller)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodTaller", psCodTaller)
                };
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerRiesgosTaller, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.TallerRiesgo> oLstRiesgosTaller = new List<Negocio.TallerRiesgo>();
                    Negocio.TallerRiesgo oTaller;
                    while (reader.Read())
                    {
                        oTaller = new Negocio.TallerRiesgo()
                        {
                            oAutoevaluacion = new Negocio.Autoevaluacion()
                            {
                                oTipoEvaluacion = new Constante()
                                {
                                    cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEvalDesc")])
                                },
                                nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]),
                                cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]),
                                dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")]))
                            },
                            oTaller = new Negocio.Taller()
                            {
                                cCodTaller = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodTaller")])
                            },

                        };

                        oLstRiesgosTaller.Add(oTaller);
                    }
                    return oLstRiesgosTaller;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.Taller ObtenerDatosTaller(string psCodTaller)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodTaller", psCodTaller)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDatosTaller, CommandType.StoredProcedure, parameters))
                {
                    Negocio.Taller oTaller = null;
                    while (reader.Read())
                    {
                        oTaller = new Negocio.Taller() {

                            cCodTaller = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodTaller")]),
                            cCodTpoEval = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodTpoEval")]),
                            oEstado = new Negocio.Constante()
                            {
                                nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]),
                                cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cEstado")])
                            },
                            cUltimaActualizacion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUltimaActualizacion")])
                        };
                        oTaller.dFechaCreacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaCreacion")]));
                    }
                    return oTaller;
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //public string AsignarRiesgosTaller(string psNroRiesgos, string psCodTaller, string psUltimaActualizacion)
        public string[] AsignarRiesgosTaller(string psNroRiesgos, string psCodTaller, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodRiesgos", psNroRiesgos),
                    new SqlParameter("@cCodTaller", psCodTaller),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cMensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output }};


                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_AsignarRiesgosTaller, CommandType.StoredProcedure, parameters);

                return (string[])parameters[3].Value.ToString().Split('-');
                //return (string)parameters[3].Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
