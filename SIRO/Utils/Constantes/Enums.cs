using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Utils.Constantes
{

    public enum Riesgos : int
    {
        RiesgoOperacional = 1,
        Autoevaluaciones = 2,
        EventoPerdida = 3
    }

    public enum TiposNotificacion : int
    {
        error = 0,
        advertencia = 1,
        informacion = 2
    }

    public enum Mensaje : int
    {
        TipoMensaje = 0,
        Mensaje = 1
    }

    public enum ProcedenciaObservaciones : int
    {
        SolicitudModificacionRiesgo = 1,
        GestionEvaluacion = 2
    }

    public enum Moneda : int
    {
        Soles = 1,
        Dolares = 2
    }

    public enum NivelMenu : int
    {
        PrimerNivel = 1,
        SegundoNivel = 2
    }


    public enum TipoEvaluaciones : int
    {
        Procesos = 1001,
        SubContratacionSignificativa = 1002,
        NuevoProducto = 1003,
        Areas = 1004
    }

    public enum ProcesoGestion : int
    {
        Registrado = 1,
        Paso_1 = 1,
        Paso_2 = 2,
        Paso_3 = 3,
        Paso_4 = 4,
        Paso_5 = 5,
        Finalizado = 6
    }


    public enum Respuesta : int
    {
        informacion = 1,
        exito = 2,
        error = 3,
        advertencia = 4
    }

}