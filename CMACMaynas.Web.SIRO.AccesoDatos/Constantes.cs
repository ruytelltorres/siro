using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Constantes : IConstantes
    {
        public List<Negocio.Constante> ObtenerConstantes(int psCodConstante)
        {
            try
            {
                SqlParameter[] parameter = {
                     new SqlParameter("@nConsCod", psCodConstante)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerConstante, CommandType.StoredProcedure, parameter))
                {
                    List<Negocio.Constante> lstConstante = new List<Negocio.Constante>();
                    Negocio.Constante oConstante;

                    while (reader.Read())
                    {
                        oConstante = new Negocio.Constante()
                        {
                            nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nConsValor")]),
                            cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cConsDescripcion")])
                        };
                        lstConstante.Add(oConstante);
                    }
                    return lstConstante;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        /// <summary>
        /// Obtene la descripcion del valor de la constante filtrado
        /// </summary>
        /// <param name="pnConsCod"></param>
        /// <param name="pnConstValor"></param>
        /// <returns></returns>
        public Negocio.Constante ObtenerDescConstante(int pnConsCod, int pnConstValor)
        {
            try
            {
                SqlParameter[] parameter = {
                   new SqlParameter("@nConsCod", pnConsCod),
                   new SqlParameter("@nConsValor", pnConstValor)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDescripcionConstante, CommandType.StoredProcedure, parameter))
                {
                    Negocio.Constante oConstante = null;
                    while (reader.Read())
                    {
                        oConstante = new Negocio.Constante()
                        {
                            nConsCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nConsCod")]),
                            nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nConsValor")]),
                            cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cConsDescripcion")])
                        };
                    }
                    return oConstante;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}
