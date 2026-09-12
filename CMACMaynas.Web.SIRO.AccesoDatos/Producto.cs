using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Producto : IProducto
    {
        
        public List<Negocio.Producto> ObtenerProducto(string psCodLineaNeg)
        {
            try
            {
                SqlParameter sqlCodLineNeg = new SqlParameter("@cCodLineaNeg", psCodLineaNeg);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerProductoLineaNegocio, CommandType.StoredProcedure, sqlCodLineNeg))
                {
                    List<Negocio.Producto> oLstProducto = new List<Negocio.Producto>();
                    Negocio.Producto oProducto;
                    while (reader.Read())
                    {
                        oProducto = new Negocio.Producto()
                        {
                            cCodProducto = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodProducto")]),
                            cDescProducto = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProducto")])
                        };
                        oLstProducto.Add(oProducto);
                    }
                    return oLstProducto;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


    }
}
