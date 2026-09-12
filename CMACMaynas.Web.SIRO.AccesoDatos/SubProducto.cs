using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class SubProducto : ISubProducto
    {
        
        public List<Negocio.SubProducto> ObtenerSubProducto(string psCodProducto)
        {
            try
            {
                SqlParameter sqlCodProducto = new SqlParameter("@cCodProducto", psCodProducto);

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerSubProducto, CommandType.StoredProcedure, sqlCodProducto))
                {
                    List<Negocio.SubProducto> olListaSubProducto = new List<Negocio.SubProducto>();
                    Negocio.SubProducto oSubProducto;
                    while (reader.Read())
                    {
                        oSubProducto = new Negocio.SubProducto()
                        {
                            cCodSubProducto = Convert.ToString(reader[reader.GetOrdinal("cCodSubProducto")]),
                            cDescSubProducto = Convert.ToString(reader[reader.GetOrdinal("cDescSubProducto")])
                        };

                        olListaSubProducto.Add(oSubProducto);
                    }
                    return olListaSubProducto;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
