var gnFilasPorPagina = 10;
var gLstAutoevauacionesNotificar = null;

window.onload = function () {
    ListarAutoevaluacionesNotificar();
}

$('#inBuscar').keyup(function () {
    $.uiTableFilter('#tbl-evaluacion-lista-notificacion', this.value);
});

$('#selMostrarRegistros').on('change', function () {
    gnFilasPorPagina = parseInt($(this).val(), 10) || 10;

    if (!gLstAutoevauacionesNotificar) {
        return;
    }

    RenderizarTablaEventosPerdida(gLstAutoevauacionesNotificar, gnFilasPorPagina);
    $('#inBuscar').val('');
});

// $("#inCodEvaluacion").on({
//     "keyup": function (event) {
//         $("#btnBuscarCodigo").prop("disabled", !($(this).val().length == 11));
//     }
// });


// $("#btnBuscarCodigo").click(function (e) {
//     e.preventDefault();
//     $.fn.Conexion({
//         direccion: '/Evaluacion/ListaEvaluacionesNotificar',
//         //datos: {  pnCodEvaluacion: lnCodEvalucion },
//         mensaje: " Obteniendo evaluaciones para notificar",
//         bloqueo: true,
//         terminado: function (data) {
//             datos = JSON.parse(data);
//             //MostrarListaEvluacionesNotificar(datos.oLstDatosEvaluaciones);
//             $("#content-tbl-evaluacion-lista-notificacion").Tabla({
//                 tblId: "tbl-evaluacion-lista-notificacion",
//                 cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,cEvalCod,<strong>Tipo Eval.</strong>,<strong>Fecha Reg.</strong>,<strong>Proc. Actual</strong>",
//                 campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oEvaluaciones.nCodEval,oEvaluaciones.cEvalDesc,dFechaReg,cProcRiesgo",
//                 datos: datos.oLstDatosEvaluaciones,
//                 classtbl: "table table-responsive responsive",
//                 //cantRegVertical: 10,
//                 //scrollVertical: "Si",
//                 alineado: "C,C,L,C,C,C,C,C",
//                 formato: "0,0,0,0,0,0,3,0",
//                 controles: "0,0,4,0,0,0,0",
//                 visible: "0,1,1,1,0,1,1,1",
//                 anchocolumna: "0,10,30,10,0,10,10,10",
//                 sindata: "No se encontró registro de evaluaciones en proceso de gestión",
//                 numerado: "Si",
//                 paginacion: "Si",
//                 ajustar: 'No',
//                 paginacion: { filas: 5, pagina: 1, nombre: "tbl-evaluacion-lista-notificar" },
//                 //opciones: [
//                 //    { Columna: "Tipo1", id: "idMostrarDetalleEvaluacion", claseIcono: "entypo-eye", clase: "DetalleEvaluacion", titulo: "Detalles de la evaluación", function: function (ee, e) { } },
//                 //    { Columna: "Tipo2", id: "idGestionarEvaluacion", claseIcono: "fa fa-cogs", clase: "GestionarEvaluacion", titulo: "Gestion evaluaicón", function: function (ee, e) { } },
//                 //],
//                 seleccion: { function: function (ee, e) { } },
//             });
//         },
//     });

// });

function ListarAutoevaluacionesNotificar() {
    $.fn.Conexion({
        direccion: '/Evaluacion/ListaEvaluacionesNotificar',
        //datos: {  pnCodEvaluacion: lnCodEvalucion },
        mensaje: " Obteniendo autoevaluaciones para notificación",
        bloqueo: false,
        terminado: function (data) {
            datos = JSON.parse(data);
            gLstAutoevauacionesNotificar = datos;
            RenderizarTablaAutoevaluacionesNotificar(gLstAutoevauacionesNotificar, gnFilasPorPagina);
        }
    });
}

function RenderizarTablaAutoevaluacionesNotificar(pDatos, pnFilas) {

    $("#content-tbl-evaluacion-lista-notificacion").Tabla({
        tblId: "tbl-evaluacion-lista-notificacion",
        cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,cEvalCod,<strong>Tipo Eval.</strong>,<strong>Fecha Reg.</strong>,<strong>Proc. Actual</strong>",
        campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oUsuario.cUser,oTipoEvaluacion.nConsValor,oTipoEvaluacion.cConsDescripcion,dFechaRegistro,oProcRiesgo.cConsDescripcion",
        datos: pDatos.oLstDatosEvaluaciones,
        classtbl: "table table-responsive responsive",
        //cantRegVertical: 10,
        //scrollVertical: "Si",
        alineado: "C,C,L,C,C,C,C,C",
        formato: "0,0,0,0,0,0,3,0",
        controles: "0,0,4,0,0,0,0",
        visible: "0,1,1,1,0,1,1,1",
        anchocolumna: "0,10,30,10,0,10,10,10",
        sindata: "No se encontró registro de autoevaluaciones en proceso de notificación",
        numerado: "Si",
        paginacion: "Si",
        ajustar: 'No',
        paginacion: { filas: pnFilas, pagina: 1, nombre: "tbl-evaluacion-lista-notificar" },
        //opciones: [
        //    { Columna: "Tipo1", id: "idMostrarDetalleEvaluacion", claseIcono: "entypo-eye", clase: "DetalleEvaluacion", titulo: "Detalles de la evaluación", function: function (ee, e) { } },
        //    { Columna: "Tipo2", id: "idGestionarEvaluacion", claseIcono: "fa fa-cogs", clase: "GestionarEvaluacion", titulo: "Gestion evaluaicón", function: function (ee, e) { } },
        //],
        seleccion: { function: function (ee, e) { } },
    });
}


// function MostrarListaEvluacionesNotificar(oLstEvaluaciones) {
//     $("#content-tbl-evaluacion-lista-notificacion").Tabla({
//         tblId: "tbl-evaluacion-lista-notificacion",
//         cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,cEvalCod,<strong>Tipo Eval.</strong>,<strong>Fecha Reg.</strong>,<strong>Proc. Actual</strong>",
//         campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oEvaluaciones.nCodEval,oEvaluaciones.cEvalDesc,dFechaReg,cProcRiesgo",
//         datos: oLstEvaluaciones,
//         classtbl: "table table-responsive responsive",
//         //cantRegVertical: 10,
//         //scrollVertical: "Si",
//         alineado: "C,C,L,C,C,C,C,C",
//         formato: "0,0,0,0,0,0,3,0",
//         controles: "0,0,4,0,0,0,0",
//         visible: "0,1,1,1,0,1,1,1",
//         anchocolumna: "0,10,30,10,0,10,10,10",
//         sindata: "No se encontró registro de evaluaciones en proceso de gestión",
//         numerado: "Si",
//         paginacion: "Si",
//         ajustar: 'No',
//         paginacion: { filas: 5, pagina: 1, nombre: "tbl-evaluacion-lista-notificar" },
//         //opciones: [
//         //    { Columna: "Tipo1", id: "idMostrarDetalleEvaluacion", claseIcono: "entypo-eye", clase: "DetalleEvaluacion", titulo: "Detalles de la evaluación", function: function (ee, e) { } },
//         //    { Columna: "Tipo2", id: "idGestionarEvaluacion", claseIcono: "fa fa-cogs", clase: "GestionarEvaluacion", titulo: "Gestion evaluaicón", function: function (ee, e) { } },
//         //],
//         seleccion: { function: function (ee, e) { } },
//     });
// }

$("#btnExportar").click(function (e) {
    e.preventDefault();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVista',
        datos: { Vista: "ExportarAutoevaluacionNotificar", Controlador: "Evaluacion" },
        terminado: function (data) {
            window.location = data;
        },
    });
});

$("#btnNotificar").click(function (e) {
    e.preventDefault();
    /*Dado que el analista de riesgo operacional tiene en lista evaluaciones de otras areas al notificar se debe realizar las siguientes validaciones:
     1. Asegurarse que se notitiquen todas las evaluaciones que pertenecen a una misma area.
     2. 
     
     */
    //if (!ValidaCreacionTaller()) { return; }

    //if (gUsuario.cRHCargoCod === "005011") {
    var lsNroRiesgo = "", lsCodRiesgos = "";

    /*Obtener solo los riesgos chekeados*/
    $("input[type=checkbox]:checked").each(function () {
        lsNroRiesgo += $(this).parent().parent().find("td").eq(1).html() + ",";
        lsCodRiesgos += $(this).parent().parent().find("td").eq(2).html() + ",";
    });
    if (lsNroRiesgo != "") {
        $.fn.Conexion({
            direccion: '/Evaluacion/EnviarNotificionRiesgos',
            datos: { psRiesgos: lsNroRiesgo },
            mensaje: "Notificando al Departamento de Riesgos",
            bloqueo: true,
            terminado: function (data) {
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: data.response.MensajeNotif,//'Se realizó la notificación del los evaluaciones con el código de taller ' + data.CodTaller,
                    titulo: 'Notificacion de la evaluación',
                    tipo_mensaje: data.response.TipoNotif,
                    onhidden: function () {
                        $("#btnBuscarCodigo").click();
                    }
                });

                //if (data.ExitoProceso > 0) {
                //    $.fn.MensajeProcesos({
                //        posicion: 'A',
                //        mensaje: 'Se realizó la notificación del los evaluaciones con el código de taller ' + data.CodTaller,
                //        titulo: 'Notificación exitosa',
                //        tipo_mensaje: 'exito'
                //    });
                //    $("#btnBuscarCodigo").click();
                //} else {
                //    $.fn.MensajeProcesos({
                //        posicion: 'A',
                //        mensaje: 'Las evaluaciones seleccionados no pertenecen al mismo proceso',
                //        titulo: 'Notificación de la Evaluación ' + lsCodRiesgos,
                //        tipo_mensaje: 'informacion'
                //    });
                //}
            },
        });
    } else {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: "No se seleccionó ninguna autoevaluación para la notificación correspondiente",
            titulo: "Notificación",
            tipo_mensaje: "informacion",
        });
    }

    //} else { //Cuando es usuario de otra area
    //    //$("input[type=checkbox]:checked").each(function () {
    //    //    lsNroRiesgo += $(this).parent().parent().find("td").eq(1).html() + ",";
    //    //});

    //}




});


//function ValidaCreacionTaller() {
//    var lsCodProcesoAnt = "", lsCodProcesoAct = "", lsCodSubProcesoAnt = "", lsCodSubProcesoAct = "", lnRecorrer = 0;
//    $("input[type=checkbox]").each(function () {
//        if ($(this).prop("checked")) {
//            lsCodProcesoAct = $(this).parent().parent().find("td").eq(5).html();
//            var lsCodRiesgo = $(this).parent().parent().find("td").eq(2).html();
//            //lsCodSubProcesoAct = $(this).parent().parent().find("td").eq(1).html();
//            lnRecorrer += 1;
//            if (lnRecorrer > 1) {
//                if (lsCodProcesoAct !== lsCodProcesoAnt /*&& lsCodSubProcesoAct == lsCodSubProcesoAnt*/) {

//                    $.fn.MensajeProcesos({
//                        posicion: 'A',
//                        mensaje: 'Las evaluaciones seleccionados no pertenecen al mismo proceso',
//                        titulo: 'Notificación de la Evaluación ' + lsCodRiesgo,
//                        tipo_mensaje: 'informacion'
//                    });

//                    return false; return;
//                }
//            }
//            lsCodProcesoAnt = lsCodProcesoAct;
//            //lsCodSubProcesoAct = lsCodSubProcesoAnt;
//        }
//    });

//    return true;
//}
