using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;


namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class Agencias: IAgencias
    {
        
        public List<Negocio.Agencias> ObtenerAgencias()
        {
            try
            {
                using (IDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ListarAgencias, CommandType.StoredProcedure))
                {
                    List<Negocio.Agencias> ListaAgencias = new List<Negocio.Agencias>();
                    Negocio.Agencias agencias;

                    while (reader.Read())
                    {
                        agencias = new Negocio.Agencias();
                        agencias.cAgeCod = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeCod")]);
                        agencias.cAgeDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cAgeDescripcion")]);
                        agencias.nAgeEstado = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nEstado")]);

                        ListaAgencias.Add(agencias);
                    }
                    return ListaAgencias;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



    }
}
