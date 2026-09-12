using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class RiesgoInherente : IRiesgoInherente
    {
        public Negocio.RiesgoInherente ObtenerNivelRiesgoInherente(int pnProbalidad, int pnImpacto)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nProbabilidad", pnProbalidad),
                    new SqlParameter("@nImpacto", pnImpacto)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerNivelRiesgoProbabilidadImpacto, CommandType.StoredProcedure, parameters))
                {
                    Negocio.RiesgoInherente oRiesgoInherente = null;
                    while (reader.Read())
                    {
                        oRiesgoInherente = new Negocio.RiesgoInherente();
                        oRiesgoInherente.nValorEscala = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNivelRiesgo")]);
                    }
                    return oRiesgoInherente;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


    }
}
