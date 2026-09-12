using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class CriteriosEvaluacion : ICriterioEvaluacion
    {
        
        public List<Negocio.CriteriosEvaluacion> ObtenerCriteriosEvaluacion(int pnCriterioCod)
        {
            try
            {
                SqlParameter sqlCriterioCod = new SqlParameter("@nCriterioCod", pnCriterioCod);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarCriteriosEvalucion, CommandType.StoredProcedure, sqlCriterioCod))
                {
                    List<Negocio.CriteriosEvaluacion> olListaCriterios = new List<Negocio.CriteriosEvaluacion>();
                    Negocio.CriteriosEvaluacion oCriterios;
                    while (reader.Read())
                    {
                        oCriterios = new Negocio.CriteriosEvaluacion()
                        {
                            nCriterioValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCriterioValor")]),
                            cCriterioDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCriterioDesc")])
                        };
                        olListaCriterios.Add(oCriterios);
                    }
                    return olListaCriterios;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<Negocio.CriteriosEvaluacion> ObtenerValorCriteriosEval()
        {
            try
            {
                //SqlParameter sqlCriterioCod = new SqlParameter("@nCriterioCod", pnCriterioCod);
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerCriteriosEval, CommandType.StoredProcedure))
                {
                    List<Negocio.CriteriosEvaluacion> olListaCriterios = new List<Negocio.CriteriosEvaluacion>();
                    Negocio.CriteriosEvaluacion oCriterios;
                    while (reader.Read())
                    {
                        oCriterios = new Negocio.CriteriosEvaluacion()
                        {
                            nCriterioCod = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCriterioCod")]),
                            nCriterioValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCriterioValor")]),
                            cCriterioDesc = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCriterioDesc")]),
                            nValorRelacion = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nValorRelacion")]),
                            bEstado = DataDefault.DbValueToDefault<Boolean>(reader[reader.GetOrdinal("bEstado")]),
                            dFechaRegistro = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dFechaReg")]),
                            dFechaActualizacion = DataDefault.DbValueToDefault<DateTime>(reader[reader.GetOrdinal("dUltimaActualizacion")])
                        };
                        olListaCriterios.Add(oCriterios);
                    }
                    return olListaCriterios;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int GrabaConfigCriterioEval(List<dynamic> poConfigCriterio, string psUltimaActualizacion)
        {
            TransactionScope transaction = new TransactionScope();
            try
            {
                int ExitoConfig = 0;
                using (transaction)
                {
                    for (int i = 0; i < poConfigCriterio.Count; i++)
                    {
                        if (poConfigCriterio[i] != null)
                        {
                            SqlParameter[] parameter = {
                            new SqlParameter("@nCodCriterio", (int) poConfigCriterio[i].Cod.Value),
                            new SqlParameter("@nValorCriterio", (int) poConfigCriterio[i].Val.Value),
                            new SqlParameter("@nValorRelacion", (int) poConfigCriterio[i].Rel.Value),
                            new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                        };
                            ExitoConfig += SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_upd_ActualizaCriterioEval, CommandType.StoredProcedure, parameter);

                        }
                    }
                    transaction.Complete();
                }
                return ExitoConfig;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



    }
}
