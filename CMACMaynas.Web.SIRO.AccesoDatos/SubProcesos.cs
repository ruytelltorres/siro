using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class SubProcesos : ISubProceso
    {
        
        public List<Negocio.SubProcesos> ObtenerSubProcesosAreas(string psCodProceso = "")
        {
            try
            {
                SqlParameter sqlCodProceso = new SqlParameter("@cCodProceso", psCodProceso);

                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarSubProcesoAreas, CommandType.StoredProcedure, sqlCodProceso))
                {
                    List<Negocio.SubProcesos> olListaSubProcesos = new List<Negocio.SubProcesos>();
                    Negocio.SubProcesos oSubProcesos;
                    while (reader.Read())
                    {
                        oSubProcesos = new Negocio.SubProcesos()
                        {
                            nCodSubProceso = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodSubProceso")]),
                            cDescSubProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubProceso")])
                        };

                        olListaSubProcesos.Add(oSubProcesos);
                    }
                    return olListaSubProcesos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Negocio.SubProcesos> MostarSubprocesosAreas(string psCodProceso)
        {
            try
            {
                SqlParameter sqlCodProceso = new SqlParameter("@cCodProceso", psCodProceso);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_MostrarSubProcesoAreas, CommandType.StoredProcedure, sqlCodProceso))
                {
                    List<Negocio.SubProcesos> ListaSubprocesos = new List<Negocio.SubProcesos>();
                    Negocio.SubProcesos subprocesos;
                    while (reader.Read())
                    {
                        subprocesos = new Negocio.SubProcesos()
                        {
                            nCodSubProceso = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodSubProceso")]),
                            cDescSubProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescSubProceso")]),
                            cAbreviatura = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAbreviatura")]),
                            dUltimaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")]))
                        };

                        ListaSubprocesos.Add(subprocesos);
                    }
                    return ListaSubprocesos;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int RegistrarGestionSubProcesosAreas(string psCodProceso, string psDescSubProceso, string psAbreviatura, string psUltimaActualizacion, int pnIdSubProceso = 0, int pnAccion = 0)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodProceso", psCodProceso),
                    new SqlParameter("@cDescSubProceso", psDescSubProceso),
                    new SqlParameter("@cAbreviatura", psAbreviatura),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@nIdSubProceso", pnIdSubProceso),
                    new SqlParameter("@Accion", pnAccion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_upd_GestionSubProceso, CommandType.StoredProcedure, parameters);
                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



    }
}
