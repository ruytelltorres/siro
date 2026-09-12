using CMACMaynas.Web.SIRO.AccesoDatos.Connect;
using CMACMaynas.Web.SIRO.AccesoDatos.Helper;
using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class GestionIncentivo : IGestionIncentivo
    {
        
        public List<Negocio.Incentivos> ObtenerRiesgoOperacionalIncentivo(int pnTrimestre, int pnAnio)
        {
            try
            {
                SqlParameter[] parameters =  {
                    new SqlParameter("@nTrimestre", pnTrimestre),
                    new SqlParameter("@nAnio", pnAnio)
                };

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerRiesgoGestionIncentivos, CommandType.StoredProcedure, parameters))
                {
                    List<Negocio.Incentivos> LstRiesgoOpeIncentivo = new List<Negocio.Incentivos>();
                    Negocio.Incentivos oRiesgoOpe;
                    while (reader.Read())
                    {
                        oRiesgoOpe = new Negocio.Incentivos();

                        oRiesgoOpe.oDatosRiesgo = new Negocio.DatosRiesgos();
                        oRiesgoOpe.oDatosRiesgo.oUsuarios = new Negocio.Usuario();
                        oRiesgoOpe.oProbabilidadInherente = new Negocio.Constante();
                        oRiesgoOpe.oImpactoInherente = new Negocio.Constante();
                        oRiesgoOpe.oNivelRiesgoInherente = new Negocio.Constante();
                        oRiesgoOpe.oMontoPerdida = new Negocio.MontoPerdida();


                        oRiesgoOpe.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oRiesgoOpe.oDatosRiesgo.cCodRiesgo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cCodRiesgo")]);
                        oRiesgoOpe.oDatosRiesgo.nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoRiesgo")]);
                        oRiesgoOpe.oDatosRiesgo.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oRiesgoOpe.oDatosRiesgo.oUsuarios.cUser = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cUser")]);
                        oRiesgoOpe.oDatosRiesgo.dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDetec")]));
                        oRiesgoOpe.oProbabilidadInherente.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProbabilidadInherente")]);
                        oRiesgoOpe.oImpactoInherente.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cImpactoInhrente")]);
                        oRiesgoOpe.oNivelRiesgoInherente.nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNivelRiesgoInherente")]);
                        oRiesgoOpe.oNivelRiesgoInherente.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNivelRiesgoInherente")]);
                        oRiesgoOpe.oMontoPerdida.nMontoPerdida = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nMontoPerdida")]);
                        oRiesgoOpe.nMontoPropuesto = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nIncentivoPropuesto")]);
                        oRiesgoOpe.nMontoIncentivo = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nIncentivo")]);
                        oRiesgoOpe.bIncentivo = Convert.ToBoolean(reader[reader.GetOrdinal("bIncentivo")]);

                        LstRiesgoOpeIncentivo.Add(oRiesgoOpe);
                    }
                    return LstRiesgoOpeIncentivo;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Negocio.Incentivos ObtenerDetalleIncentivo(long pnNroRiesog)
        {
            try
            {
                SqlParameter sqlNroRiesgo = new SqlParameter("@nNroRiesgo", pnNroRiesog);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_ObtenerDetalleIncentivo, CommandType.StoredProcedure, sqlNroRiesgo))
                {
                    Negocio.Incentivos oDetalleIncentivo = null;
                    while (reader.Read())
                    {
                        oDetalleIncentivo = new Negocio.Incentivos();

                        oDetalleIncentivo.oDatosRiesgo = new Negocio.DatosRiesgos();
                        oDetalleIncentivo.oTipoIncentivo = new Negocio.Constante();

                        oDetalleIncentivo.oDatosRiesgo.nNroRiesgo = (long)DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nNroRiesgo")]);
                        oDetalleIncentivo.oDatosRiesgo.nTpoRiesgo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoRiesgo")]);
                        oDetalleIncentivo.oDatosRiesgo.cRiesgoIdentiticado = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cRiesgoIdentificado")]);
                        oDetalleIncentivo.oDatosRiesgo.dFechaDeteccion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaDetec")]));
                        oDetalleIncentivo.oTipoIncentivo.nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nTpoIncentivo")]);
                        oDetalleIncentivo.oTipoIncentivo.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDesTpoIncentivo")]);
                        oDetalleIncentivo.bIncentivo = Convert.ToBoolean(reader[reader.GetOrdinal("nIncentivo")]);
                        oDetalleIncentivo.cComentarioIncentivo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cComentIncentivo")]);
                        oDetalleIncentivo.nMontoIncentivo = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nMontoInc")]);
                        oDetalleIncentivo.cNombreDocIncentivo = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDocAjunto")]);
                        oDetalleIncentivo.cNombreDocIncentivoDB = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cDocAjuntoBD")]);
                        oDetalleIncentivo.dFechaGestion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaGestion")]));
                        oDetalleIncentivo.dFechaActualizacion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaActualizacion")]));
                    }
                    return oDetalleIncentivo;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public int GrabaGestionIncentivo(long pnNroRiesog, int pnMotivo, string psComentario, decimal psMontoInc, string cNombreDoc, string cNombreDocDB, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nNroRiesgo", pnNroRiesog),
                    new SqlParameter("@nMotivoIncentivo", pnMotivo),
                    new SqlParameter("@cComentario", psComentario),
                    new SqlParameter("@nMontoInc", psMontoInc),
                    new SqlParameter("@cNombreDoc", cNombreDoc),
                    new SqlParameter("@cNombreDocDB", cNombreDocDB),
                    //SqlParameter sqlEstado = new SqlParameter("@nEstadoInc", pnEstado);
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion)
                };

                int exito = SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_ins_GrabarGestionIncentivo, CommandType.StoredProcedure, parameters);

                return exito;

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string GrabaConfigIncentivo(int pnProbabilidad, int pnImpacto, decimal psMontoInc, string psUltimaActualizacion)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@nProbabilidad", pnProbabilidad),
                    new SqlParameter("@nImpacto", pnImpacto),
                    new SqlParameter("@nMontoIncentivo", psMontoInc),
                    new SqlParameter("@cUltimaActualizacion", psUltimaActualizacion),
                    new SqlParameter("@cErrorValid", SqlDbType.VarChar, 150) { Direction = ParameterDirection.Output }
                };
                SqlHelper.ExecuteNonQuery(ConectarBD.Get.ConnectionString(), SP.stp_sel_GrabaConfigIncentivo, CommandType.StoredProcedure, parameters);

                return (string)parameters[4].Value;

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<Negocio.Incentivos> MostrarConfigMontoIncentos()
        {
            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConectarBD.Get.ConnectionString(), SP.stp_sel_MostrarConfigIncentivo, CommandType.StoredProcedure))
                {
                    List<Negocio.Incentivos> LstConfigIncentivo = new List<Negocio.Incentivos>();
                    Negocio.Incentivos oIncentivos;
                    while (reader.Read())
                    {
                        oIncentivos = new Negocio.Incentivos();
                        oIncentivos.oNivelRiesgoInherente = new Negocio.Constante();
                        oIncentivos.oProbabilidadInherente = new Negocio.Constante();
                        oIncentivos.oImpactoInherente = new Negocio.Constante();

                        oIncentivos.nCodConfigIncentivo = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nCodConfigInc")]);
                        oIncentivos.oNivelRiesgoInherente.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cNivelRiesgoIhrente")]);
                        oIncentivos.oProbabilidadInherente.nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nProbabilidad")]);
                        oIncentivos.oProbabilidadInherente.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cProbabilidad")]);
                        oIncentivos.oImpactoInherente.nConsValor = DataDefault.DbValueToDefault<Int32>(reader[reader.GetOrdinal("nImpacto")]);
                        oIncentivos.oImpactoInherente.cConsDescripcion = DataDefault.DbValueToDefault<String>(reader[reader.GetOrdinal("cImpacto")]);
                        oIncentivos.nMontoIncentivo = DataDefault.DbValueToDefault<Decimal>(reader[reader.GetOrdinal("nMontoIncentivo")]);
                        oIncentivos.dFechaGestion = DataDefault.DbValueToDefault<DateTime>(DataDefault.toDatetimeDefault(reader[reader.GetOrdinal("dFechaRegistro")]));
                        LstConfigIncentivo.Add(oIncentivos);
                    }
                    return LstConfigIncentivo;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
