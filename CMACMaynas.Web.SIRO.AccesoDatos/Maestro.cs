using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public  class Maestro: IMaestro
    {
        public  string GenerarNroRiesgo(string psFecha = "", string psAgencia = "01", string psArea = "000", string psUsuario = "SIST")
        {
            try
            {
                if (string.IsNullOrEmpty(psFecha)) { psFecha = string.Empty; } else { psFecha += string.Concat(psFecha, ObtenerHoraServidor()); }

                SqlParameter[] parameters = {
                    new SqlParameter("@pdFecha", psFecha),
                    new SqlParameter("@psAgeCod", psAgencia),
                    new SqlParameter("@psAreaCod", psArea),
                    new SqlParameter("@psUserCod", psUsuario),
                    new SqlParameter("@psNroRiesgo", SqlDbType.VarChar, 25) { Direction = ParameterDirection.Output }
                };
                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.GenerarNroRiesgo, CommandType.StoredProcedure, parameters);
                return (string)parameters[4].Value;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        string ObtenerHoraServidor()
        {
            try
            {
                string cHoraServer = string.Empty;
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_HoraServer, CommandType.StoredProcedure))
                {
                    while (reader.Read())
                    {
                        cHoraServer = DataDefault.DbValueToDefault<string>(reader[reader.GetOrdinal("cHoraServer")]);
                    }
                }
                return cHoraServer;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        
    }
}
