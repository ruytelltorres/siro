using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Areas : IAreas
    {
        public List<Negocio.Areas> ObtenerAreasAgencia(string psAgeCod)
        {
            try
            {
                SqlParameter sqlAgeCod = new SqlParameter("@cAgeCod", psAgeCod);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarAreasAgencias, CommandType.StoredProcedure, sqlAgeCod))
                {
                    List<Negocio.Areas> olListaAreas = new List<Negocio.Areas>();
                    Negocio.Areas oAreas;
                    while (reader.Read())
                    {
                        oAreas = new Negocio.Areas();
                        oAreas.cAreaCod = Convert.ToString(reader[reader.GetOrdinal("cAreaCod")]);
                        oAreas.cAreaDescripcion = Convert.ToString(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        olListaAreas.Add(oAreas);
                    }
                    return olListaAreas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.Areas> ObtenerAreas()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarAreas, CommandType.StoredProcedure))
                {
                    List<Negocio.Areas> olListaAreas = new List<Negocio.Areas>();
                    Negocio.Areas oAreas;
                    while (reader.Read())
                    {
                        oAreas = new Negocio.Areas();
                        oAreas.cAreaCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaCod")]);
                        oAreas.cAreaDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAreaDescripcion")]);
                        olListaAreas.Add(oAreas);
                    }
                    return olListaAreas;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        

    }
}
