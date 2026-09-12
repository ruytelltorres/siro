using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class SubLineaNegocio : ISubLineaNegocio
    {
        
        public List<Negocio.SubLineaNegocio> ObtenerSubLineaNegocio(string psCodLineaNeg)
        {
            try
            {
                SqlParameter sqlCodLineNeg = new SqlParameter("@cCodLineaNeg", psCodLineaNeg);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarSubLineNegocio, CommandType.StoredProcedure, sqlCodLineNeg))
                {
                    List<Negocio.SubLineaNegocio> olListaSubLineaNego = new List<Negocio.SubLineaNegocio>();
                    Negocio.SubLineaNegocio oSubLineaNeg;
                    while (reader.Read())
                    {
                        oSubLineaNeg = new Negocio.SubLineaNegocio()
                        {
                            oLineaNeg = new Negocio.LineaNegocio()
                            {
                                cCodLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodLineaNeg")])
                            },
                            cCodSubLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodSubLineaNeg")]),
                            cDescSubLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubLineaNeg")]),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                            dUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dUltimaActualizacion")]))

                        };

                        olListaSubLineaNego.Add(oSubLineaNeg);
                    }
                    return olListaSubLineaNego;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
