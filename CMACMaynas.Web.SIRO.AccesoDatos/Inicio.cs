using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Inicio : IInicio
    {
        private static bool ExisteColumna(IDataRecord reader, string nombreColumna)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string ObtenerValorTexto(IDataRecord reader, string nombreColumna, string valorDefecto = "0")
        {
            if (!ExisteColumna(reader, nombreColumna))
            {
                return valorDefecto;
            }

            var indice = reader.GetOrdinal(nombreColumna);
            return reader.IsDBNull(indice) ? valorDefecto : Convert.ToString(reader[indice]);
        }

        
        public  List<dynamic> ObtenerInformacionInicio(string psUsuario)
        {
            try
            {
               
                SqlParameter pcUser = new SqlParameter("@cUser", psUsuario);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDatosPrincipal, CommandType.StoredProcedure, pcUser))
                {
                    List<dynamic> LstInfoIncio = new List<dynamic>();
                    dynamic dInicio = null;
                    while (reader.Read())
                    {

                        dInicio = new System.Dynamic.ExpandoObject();
                        dInicio.RiesgoOpe = ObtenerValorTexto(reader, "RiesgoOperacional");
                        dInicio.MofRiesgoOpe = ObtenerValorTexto(reader, "ModificacionRiesgoOperacional");
                        dInicio.PlanAccionRiesgoOpe = ObtenerValorTexto(reader, "PlanAccionRiesgo");
                        dInicio.Evaluaciones = ObtenerValorTexto(reader, "Evaluaciones");
                        dInicio.PlanAccionEval = ObtenerValorTexto(reader, "PlanAccionEvaluacion");
                        dInicio.EvaluacionesPendientes = ObtenerValorTexto(reader, "EvaluacionesPendientes", dInicio.PlanAccionEval);
                        LstInfoIncio.Add(dInicio);
                    }
                    return LstInfoIncio;
                }
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }

        //public static List<InicioEN> ObtenerInformacionInicio(string _cUser)
        //{
        //    try
        //    {
        //        SqlParameter pcUser = new SqlParameter("@cUser", _cUser);

        //        using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectDB.CadenaConexionBD, SP.stp_sel_ObtenerInfoInicio, CommandType.StoredProcedure, pcUser))
        //        {
        //            List<InicioEN> ListDI = new List<InicioEN>();
        //            InicioEN DI;
        //            while (reader.Read())
        //            {
        //                DI = new InicioEN();
        //                DI.nCantRiesgoOperacional = reader.GetString(0);
        //                DI.nCantAutoevaluacion = reader.GetString(1);
        //                ListDI.Add(DI);
        //            }
        //            return ListDI;
        //        }
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}


    }
}
