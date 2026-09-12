using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Menu : IMenu
    {

        public List<Negocio.Menu> ObtenerMenuUsuario(string psGrupoUser, string psCargoUser = "")
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cGrupoUser", psGrupoUser),
                    new SqlParameter("@cRHCargoUser", psCargoUser),
                };


                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerMenuUser, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Menu> ListaMenu = new List<Negocio.Menu>();
                    Negocio.Menu oMenu;
                    while (reader.Read())
                    {
                        oMenu = new Negocio.Menu()
                        {
                            cMenuId = Convert.ToString(reader[reader.GetOrdinal("cMenuId")]),
                            cMenuPadre = Convert.ToString(reader[reader.GetOrdinal("cMenuPadre")]),
                            cTitulo = Convert.ToString(reader[reader.GetOrdinal("cTitulo")]),
                            cDescripcion = Convert.ToString(reader[reader.GetOrdinal("cDescripcion")]),
                            cUrl = Convert.ToString(reader[reader.GetOrdinal("cUrl")]),
                            cIcono = Convert.ToString(reader[reader.GetOrdinal("cIcono")]),
                            nPosicion = Convert.ToInt32(reader[reader.GetOrdinal("nPosicion")]),
                            bEstado = Convert.ToBoolean(reader[reader.GetOrdinal("bEstado")]),
                            cDescUrl = Convert.ToString(reader[reader.GetOrdinal("cUrlTitulo")])
                        };


                        ListaMenu.Add(oMenu);
                    }
                    return ListaMenu;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //Added TORE
        //public List<Negocio.Menu> ObtenerMenuSistema(int pnTipoMenu, string psMenuP = "")
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = {
        //            new SqlParameter("@nTipoMenu", pnTipoMenu),
        //            new SqlParameter("@cMenuPadre", psMenuP)
        //        };

        //        using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerMenuSistema, CommandType.StoredProcedure, parameters))
        //        {
        //            List<Negocio.Menu> ListaMenusistema = new List<Negocio.Menu>();
        //            Negocio.Menu oMenu;
        //            while (reader.Read())
        //            {
        //                oMenu = new Negocio.Menu()
        //                {
        //                    cMenuId = Convert.ToString(reader[reader.GetOrdinal("cMenuId")]),
        //                    cMenuPadre = Convert.ToString(reader[reader.GetOrdinal("cMenuPadre")]),
        //                    cTitulo = Convert.ToString(reader[reader.GetOrdinal("cTitulo")]),
        //                    cIcono = Convert.ToString(reader[reader.GetOrdinal("cIcono")])
        //                };

        //                ListaMenusistema.Add(oMenu);
        //            }
        //            return ListaMenusistema;
        //        }
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        public List<Negocio.Menu> ObtenerMenuSistema()
        {
            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerMenuSistema, CommandType.StoredProcedure))
                {
                    List<Negocio.Menu> ListaMenusistema = new List<Negocio.Menu>();
                    Negocio.Menu oMenu;
                    while (reader.Read())
                    {
                        oMenu = new Negocio.Menu()
                        {
                            cMenuId = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cMenuId")]),
                            cMenuPadre = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cMenuPadre")]),
                            cTitulo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cTitulo")]),
                            cIcono = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cIcono")]),
                            nPosicion = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nPosicion")]),
                            nNivel = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNivel")])
                        };

                        ListaMenusistema.Add(oMenu);
                    }
                    return ListaMenusistema;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string ActivarDesactivarOpcionMenu(string psGrupoUsu, string psMenuId, int Opcion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cGrupoUser", psGrupoUsu),
                    new SqlParameter("@cMenuId", psMenuId),
                    new SqlParameter("@nActivar", Opcion),
                    new SqlParameter("@cMensajeActivacion", SqlDbType.VarChar, 150) { Direction = ParameterDirection.Output }
                };

                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_upd_ActivarDesactivarOpcionMenu, CommandType.StoredProcedure, parameters);

                return (string)parameters[3].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Menu> ObtenerCargosMenu(string cMenuId, string cUsuario, int Proceso)
        {
            try
            {
                /*Proceso: 1 -> Consulta, 2 -> Asignar*/
                SqlParameter sqlMenuID = new SqlParameter("@cMenuId", cMenuId);
                SqlParameter sqlUsuario = new SqlParameter("@cUser", cUsuario);
                SqlParameter sqlProceso = new SqlParameter("@nProceso", Proceso);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerPermisoCargoMenu, CommandType.StoredProcedure, sqlMenuID, sqlUsuario, sqlProceso))
                {
                    List<Negocio.Menu> ListaMenusistema = new List<Negocio.Menu>();
                    Negocio.Menu oMenu;
                    while (reader.Read())
                    {
                        oMenu = new Negocio.Menu();
                        oMenu.oUsuario = new Negocio.Usuario();

                        oMenu.cMenuId = Convert.ToString(reader[reader.GetOrdinal("cMenuId")]);
                        oMenu.cTitulo = Convert.ToString(reader[reader.GetOrdinal("cTitulo")]);
                        oMenu.oUsuario.cRHCargoCod = Convert.ToString(reader[reader.GetOrdinal("cCargoCod")]);
                        oMenu.oUsuario.cRHCargoDescripcion = Convert.ToString(reader[reader.GetOrdinal("cNombreCargo")]);
                        oMenu.bEstado = Convert.ToBoolean(reader[reader.GetOrdinal("bEstado")]);

                        ListaMenusistema.Add(oMenu);
                    }
                    return ListaMenusistema;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }


    }
}












