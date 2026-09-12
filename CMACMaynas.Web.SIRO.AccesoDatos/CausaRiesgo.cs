using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class CausaRiesgo : ICausaRiesgo
    {
        public List<Negocio.CausaRiesgo> MostrarCausasRiesgo()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCausasRiesgo, CommandType.StoredProcedure))
                {
                    List<Negocio.CausaRiesgo> lstCausas = new List<Negocio.CausaRiesgo>();
                    Negocio.CausaRiesgo oCausas;

                    while (reader.Read())
                    {
                        oCausas = new Negocio.CausaRiesgo()
                        {
                            cCodCausa = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodCausa")]),
                            cCausaDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCausaDesc")]),
                            dFechaReg = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                            dFechaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaActualizacion")])),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")])
                        };
                        lstCausas.Add(oCausas);
                    }
                    return lstCausas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<Negocio.CausaRiesgo> ObtenerCausasRiesgo()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarCausaRiesgo, CommandType.StoredProcedure))
                {
                    List<Negocio.CausaRiesgo> lstCausas = new List<Negocio.CausaRiesgo>();
                    Negocio.CausaRiesgo oCausas;

                    while (reader.Read())
                    {
                        oCausas = new Negocio.CausaRiesgo()
                        {
                            cCodCausa = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodCausa")]),
                            cCausaDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCausaDesc")])
                        };
                        lstCausas.Add(oCausas);
                    }
                    return lstCausas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.CausaRiesgo> ObtenerCausaPorRiesgo(long pnNroRiesgo)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesgo);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCausasPorRiesgo, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    List<Negocio.CausaRiesgo> lstCausas = new List<Negocio.CausaRiesgo>();
                    Negocio.CausaRiesgo oCausas;

                    while (reader.Read())
                    {
                        oCausas = new Negocio.CausaRiesgo()
                        {
                            oDatosRiesgo = new Negocio.DatosRiesgos()
                            {
                                nNroRiesgo = (long)Convert.ToInt32(reader[reader.GetOrdinal("nNroRiesgo")])
                            },
                            cCodCausa = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodCausa")]),
                            nItem = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nItem")]),
                            cCausaDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCausaDesc")])
                        };
                        lstCausas.Add(oCausas);
                    }
                    return lstCausas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RegistrarGestionCausaRiesgo(string psCodCausa, string psCausaDesc, string psUltimaActualizacion, int pnAccion = 0)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodCausa", psCodCausa),
                    new SqlParameter("@cCausaDesc", psCausaDesc),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@Accion", pnAccion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_upd_GestionCausasRiesgo, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
