using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class ConstSistema: IConstSistema
    {
        
        public Negocio.ConstSistema ObtenerConstanteSistema(int pnConstSisCod)
        {
            try
            {
                SqlParameter sqlConsSisCod = new SqlParameter("@nConstSisCod", pnConstSisCod);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerConstSistema, CommandType.StoredProcedure, sqlConsSisCod))
                {
                    Negocio.ConstSistema oConstSistema = null;
                    while (reader.Read())
                    {
                        oConstSistema = new Negocio.ConstSistema()
                        {
                            nConsSisCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nConsSisCod")]),
                            cConsSisDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cConsSisDesc")]),
                            cConsSisValor = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cConsSisValor")])
                        };
                    }
                    return oConstSistema;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


    }
}
