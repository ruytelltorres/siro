using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class MontoPerdida : IMontoPerdida
    {

        public List<Negocio.MontoPerdida> ObtenerMontoPerdida()
        {
            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerMontoPerdida, CommandType.StoredProcedure))
                {
                    List<Negocio.MontoPerdida> oLstMontoPerdida = new List<Negocio.MontoPerdida>();
                    Negocio.MontoPerdida oMontoPerdida = null;
                    while (reader.Read())
                    {
                        oMontoPerdida = new Negocio.MontoPerdida()
                        {
                            nMontPerdCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nMonPerdCod")]),
                            nProbabilidad = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProbabilidad")]),
                            nImpacto = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nImpacto")]),
                            nMontoPerdida = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nMontoPerdida")]),
                            cComentario = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cComentario")]),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaReg")])),
                            dFechaCese = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("cFechaCese")]))
                        };
                        oLstMontoPerdida.Add(oMontoPerdida);
                    }
                    return oLstMontoPerdida;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int GrabaMontoPerdida(int pnProbabilidad, int pnImpacto, decimal pnMontoPerdida, string psComentarios, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameter = {
                    new SqlParameter("@nProbabilidad", pnProbabilidad),
                    new SqlParameter("@nImpacto", pnImpacto),
                    new SqlParameter("@nMontoPerdida", pnMontoPerdida),
                    new SqlParameter("@cComentarios", psComentarios),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };
                return SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabaMontoPerdida, CommandType.StoredProcedure, parameter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
