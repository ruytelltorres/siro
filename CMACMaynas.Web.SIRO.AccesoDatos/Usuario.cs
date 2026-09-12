using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Usuario : IUsuario
    {

        public Negocio.Usuario ObtenerDatosUsuario(string psUser)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cUser", psUser)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDatosUsuario, CommandType.StoredProcedure, parameters))
                {
                    Negocio.Usuario usuario = null;
                    while (reader.Read())
                    {
                        usuario = new Negocio.Usuario();
                        usuario.oPersona = new Negocio.Persona();
                        usuario.oPerfil = new Negocio.Perfil();
                        usuario.oAreas = new Negocio.Areas();
                        usuario.oAgencia = new Negocio.Agencias();
                        usuario.cUser = Convert.ToString(reader[reader.GetOrdinal("cUser")]);
                        usuario.oPerfil.cUrlImagen = Convert.ToString(reader[reader.GetOrdinal("cImagenUrl")]);
                        usuario.oPersona.cPersCod = Convert.ToString(reader[reader.GetOrdinal("cPersCod")]);
                        usuario.oPersona.cPersNombre = Convert.ToString(reader[reader.GetOrdinal("cPersNombre")]);
                        usuario.cRHCargoCod = Convert.ToString(reader[reader.GetOrdinal("cRHCargoCod")]);
                        usuario.cRHCargoDescripcion = Convert.ToString(reader[reader.GetOrdinal("cRHCargoDescripcion")]);
                        usuario.oAreas.cAreaCod = Convert.ToString(reader[reader.GetOrdinal("cAreaCod")]);
                        usuario.oAreas.cAreaDescripcion = Convert.ToString(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        usuario.oAgencia.cAgeCod = Convert.ToString(reader[reader.GetOrdinal("cAgenciaActual")]);
                        usuario.oAgencia.cAgeDescripcion = Convert.ToString(reader[reader.GetOrdinal("cAgeDescripcion")]);
                    }
                    return usuario;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Usuario> ObtenerUsuarioAgenciaArea(string psAgeCod, string psAreaCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cAgeCod", psAgeCod),
                    new SqlParameter("@cAreaCod", psAreaCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerUsuarioAgenciasAreas, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Usuario> oListaUsuarios = new List<Negocio.Usuario>();
                    Negocio.Usuario oUsuario;
                    while (reader.Read())
                    {
                        oUsuario = new Negocio.Usuario();
                        oUsuario.oPersona = new Negocio.Persona();
                        oUsuario.oAreas = new Negocio.Areas();
                        oUsuario.oAgencia = new Negocio.Agencias();

                        oUsuario.cUser = Convert.ToString(reader[reader.GetOrdinal("cUser")]);
                        oUsuario.cUsuario = Convert.ToString(reader[reader.GetOrdinal("cUsuario")]);
                        oUsuario.oPersona.cPersCod = Convert.ToString(reader[reader.GetOrdinal("cPersCod")]);
                        oUsuario.oPersona.cPersNombre = Convert.ToString(reader[reader.GetOrdinal("cPersNombre")]);
                        oUsuario.cRHCargoCod = Convert.ToString(reader[reader.GetOrdinal("cRHCargoCod")]);
                        oUsuario.cRHCargoDescripcion = Convert.ToString(reader[reader.GetOrdinal("cRHCargoDescripcion")]);
                        oUsuario.oAreas.cAreaCod = Convert.ToString(reader[reader.GetOrdinal("cAreaCod")]);
                        oUsuario.oAreas.cAreaDescripcion = Convert.ToString(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        oUsuario.oAgencia.cAgeCod = Convert.ToString(reader[reader.GetOrdinal("cAgenciaActual")]);
                        oUsuario.oAgencia.cAgeDescripcion = Convert.ToString(reader[reader.GetOrdinal("cAgeDescripcion")]);
                        oListaUsuarios.Add(oUsuario);
                    }
                    return oListaUsuarios;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Usuario> ObtenerUsuarioArea(string psAreaCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cAreaCod", psAreaCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerUsuarioAreas, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Usuario> oListaUsuarios = new List<Negocio.Usuario>();
                    Negocio.Usuario oUsuario;
                    while (reader.Read())
                    {
                        oUsuario = new Negocio.Usuario()
                        {
                            oPersona = new Negocio.Persona()
                            {
                                cPersCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPersCod")]),
                                cPersNombre = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cPersNombre")])
                            },
                            oAreas = new Negocio.Areas()
                            {
                                cAreaCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaCod")]),
                                cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")])
                            },
                            oAgencia = new Negocio.Agencias()
                            {
                                cAgeCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgenciaActual")]),
                                cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")])
                            },
                            cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]),
                            cUsuario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUsuario")]),

                            cRHCargoCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRHCargoCod")]),
                            cRHCargoDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRHCargoDescripcion")])
                        };
                        oListaUsuarios.Add(oUsuario);
                    }
                    return oListaUsuarios;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Usuario> ObtenerUsuariosCargos(string psCargoCod)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCargoCod", psCargoCod)
                };

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerUsuariosCargos, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Usuario> oListaUsuarios = new List<Negocio.Usuario>();
                    Negocio.Usuario oUsuario;
                    while (reader.Read())
                    {
                        oUsuario = new Negocio.Usuario()
                        {
                            oPersona = new Negocio.Persona()
                            {
                                cPersNombre = Convert.ToString(reader[reader.GetOrdinal("cPersNombre")]),
                            },
                            cUser = Convert.ToString(reader[reader.GetOrdinal("cUser")]),
                            cRHCargoCod = Convert.ToString(reader[reader.GetOrdinal("cRHCargoCod")])
                        };
                        oListaUsuarios.Add(oUsuario);
                    }
                    return oListaUsuarios;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool GuardarDatosPerfil(string psUser, string psUrlImagen, int pnProceso = 1)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cUser", psUser),
                    new SqlParameter("@cImagenUrl", psUrlImagen),
                    new SqlParameter("@Proceso", pnProceso)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_ActualizarDatosPerfilSiro, CommandType.StoredProcedure, parameters);
                if (exito > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string ObtenerMailUsuario(string cUsuario) {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cUsuario", cUsuario)
                };

                string usuario = Convert.ToString(SqlHelper.ExecuteScalar(ConectarBD.Get.ConnectionString(), SP.spt_sel_obtener_email_usuario, CommandType.StoredProcedure, parameters));
                return usuario;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
