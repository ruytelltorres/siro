using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using entidad = CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class ControlProceso : IControlProceso
    {
        
        public List<entidad.ControlProceso> ObtenerControlesProceso(string psCodProceso = "")
        {
            try
            {
                SqlParameter sqlCodProceso = new SqlParameter("@cCodProceso", psCodProceso);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarControlesxProceso, CommandType.StoredProcedure, sqlCodProceso))
                {
                    List<entidad.ControlProceso> lstControlProceso = new List<entidad.ControlProceso>();
                    entidad.ControlProceso oControl;
                    while (reader.Read())
                    {
                        oControl = new entidad.ControlProceso()
                        {
                            nCodControl = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodControl")]),
                            cControlDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cControlDescripcion")])
                        };
                        lstControlProceso.Add(oControl);
                    }
                    return lstControlProceso;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<entidad.ControlProceso> MostrarControlesProceso(string psCodProceso = "")
        {
            try
            {
                SqlParameter sqlCodProceso = new SqlParameter("@cCodProceso", psCodProceso);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_MostrarControlesProceso, CommandType.StoredProcedure, sqlCodProceso))
                {
                    List<entidad.ControlProceso> olListaControlesProceso = new List<entidad.ControlProceso>();
                    entidad.ControlProceso oControlesProceso;
                    while (reader.Read())
                    {
                        oControlesProceso = new entidad.ControlProceso()
                        {
                            nCodControl = Convert.ToInt32(reader[reader.GetOrdinal("nCodControl")]),
                            cControlDescripcion = Convert.ToString(reader[reader.GetOrdinal("cControlDescripcion")]),
                            oProceso = new entidad.ProcesoArea()
                            {
                                cCodProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodProceso")]),
                                cDescProceso = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDescProceso")]),
                            },
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaReg")])),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]),
                        };
                        olListaControlesProceso.Add(oControlesProceso);
                    }
                    return olListaControlesProceso;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int RegistraGestionControlProceso(string psCodProceso, string psControlDesc, string psUltimaActualizacion, long pnCodControl = 0, int pnAccion = 0)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@cCodProceso", psCodProceso),
                    new SqlParameter("@cControlDescripcion", psControlDesc),
                    new SqlParameter  ("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@nCodControl", pnCodControl),
                    new SqlParameter("@Accion", pnAccion)
                };

                var exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_upd_GestionControlProceso, CommandType.StoredProcedure, parameters);

                return exito;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


    }
}
