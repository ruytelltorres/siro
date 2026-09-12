using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class LineaNegocio : ILineaNegocio
    {

        public List<Negocio.LineaNegocio> ObtenerLineaNegocio()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarLineasNegocio, CommandType.StoredProcedure))
                {
                    List<Negocio.LineaNegocio> olListaLineaNego = new List<Negocio.LineaNegocio>();
                    Negocio.LineaNegocio oLineaNeg;
                    while (reader.Read())
                    {
                        oLineaNeg = new Negocio.LineaNegocio()
                        {
                            cCodLineaNeg = Convert.ToString(reader[reader.GetOrdinal("cCodLineaNeg")]),
                            cDescLineaNeg = Convert.ToString(reader[reader.GetOrdinal("cDescLineaNeg")]),
                            //bEstado = Convert.ToBoolean(reader[reader.GetOrdinal("bEstado")]),
                            //dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")]),
                            //dUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaActualizacion")])
                        };
                        olListaLineaNego.Add(oLineaNeg);
                    }
                    return olListaLineaNego;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public List<Negocio.LineaNegocio> MostrarLineaNegocio()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarLineasNegocio, CommandType.StoredProcedure))
                {
                    List<Negocio.LineaNegocio> olListaLineaNego = new List<Negocio.LineaNegocio>();
                    Negocio.LineaNegocio oLineaNeg;
                    while (reader.Read())
                    {
                        oLineaNeg = new Negocio.LineaNegocio()
                        {
                            cCodLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodLineaNeg")]),
                            cDescLineaNeg = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescLineaNeg")]),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                            dUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaActualizacion")])),

                        };

                        olListaLineaNego.Add(oLineaNeg);
                    }
                    return olListaLineaNego;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
