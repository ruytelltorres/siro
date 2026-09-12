using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class ProcesoArea : IProcesoArea
    {
        
        /// <summary>
        /// Obtiene información de los procesos de las areas
        /// </summary>
        /// <param name="psAreaCod">Código del área</param>
        /// <returns></returns>
        public List<Negocio.ProcesoArea> ObtenerProcesoAreas(string psAreaCod)
        {
            try
            {
                SqlParameter sqlAreaCod = new SqlParameter("@cAreaCod", psAreaCod);

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarProcesoxArea, CommandType.StoredProcedure, sqlAreaCod))
                {
                    List<Negocio.ProcesoArea> oListaProcesos = new List<Negocio.ProcesoArea>();
                    Negocio.ProcesoArea oProcesoAreas;

                    while (reader.Read())
                    {
                        oProcesoAreas = new Negocio.ProcesoArea()
                        {
                            cCodProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodProceso")]),
                            cDescProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProceso")])
                        };
                        oListaProcesos.Add(oProcesoAreas);
                    }
                    return oListaProcesos;
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Negocio.ProcesoArea> MostrarProcesosAreas(string psCodArea = "")
        {
            try
            {
                SqlParameter pcAreaCod = new SqlParameter("@cAreaCod", psCodArea);

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_MostrarProcesoxArea, CommandType.StoredProcedure, pcAreaCod))
                {
                    List<Negocio.ProcesoArea> ListaProcesos = new List<Negocio.ProcesoArea>();
                    Negocio.ProcesoArea procesos;

                    while (reader.Read())
                    {
                        procesos = new Negocio.ProcesoArea()
                        {
                            oAreas = new Negocio.Areas()
                            {
                                cAreaCod = Convert.ToString(reader[reader.GetOrdinal("cAreaCod")]),
                                cAreaDescripcion = Convert.ToString(reader[reader.GetOrdinal("cAreaDescResumen")])
                            },
                            cCodProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodProceso")]),
                            cDescProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProceso")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                            bProcesoEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstadoProceso")])

                        };
                        ListaProcesos.Add(procesos);
                    }
                    return ListaProcesos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RegistrarGestionProcesoArea(string psAreaCod, string psUltimaActualizacion, string psNombreProceso = "", int pnProceso = 0)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cAreaCod", psAreaCod),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cProcesDesc", psNombreProceso),
                    new SqlParameter("@Proceso", pnProceso),
                    new SqlParameter("@Exito", SqlDbType.Int, 1){ Direction = ParameterDirection.Output}
                };

                var exec = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_upd_GestionProcesoArea, CommandType.StoredProcedure, parameters);

                return (int)parameters[4].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }










    }
}
